using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Interfaces.Config;
using ApplicationCore.Specifications.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Config
{
    public class MenuRoleService : IMenuRoleService
    {
        private readonly IEfRepository<MenuRole, Guid> _repository;

        public MenuRoleService(IEfRepository<MenuRole, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<DatatablesPagedResults<MenuRole>> DatatablesAsync(DatatablesParameter param)
        {
            var spec = new MenuRoleSpesification();
            return await _repository.DatatablesAsync(param, spec);
        }

        public async Task<DatatablesPagedResults<MenuRole>> GetAsync(DataParameter param)
        {
            var spec = new MenuRoleSpesification();
            var source = await _repository.GetAllAsync(spec);

            return await _repository.GetByPagingAsync(source, param.Start, param.Length);
        }

        public async Task<IReadOnlyList<MenuRole>> GetAllAsync()
        {
            var spec = new MenuRoleSpesification();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<IReadOnlyList<MenuRole>> GetAllAsync(Guid fkRoleId)
        {
            var spec = new MenuRoleSpesification();
            var source = await _repository.GetAllAsync(spec);

            return source.Where(x => x.FkRoleId == fkRoleId).ToList();
        }

        public async Task<MenuRole> GetAsync(Guid id)
        {
            var spec = new MenuRoleSpesification();
            return await _repository.GetAsync(spec, id);
        }

        public async Task<Guid?> PostAsync(MenuRole model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.FkRoleId), model.FkRoleId },
                { nameof(model.FkMenuId), model.FkMenuId }
            };

            if (!await _repository.IsExistDataAsync(where))
            {
                var data = await _repository.AddAsync(model);
                id = data.Id;
            }

            return id;
        }

        public async Task<Guid?> PutAsync(MenuRole model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.FkRoleId), model.FkRoleId },
                { nameof(model.FkMenuId), model.FkMenuId }
            };

            var param = new ExistWithKeyModel
            {
                KeyName = nameof(model.Id),
                KeyValue = model.Id,
                FieldName = nameof(model.AccessTypes),
                FieldValue = model.AccessTypes,
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
    }
}
