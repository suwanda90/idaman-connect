using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Config
{
    public class Menu : BaseEntity<Guid>
    {
        public Guid? FkParentId { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Controller { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string SequenceNumber { get; set; }
        
        public string Icon { get; set; }

        public bool IsSection { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string AccessTypes { get; set; }
    }
}
