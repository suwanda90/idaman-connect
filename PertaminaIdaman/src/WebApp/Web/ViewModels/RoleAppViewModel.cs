using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class RoleAppViewModel : BaseEntityViewModel<Guid>
    {
        [Required]
        public string Name { get; set; }
    }
}
