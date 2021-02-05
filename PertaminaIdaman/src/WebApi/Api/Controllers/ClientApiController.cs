using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers;
using ApplicationCore.Helpers.Api.Response;
using ApplicationCore.Helpers.ApiResponse;
using ApplicationCore.Helpers.BaseEntity;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Interfaces.Config;
using ApplicationCore.Interfaces.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class ClientApiController : BaseApiController
    {
        private readonly IClientApiService _service;
        private readonly IAppLogger<ClientApi> _loger;
        private readonly IConfiguration _config;

        public ClientApiController(IConfiguration config, IClientApiService service, IAppLogger<ClientApi> loger)
        {
            _config = config;
            _service = service;
            _loger = loger;
        }

        [HttpPost("get/datatables")]
        public async Task<IActionResult> LoadDatatablesAsync([FromBody] DatatablesParameter param)
        {
            if (param == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Parameter is null", new { param });
                return error.ReturnResponse();
            }

            var data = await _service.DatatablesAsync(param);

            var items = new List<ClientApi>();

            foreach (var item in data.Items)
            {
                item.ClientSecret = item.ClientSecret.ToBase64EncodeWithKey(_config["Security:EncryptKey"]);
                items.Add(item);
            }
            data.Items = items.AsEnumerable();

            var result = new OK("Success load datatables", data);
            return result.ReturnResponse();
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _service.GetAllAsync();
            var result = new OK("Success get all data", data);
            return result.ReturnResponse();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await _service.GetAsync(id);
            data.ClientSecret = data.ClientSecret.ToBase64EncodeWithKey(_config["Security:EncryptKey"]);

            if (data == null)
            {
                _loger.LogError("Data not found for id : " + id.ToString());

                var error = new NotFound("Data not found for id : " + id.ToString(), new { id });
                return error.ReturnResponse();
            }

            var result = new OK("Success get data by id = " + id.ToString(), data);
            return result.ReturnResponse();
        }

        [HttpGet("get/param")]
        public async Task<IActionResult> GetAsync(DataParameter param)
        {
            if (param.Start <= 0 || param.Length <= 0)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Start page > 0 and page size > 0", new { param.Start, param.Length });
                return error.ReturnResponse();
            }

            var data = await _service.GetAsync(param);
            var result = new OK("Success get data by paging", data);
            return result.ReturnResponse();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ClientApi data)
        {
            if (data == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Parameter is null", new { data });
                return error.ReturnResponse();
            }

            var id = await _service.PostAsync(data);
            if (id == null)
            {
                _loger.LogError("Duplicate data for client id : " + data.ClientId);

                var error = new Forbidden("Duplicate data for client id : " + data.ClientId, new { data });
                return error.ReturnResponse();
            }

            var result = new OK("Success add data", new { id });
            return result.ReturnResponse();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ClientApi data)
        {
            if (data == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Parameter is null", new { data });
                return error.ReturnResponse();
            }

            var id = await _service.PutAsync(data);
            if (id == null)
            {
                _loger.LogError("Duplicate data for client id : " + data.ClientId);

                var error = new Forbidden("Duplicate data for client id : " + data.ClientId, new { data });
                return error.ReturnResponse();
            }

            var result = new OK("Success update data", new { id });
            return result.ReturnResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                _loger.LogError("Not found data for id : " + id.ToString());

                var error = new NotFound("Not found data for id : " + id.ToString(), new { id });
                return error.ReturnResponse();
            }

            await _service.DeleteAsync(id);
            var result = new OK("Success delete data " + data.Name, new { id });
            return result.ReturnResponse();
        }

        [HttpPost("exist")]
        public async Task<IActionResult> IsExistDataAsync([FromBody] IDictionary<string, object> whereData)
        {
            if (whereData == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Bad request parameter", new { whereData });
                return error.ReturnResponse();
            }

            var exist = await _service.IsExistDataAsync(whereData);

            var result = new OK("Success check exist data", new { exist });
            return result.ReturnResponse();
        }

        [HttpPost("exist/key")]
        public async Task<IActionResult> IsExistDataWithKeyAsync([FromBody] ExistWithKeyModel model)
        {
            if (string.IsNullOrEmpty(model.FieldName)
                || string.IsNullOrEmpty(model.FieldName))
            {
                _loger.LogError("Bad Request Parameter");

                var error = new BadRequest("Bad Request Parameter", new { model });
                return error.ReturnResponse();
            }
            else if (typeof(ClientApi).HasProperty(model.KeyName.ToLower()) && typeof(ClientApi).HasProperty(model.FieldName.ToLower()))
            {
                _loger.LogError("Property name does not exist");

                var error = new BadRequest("Property name does not exist", new { model });
                return error.ReturnResponse();
            }

            var exist = await _service.IsExistDataWithKeyAsync(model);

            var result = new OK("Success check exist data with key " + model.FieldName + " : " + model.FieldValue, new { exist });
            return result.ReturnResponse();
        }

        [HttpGet("binding/select2")]
        public async Task<IActionResult> BindingSelect2Async()
        {
            var data = await _service.BindingSelect2Async();

            var result = new OK("Success binding select2", data);

            return result.ReturnResponse();
        }

        [HttpGet("binding/select2/{id}")]
        public async Task<IActionResult> BindingSelect2Async(Guid id)
        {
            var data = await _service.BindingSelect2Async(id);

            var result = new OK("Success binding select2", data);
            return result.ReturnResponse();
        }
    }
}
