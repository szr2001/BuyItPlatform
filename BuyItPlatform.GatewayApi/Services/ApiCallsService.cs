using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace BuyItPlatform.GatewayApi.Services
{
    //handles all api requests
    public class ApiCallsService : IApiCallsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ITokensProvider tokensProvider;
        public ApiCallsService(IHttpClientFactory httpClientFactory, ITokensProvider tokensProvider)
        {
            this.httpClientFactory = httpClientFactory;
            this.tokensProvider = tokensProvider;
        }

        public async Task<ResponseDto<T>> SendAsync<T>(RequestDto request, bool withTokens = true)
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
                    if(token != null)
                    {
                        tokensProvider.SetToken(token);
                    }
                    if (refreshToken != null) 
                    {
                        tokensProvider.SetRefreshToken(refreshToken);
                    }
                }

                message.RequestUri = new Uri(request.Url);
                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
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

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { Success = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { Success = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { Success = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { Success = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        //use generics so it's able to deserialize the responseDto correctly, if we just use an object type,
                        //then it would faill because it does't know in what to deserialize it.
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto<T>>(apiContent);
                        return apiResponseDto!;
                }
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = ex.Message };
            }
        }
    }
}
