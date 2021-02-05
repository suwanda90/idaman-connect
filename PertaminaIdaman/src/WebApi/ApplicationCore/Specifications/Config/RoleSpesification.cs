using ApplicationCore.Entities.Config;

namespace ApplicationCore.Specifications.Config
{
    public class RoleSpesification : SpecificationQuery<Role>
    {
        public RoleSpesification()
        {
            ApplyOrderBy(c => c.Name);
        }

        public RoleSpesification(bool isActive)
            : base(c => c.IsActive == isActive && c.IsActive == true)
        {
            ApplyOrderBy(c => c.Name);
        }
    }
}
