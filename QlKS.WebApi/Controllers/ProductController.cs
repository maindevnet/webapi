using QLKS.Service.IService;
using QLKS.Service.Service;
using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;
using System;
using System.Web.Http;

namespace QlKS.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IHttpActionResult> GetAll([FromUri] Serachmodel serachmodel)
        {
            var result = await _productService.GetAll(serachmodel);
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
            var result = await _productService.GetById(Id);
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
                var result = await _productService.CheckName(name);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add(ProductsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Add(viewModel);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IHttpActionResult> Edit(ProductsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Edit(viewModel);
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
            var result = await _productService.Delete(id);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}