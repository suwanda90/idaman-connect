using System.Collections.Generic;

namespace IdAManConnect.ViewModels.Helpers
{
    public class BaseApiResultsViewModel<TEntity>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IReadOnlyList<TEntity> Data { get; set; }
    }
}
