using ApplicationCore.Helpers.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Helpers.ApiResponse
{
    public static class ApiResponseHelper
    {
        public static ObjectResult ReturnResponse(this BaseApiResponse value)
        {
            var objectResult = new ObjectResult(value)
            {
                Value = value,
                StatusCode = value.StatusCode
            };

            return objectResult;
        }
    }
}
