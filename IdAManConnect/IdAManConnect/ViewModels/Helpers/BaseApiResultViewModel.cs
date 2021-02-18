namespace IdAManConnect.ViewModels.Helpers
{
    public class BaseApiResultViewModel<TEntity>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }
    }
}
