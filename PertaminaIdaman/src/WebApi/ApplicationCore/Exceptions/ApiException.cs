using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationCore.Helpers.Api.Model;
using Newtonsoft.Json;

namespace ApplicationCore.Exceptions
{
    public static class ApiException
    {
        public static async Task ThrowResponseErrorAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(content))
            {
                var apiResponse = JsonConvert.DeserializeObject<BaseApiResponse>(content);

                if (apiResponse.StatusCode == (int)HttpStatusCode.BadRequest)
                {
                    throw new BadRequestException(apiResponse.Message);
                }
                else if (apiResponse.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException(apiResponse.Message);
                }
                else if (apiResponse.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    throw new ForbiddenException(apiResponse.Message);
                }
                else if (apiResponse.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(apiResponse.Message);
                }
                else if (apiResponse.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException(apiResponse.Message);
                }
            }
            else
            {
                throw new BadRequestException();
            }
        }
    }
}
