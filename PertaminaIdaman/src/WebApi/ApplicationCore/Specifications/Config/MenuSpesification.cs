using ApplicationCore.Entities.Config;

namespace ApplicationCore.Specifications.Config
{
    public class MenuSpesification : SpecificationQuery<Menu>
    {
        public MenuSpesification()
        {
            ApplyOrderBy(c => c.Name);
        }

        public MenuSpesification(bool isActive)
            : base(c => c.IsActive == isActive && c.IsActive == true)
        {
            ApplyOrderBy(c => c.Name);
        }
    }
}
