using System;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Helpers.File.Model
{
    public class UploadFile
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public IFormFile File { get; set; }

        public Guid FkTransactionId { get; set; }

        public Guid? FkDocumentId { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
