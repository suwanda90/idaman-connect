using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.Config
{
    public class MenuRole : BaseEntity<Guid>
    {
        [ForeignKey("FkMenuId")]
        public Menu Menu { get; set; }
        public Guid? FkMenuId { get; set; }

        [ForeignKey("FkRoleId")]
        public Role Role { get; set; }
        public Guid? FkRoleId { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string AccessTypes { get; set; }
    }
}
