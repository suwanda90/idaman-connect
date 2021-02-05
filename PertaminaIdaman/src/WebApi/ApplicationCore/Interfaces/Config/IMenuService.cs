using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Helpers.Select2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Config
{
    public interface IMenuService
    {
        Task<DatatablesPagedResults<Menu>> DatatablesAsync(DatatablesParameter param);

        Task<IReadOnlyList<Menu>> GetAllAsync();

        Task<Menu> GetAsync(Guid id);

        Task<DatatablesPagedResults<Menu>> GetAsync(DataParameter param);

        Task<Guid?> PostAsync(Menu model);

        Task<Guid?> PutAsync(Menu model);

        Task DeleteAsync(Guid id);

        Task<bool> IsExistDataAsync(IDictionary<string, object> where);

        Task<bool> IsExistDataWithKeyAsync(ExistWithKeyModel model);

        Task<IReadOnlyList<Select2Binding>> BindingSelect2Async();

        Task<Select2Result> BindingSelect2Async(Guid id);

        Task<IReadOnlyList<Select2Binding>> BindingSelect2ParentAsync();

        Task<Select2Result> BindingSelect2ParentAsync(Guid id);
    }
}
