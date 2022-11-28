using QLKS.Service.IService;
using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;
using System;
using System.Web.Http;

namespace QlKS.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/typeproduct")]
    public class TypeProductController : ApiController
    {
        private readonly ITypeProductService _typeProductService;
        public TypeProductController(ITypeProductService typeProductService)
        {
            _typeProductService = typeProductService;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IHttpActionResult> GetAll([FromUri] Serachmodel serachmodel)
        {
            var result = await _typeProductService.GetAll(serachmodel);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet]
        [Route("getbyid/{Id}")]
        public async Task<IHttpActionResult> GetById([FromUri] int Id)
        {
            var result = await _typeProductService.GetById(Id);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet]
        [Route("checkname/{name}")]
        public async Task<IHttpActionResult> CheckName(string name)
        {
            try
            {
                var result = await _typeProductService.CheckName(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add(TypeProductsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _typeProductService.Add(viewModel);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IHttpActionResult> Edit(TypeProductsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _typeProductService.Edit(viewModel);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id is 0)
            {
                return BadRequest("Không tồn tại dữ liệu!");
            }
            var result = await _typeProductService.Delete(id);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}