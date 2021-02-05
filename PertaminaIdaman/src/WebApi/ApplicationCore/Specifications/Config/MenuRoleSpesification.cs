using ApplicationCore.Entities.Config;
using System;

namespace ApplicationCore.Specifications.Config
{
    public class MenuRoleSpesification : SpecificationQuery<MenuRole>
    {
        public MenuRoleSpesification()
        {
            ApplyOrderBy(c => c.Menu.Name);

            AddIncludes(query => query
            .Include(e => e.Role));
        }

        public MenuRoleSpesification(bool isActive, Guid fkMenuId, Guid fkRoleId)
            : base(c => c.IsActive == isActive && c.Role.IsActive == true && c.Menu.IsActive == true && c.FkMenuId == fkMenuId && c.FkRoleId == fkRoleId)
        {
            ApplyOrderBy(c => c.Menu.Name);

            AddIncludes(query => query
            .Include(e => e.Role));
        }
    }
}
