using System.Collections.Generic;

namespace ApplicationCore.Helpers.Api.Model
{
    public class BaseApiResults<TEntity>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IReadOnlyList<TEntity> Data { get; set; }
    }
}
