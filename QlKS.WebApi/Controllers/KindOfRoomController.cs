using QLKS.Service.IService;
using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace QlKS.WebApi.Controllers
{
    [RoutePrefix("api/kindofroom")]
    [Authorize]
    public class KindOfRoomController : ApiController
    {
        private readonly IKindOfRoomService _kindOfRoomService;
        public KindOfRoomController(IKindOfRoomService kindOfRoomService)
        {
            _kindOfRoomService = kindOfRoomService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IHttpActionResult> GetAll([FromUri]Serachmodel serachmodel)
        {
            var result = await _kindOfRoomService.GetAll(serachmodel);
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
            var result = await _kindOfRoomService.GetById(Id);
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
                var result = await _kindOfRoomService.CheckName(name);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add(KindOfRoomsViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _kindOfRoomService.Add(viewModel);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IHttpActionResult> Edit(KindOfRoomsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _kindOfRoomService.Edit(viewModel);
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
            var result = await _kindOfRoomService.Delete(id);
            if (result.MessageType)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}