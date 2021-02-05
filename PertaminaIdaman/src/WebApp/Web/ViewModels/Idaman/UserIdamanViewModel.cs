using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Web.ViewModels.Idaman
{
    public class UserIdamanViewModel
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "organizationId")]
        public string OrganizationId { get; set; }

        [JsonProperty(PropertyName = "positionId")]
        public string PositionId { get; set; }

        [JsonProperty(PropertyName = "companyCode")]
        public string CompanyCode { get; set; }

        [JsonProperty(PropertyName = "kbo")]
        public string Kbo { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public string IsActive { get; set; }

        [JsonProperty(PropertyName = "birthday")]
        public string Birthday { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "companyName")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "department")]
        public string Department { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "employeeId")]
        public string EmployeeId { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "hireDate")]
        public string HireDate { get; set; }

        [JsonProperty(PropertyName = "jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty(PropertyName = "website")]
        public string Website { get; set; }

        [JsonProperty(PropertyName = "aboutMe")]
        public string AboutMe { get; set; }

        [JsonProperty(PropertyName = "officeLocation")]
        public string OfficeLocation { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "userType")]
        public string UserType { get; set; }

        [JsonProperty(PropertyName = "photo")]
        public string Photo { get; set; }

        [JsonProperty(PropertyName = "extensionAttributes")]
        public object ExtensionAttributes { get; set; }

        [JsonProperty(PropertyName = "idp")]
        public string Idp { get; set; }

        [JsonProperty(PropertyName = "directoryId")]
        public string DirectoryId { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "created")]
        public string Created { get; set; }

        [JsonProperty(PropertyName = "updated")]
        public string Updated { get; set; }

        [JsonProperty(PropertyName = "employeeNumber")]
        public string EmployeeNumber { get; set; }

        [JsonProperty(PropertyName = "employeeType")]
        public string EmployeeType { get; set; }

        [JsonProperty(PropertyName = "manager")]
        public string Manager { get; set; }

        [JsonProperty(PropertyName = "roomNumber")]
        public string RoomNumber { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "division")]
        public string Division { get; set; }

        [JsonProperty(PropertyName = "cultureInfo")]
        public string CultureInfo { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "dateFormat")]
        public string DateFormat { get; set; }

        [JsonProperty(PropertyName = "timeFormat")]
        public string TimeFormat { get; set; }

        [JsonProperty(PropertyName = "isCheif")]
        public bool IsCheif { get; set; }
    }
}