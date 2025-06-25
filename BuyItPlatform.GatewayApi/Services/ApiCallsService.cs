using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace BuyItPlatform.GatewayApi.Services
{
    //handles all api requests
    //bug with token refresh handling in frontend and backend
    //if the token expired, it returns unauthrozied, then the frontend picks it up and tries to do the api call to refresh the token
    //if that one also returns unauthorized, then it redirects the user to login page, if it returns 200, then it tries to call the previous api again
    //so it refreshse the token in the frontend automatically
    //but sometimes the frontend does 2 api calls one after the other, and both of them return unauthorized, so the frontend redirects the user to login
    //before the refresh token api call was a success or not, so in the backend return another http code instead of 401 and modify the forntend
    //so it won't redirect the user to login page after 2 consecutive 401 but after a specific http code, this way the problem will go away.
    public class ApiCallsService : IApiCallsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ITokenCookiesProvider tokensProvider;
        public ApiCallsService(IHttpClientFactory httpClientFactory, ITokenCookiesProvider tokensProvider)
        {
            this.httpClientFactory = httpClientFactory;
            this.tokensProvider = tokensProvider;
        }

        public async Task<MicroserviceResponseDto<T>> SendAsync<T>(RequestDto request, bool withTokens = true)
        {
            try
            {
                HttpClient client = httpClientFactory.CreateClient("BuyItApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                
                //add the tokens
                if (withTokens)
                {
                    var token = tokensProvider.GetToken();
                    var refreshToken = tokensProvider.GetRefreshToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                    if (refreshToken != null) 
                    {
                        message.Headers.Add("Cookie", $"{ITokenCookiesProvider.RefreshTokenKey}={refreshToken}");
                    }
                    if(token != null)
                    {
                        message.Headers.Add("Cookie", $"{ITokenCookiesProvider.TokenKey}={token}");
                    }
                }

                //check if the data is BodyData meaning Json
                message.RequestUri = new Uri(request.Url);
                if (request.BodyData != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.BodyData), Encoding.UTF8, "application/json");
                }
                //or FormData meaning binary
                //and serialize it correctly
                else if (request.FormData != null)
                {
                    var form = new MultipartFormDataContent();
                    foreach (var prop in request.FormData.GetType().GetProperties())
                    {
                        var value = prop.GetValue(request.FormData);
                        var name = prop.Name;

                        if (value == null) continue;

                        //use a switch
                        if (value is IFormFile file)
                        {
                            var fileContent = new StreamContent(file.OpenReadStream());
                            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                            form.Add(fileContent, name, file.FileName);
                        }
                        else if (value is IEnumerable<IFormFile> fileList)
                        {
                            foreach (var f in fileList)
                            {
                                var fileContent = new StreamContent(f.OpenReadStream());
                                fileContent.Headers.ContentType = new MediaTypeHeaderValue(f.ContentType);
                                form.Add(fileContent, name, f.FileName);
                            }
                        }
                        else if (value is IEnumerable<string> list)
                        {
                            foreach (var item in list)
                            {
                                form.Add(new StringContent(item), name);
                            }
                        }
                        else
                        {
                            form.Add(new StringContent(value.ToString()), name);
                        }
                    }
                    message.Content = form;
                }

                HttpResponseMessage? apiResponse = null;

                switch (request.ApiType)
                {
                    case Enums.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Enums.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Enums.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();


                var apiResponseDto = JsonConvert.DeserializeObject<MicroserviceResponseDto<T>>(apiContent);
                if (apiResponseDto == null)
                {
                    return new()
                    {
                        Success = false,
                        StatusCode = (int)apiResponse.StatusCode,
                        Message = apiResponse.StatusCode.ToString()
                    };
                }
                apiResponseDto.StatusCode = (int)apiResponse.StatusCode;
                if (!apiResponse.IsSuccessStatusCode && string.IsNullOrEmpty(apiResponseDto.Message))
                {
                    apiResponseDto.Message = apiContent;
                }

                return apiResponseDto;
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = ex.Message };
            }
        }
    }
}
