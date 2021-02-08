using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    public class RoleSpecification : SpecificationQuery<Role>
    {
        public RoleSpecification()
        {
            ApplyOrderBy(c => c.Name);
        }
    }
}
