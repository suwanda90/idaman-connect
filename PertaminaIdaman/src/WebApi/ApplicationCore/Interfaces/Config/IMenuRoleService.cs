using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Config
{
    public interface IMenuRoleService
    {
        Task<DatatablesPagedResults<MenuRole>> DatatablesAsync(DatatablesParameter param);

        Task<IReadOnlyList<MenuRole>> GetAllAsync();

        Task<IReadOnlyList<MenuRole>> GetAllAsync(Guid fkRoleId);

        Task<MenuRole> GetAsync(Guid id);

        Task<DatatablesPagedResults<MenuRole>> GetAsync(DataParameter param);

        Task<Guid?> PostAsync(MenuRole model);

        Task<Guid?> PutAsync(MenuRole model);

        Task DeleteAsync(Guid id);

        Task<bool> IsExistDataAsync(IDictionary<string, object> where);

        Task<bool> IsExistDataWithKeyAsync(ExistWithKeyModel model);
    }
}
