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
        public ApiCallsService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto<T>> SendAsync<T>(RequestDto request)
        {
            try
            {
                HttpClient client = httpClientFactory.CreateClient("BuyItApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //add token later on god, no cap

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
