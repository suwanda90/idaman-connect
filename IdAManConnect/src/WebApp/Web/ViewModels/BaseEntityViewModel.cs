using System;

namespace Web.ViewModels
{
    public class BaseEntityViewModel<TId>
    {
        public virtual TId Id { get; set; }

        public DateTime? DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
