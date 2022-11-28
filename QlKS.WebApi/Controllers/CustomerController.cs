using QLKS.Service.IService;
using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace QlKS.WebApi.Controllers
{
    [RoutePrefix("api/customer")]
    [Authorize]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IHttpActionResult> GetAll([FromUri] Serachmodel serachmodel)
        {
            var result = await _customerService.GetAll(serachmodel);
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
            var result = await _customerService.GetById(Id);
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
                var result = await _customerService.CheckName(name);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add(CustomersInvoice viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _customerService.Add(viewModel);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IHttpActionResult> Edit(CustomersInvoice viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _customerService.Edit(viewModel);
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
            var result = await _customerService.Delete(id);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}