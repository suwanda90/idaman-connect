namespace ApplicationCore.Helpers.Api.Model
{
    public class BaseApiResult<TEntity>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }
    }
}
