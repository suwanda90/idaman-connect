using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Helpers.Select2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Config
{
    public interface IClientApiService
    {
        Task<DatatablesPagedResults<ClientApi>> DatatablesAsync(DatatablesParameter param);

        Task<IReadOnlyList<ClientApi>> GetAllAsync();

        Task<ClientApi> GetAsync(Guid id);

        Task<DatatablesPagedResults<ClientApi>> GetAsync(DataParameter param);

        Task<Guid?> PostAsync(ClientApi model);

        Task<Guid?> PutAsync(ClientApi model);

        Task DeleteAsync(Guid id);

        Task<bool> IsExistDataAsync(IDictionary<string, object> where);

        Task<bool> IsExistDataWithKeyAsync(ExistWithKeyModel model);

        Task<IReadOnlyList<Select2Binding>> BindingSelect2Async();

        Task<Select2Result> BindingSelect2Async(Guid id);
    }
}
