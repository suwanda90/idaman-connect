using System;
using System.ComponentModel.DataAnnotations;

namespace IdAManConnect.ViewModels
{
    public class RoleAppViewModel : BaseEntityViewModel<Guid>
    {
        [Required]
        public string Name { get; set; }
    }
}
