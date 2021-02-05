using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Helpers.Select2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Config
{
    public interface IRoleService
    {
        Task<DatatablesPagedResults<Role>> DatatablesAsync(DatatablesParameter param);

        Task<IReadOnlyList<Role>> GetAllAsync();

        Task<Role> GetAsync(Guid id);

        Task<DatatablesPagedResults<Role>> GetAsync(DataParameter param);

        Task<Guid?> PostAsync(Role model);

        Task<Guid?> PutAsync(Role model);

        Task DeleteAsync(Guid id);

        Task<bool> IsExistDataAsync(IDictionary<string, object> where);

        Task<bool> IsExistDataWithKeyAsync(ExistWithKeyModel model);

        Task<IReadOnlyList<Select2Binding>> BindingSelect2Async();

        Task<Select2Result> BindingSelect2Async(Guid id);
    }
}
