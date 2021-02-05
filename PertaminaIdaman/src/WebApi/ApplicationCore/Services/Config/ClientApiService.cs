using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Helpers.Select2;
using ApplicationCore.Helpers.Select2.Model;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Interfaces.Config;
using ApplicationCore.Specifications.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Config
{
    public class ClientApiService : IClientApiService
    {
        private readonly IEfRepository<ClientApi, Guid> _repository;

        public ClientApiService(IEfRepository<ClientApi, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<DatatablesPagedResults<ClientApi>> DatatablesAsync(DatatablesParameter param)
        {
            var spec = new ClientApiSpecification();
            return await _repository.DatatablesAsync(param, spec);
        }

        public async Task<DatatablesPagedResults<ClientApi>> GetAsync(DataParameter param)
        {
            var spec = new ClientApiSpecification();
            var source = await _repository.GetAllAsync(spec);

            return await _repository.GetByPagingAsync(source, param.Start, param.Length);
        }

        public async Task<IReadOnlyList<ClientApi>> GetAllAsync()
        {
            var spec = new ClientApiSpecification();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<ClientApi> GetAsync(Guid id)
        {
            var spec = new ClientApiSpecification();
            return await _repository.GetAsync(spec, id);
        }

        public async Task<Guid?> PostAsync(ClientApi model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.ClientId), model.ClientId}
            };

            if (!await _repository.IsExistDataAsync(where))
            {
                var data = await _repository.AddAsync(model);
                id = data.Id;
            }

            return id;
        }

        public async Task<Guid?> PutAsync(ClientApi model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.ClientId), model.ClientId}
            };

            var param = new ExistWithKeyModel
            {
                KeyName = nameof(model.Id),
                KeyValue = model.Id,
                FieldName = nameof(model.ClientId),
                FieldValue = model.ClientId,
                WhereData = where
            };

            if (!await _repository.IsExistDataWithKeyAsync(param))
            {
                await _repository.UpdateAsync(model);
                id = model.Id;
            }

            return id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var data = await _repository.GetAsync(id);
            await _repository.DeleteAsync(data);
        }

        public async Task<bool> IsExistDataAsync(IDictionary<string, object> where)
        {
            return await _repository.IsExistDataAsync(where);
        }

        public async Task<bool> IsExistDataWithKeyAsync(ExistWithKeyModel model)
        {
            return await _repository.IsExistDataWithKeyAsync(model);
        }

        public async Task<IReadOnlyList<Select2Binding>> BindingSelect2Async()
        {
            var spec = new ClientApiSpecification();
            var source = await _repository.GetAllAsync(spec);

            var result = source.Where(x => x.IsActive == true)
            .Select(x => new Select2Binding
            {
                Id = x.Id,
                Text = x.Name
            }).ToList();

            return result;
        }

        public async Task<Select2Result> BindingSelect2Async(Guid id)
        {
            var spec = new ClientApiSpecification();
            var data = await _repository.GetAsync(id);
            var text = string.Empty;

            if (data != null)
            {
                text = data.Name;
            }

            var source = await _repository.GetAllAsync(spec);
            var result = source.Where(x => x.IsActive == true)
                .Select(x => new Select2Binding
                {
                    Id = x.Id,
                    Text = x.Name
                }).ToList();

            return result.BindingSelect2Edit(id, text);
        }
    }
}
