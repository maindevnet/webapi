using AutoMapper.QueryableExtensions;
using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using QLKS.Service.IService;
using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.Conmon;
using QLKS.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS.Service.Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _RoomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IRoomRepository RoomRepository, IUnitOfWork unitOfWork)
        {
            _RoomRepository = RoomRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultMessage<bool>> Delete(int Id)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                _RoomRepository.Delete(Id);
                await _unitOfWork.CommitAsync();

                result.MessageType = true;
                result.Message = Notify.DELETE_SUCCESS;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Edit(RoomsViewModel Rooms)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await _RoomRepository.GetById(Rooms.Id);
                var model = AutoMapper.Mapper.Map<RoomsViewModel, Room>(Rooms, data);
                _RoomRepository.Updated(model);
                await _unitOfWork.CommitAsync();

                result.MessageType = true;
                result.Message = Notify.EDIT_SUCCESS;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<Pageding<RoomsViewModel>> GetAll(Serachmodel serachmodel)
        {
            Pageding<RoomsViewModel> result = new Pageding<RoomsViewModel>();
            try
            {
                var query = _RoomRepository.GetAll().Project().To<RoomsViewModel>();
                if (!string.IsNullOrEmpty(serachmodel.keyword))
                {
                    query = query.Where(x => x.Name.Contains(serachmodel.keyword));
                }
                result.TotalPage = await query.CountAsync();
                result.TotalPage = (int)Math.Ceiling((result.TotalPage * 1.0 / serachmodel.pagesize));
                //Phan phaan trang
                query = query.OrderByDescending(x => x.Id).Skip(serachmodel.pagesize * (serachmodel.pageindex - 1)).
                    Take(serachmodel.pagesize);

                result.MessageType = true;
                result.Message = Notify.LOAD_SUCCESS;
                result.Items = await query.ToListAsync<RoomsViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Add(RoomsViewModel Rooms)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var model = AutoMapper.Mapper.Map<RoomsViewModel, Room>(Rooms);
                _RoomRepository.Created(model);
                await _unitOfWork.CommitAsync();

                result.MessageType = true;
                result.Message = Notify.ADD_SUCCESS;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<RoomsViewModel>> GetById(int Id)
        {
            ResultMessage<RoomsViewModel> result = new ResultMessage<RoomsViewModel>();
            try
            {
                var data = await _RoomRepository.GetById(Id);
                var model = AutoMapper.Mapper.Map<Room, RoomsViewModel>(data);

                result.MessageType = true;
                result.Message = Notify.LOAD_SUCCESS;
                result.Result = model;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> CheckName(string name)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await _RoomRepository.Contains(x => x.Name == name);
                if (data)
                {
                    result.MessageType = true;
                    result.Message = Notify.DUPLICATE_DATA;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
