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
    public class MenuService : IMenuService
    {
        private readonly IEfRepository<Menu, Guid> _repository;
        private readonly IEfRepository<MenuRole, Guid> _menuRoleRepository;

        public MenuService(IEfRepository<Menu, Guid> repository, IEfRepository<MenuRole, Guid> menuRoleRepository)
        {
            _repository = repository;
            _menuRoleRepository = menuRoleRepository;
        }

        public async Task<DatatablesPagedResults<Menu>> DatatablesAsync(DatatablesParameter param)
        {
            var spec = new MenuSpesification();
            return await _repository.DatatablesAsync(param, spec);
        }

        public async Task<DatatablesPagedResults<Menu>> GetAsync(DataParameter param)
        {
            var spec = new MenuSpesification();
            var source = await _repository.GetAllAsync(spec);

            return await _repository.GetByPagingAsync(source, param.Start, param.Length);
        }

        public async Task<IReadOnlyList<Menu>> GetAllAsync()
        {
            var spec = new MenuSpesification();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<Menu> GetAsync(Guid id)
        {
            var spec = new MenuSpesification();
            return await _repository.GetAsync(spec, id);
        }

        public async Task<Guid?> PostAsync(Menu model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.Name), model.Name }
            };

            if (!await _repository.IsExistDataAsync(where))
            {
                var data = await _repository.AddAsync(model);
                id = data.Id;
            }

            return id;
        }

        public async Task<Guid?> PutAsync(Menu model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.Name), model.Name }
            };

            var param = new ExistWithKeyModel
            {
                KeyName = nameof(model.Id),
                KeyValue = model.Id,
                FieldName = nameof(model.Name),
                FieldValue = model.Name,
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

            var menus = await _repository.GetAllAsync();
            var parentMenus = menus.Where(x => x.FkParentId == id).ToList();
            if (parentMenus != null)
            {
                foreach (var item in parentMenus)
                {
                    await _repository.DeleteAsync(item);
                }
            }

            var menuRoles = await _menuRoleRepository.GetAllAsync();
            var listMenuRole = menuRoles.Where(x => x.FkMenuId == id).ToList();
            if (listMenuRole != null)
            {
                foreach (var item in listMenuRole)
                {
                    await _menuRoleRepository.DeleteAsync(item);
                }
            }
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
            var source = await _repository.GetAllAsync();
            var result = source
                .Where(x => x.IsActive == true && x.FkParentId != null)
                .Select(x => new Select2Binding
                {
                    Id = x.Id,
                    Text = x.Name
                }).ToList();

            return result;
        }

        public async Task<Select2Result> BindingSelect2Async(Guid id)
        {
            var spec = new MenuSpesification();
            var data = await _repository.GetAsync(id);
            var text = string.Empty;

            if (data != null)
            {
                text = data.Name;
            }

            var source = await _repository.GetAllAsync(spec);
            var result = source
                .Where(x => x.IsActive == true && x.FkParentId != null)
                .Select(x => new Select2Binding
                {
                    Id = x.Id,
                    Text = x.Name
                }).ToList();

            return result.BindingSelect2Edit(id, text);
        }

        public async Task<IReadOnlyList<Select2Binding>> BindingSelect2ParentAsync()
        {
            var spec = new MenuSpesification();
            var source = await _repository.GetAllAsync(spec);
            var result = source
                .Where(x => x.IsActive == true && x.FkParentId == null)
                .Select(x => new Select2Binding
                {
                    Id = x.Id,
                    Text = x.Name
                }).ToList();

            return result;
        }

        public async Task<Select2Result> BindingSelect2ParentAsync(Guid id)
        {
            var spec = new MenuSpesification();
            var data = await _repository.GetAsync(id);
            var text = string.Empty;

            if (data != null)
            {
                text = data.Name;
            }

            var source = await _repository.GetAllAsync(spec);
            var result = source
                .Where(x => x.IsActive == true && x.FkParentId == null)
                .Select(x => new Select2Binding
                {
                    Id = x.Id,
                    Text = x.Name
                }).ToList();

            return result.BindingSelect2Edit(id, text);
        }
    }
}
