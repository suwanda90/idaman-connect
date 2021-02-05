namespace ApplicationCore.Helpers.Api.Model
{
    public class BaseApiResponse
    {
        public int StatusCode { get; set; }
        public string StatusText { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public void SetBaseApiResponse(int statusCode, string statusText, string message, object data)
        {
            StatusCode = statusCode;
            StatusText = statusText;
            Message = message;
            Data = data;
        }
    }
}
