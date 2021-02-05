using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ClientApiViewModel : BaseEntityViewModel<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Token { get; set; }

        public DateTime? ExpiredToken { get; set; }
    }
}
