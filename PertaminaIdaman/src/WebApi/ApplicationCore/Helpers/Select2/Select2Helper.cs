using System.Collections.Generic;
using ApplicationCore.Helpers.Select2.Model;

namespace ApplicationCore.Helpers.Select2
{
    public static class Select2Helper
    {
        public static Select2Result BindingSelect2Edit(this IList<Select2Binding> result, object id, string text)
        {
            var select2Result = new Select2Result
            {
                Key = id,
                Result = result,
                Value = text
            };

            return select2Result;
        }
    }
}
