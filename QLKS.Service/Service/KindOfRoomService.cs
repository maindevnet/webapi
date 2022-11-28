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
using System.Threading.Tasks;

namespace QLKS.Service.Service
{
    public class KindOfRoomService : IKindOfRoomService
    {
        private readonly IKindOfRoomRepository _kindOfRoomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public KindOfRoomService(IKindOfRoomRepository kindOfRoomRepository, IUnitOfWork unitOfWork)
        {
            _kindOfRoomRepository = kindOfRoomRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultMessage<bool>> Delete(int Id)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                _kindOfRoomRepository.Delete(Id);
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

        public async Task<ResultMessage<bool>> Edit(KindOfRoomsViewModel kindOfRooms)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await  _kindOfRoomRepository.GetById(kindOfRooms.Id);
                var model = AutoMapper.Mapper.Map<KindOfRoomsViewModel, KindOfRoom>(kindOfRooms, data);
                _kindOfRoomRepository.Updated(model);
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

        public async Task<Pageding<KindOfRoomsViewModel>> GetAll(Serachmodel serachmodel)
        {
            Pageding<KindOfRoomsViewModel> result = new Pageding<KindOfRoomsViewModel>();
            try
            {
                var query = _kindOfRoomRepository.GetAll().Project().To<KindOfRoomsViewModel>();
                if(!string.IsNullOrEmpty(serachmodel.keyword))
                {
                    query = query.Where(x => x.Name.Contains(serachmodel.keyword));
                }    
                result.TotalPage = await query.CountAsync();
                result.TotalPage = (int)Math.Ceiling((result.TotalPage*1.0 / serachmodel.pagesize));
                //Phan phaan trang
                query = query.OrderByDescending(x => x.Id).Skip(serachmodel.pagesize * (serachmodel.pageindex - 1)).
                    Take(serachmodel.pagesize);

                result.MessageType = true;
                result.Message =Notify.LOAD_SUCCESS;
                result.Items = await query.ToListAsync<KindOfRoomsViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Add(KindOfRoomsViewModel kindOfRooms)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var model = AutoMapper.Mapper.Map<KindOfRoomsViewModel, KindOfRoom>(kindOfRooms);
                _kindOfRoomRepository.Created(model);
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

        public async Task<ResultMessage<KindOfRoomsViewModel>> GetById(int Id)
        {
            ResultMessage<KindOfRoomsViewModel> result = new ResultMessage<KindOfRoomsViewModel>();
            try
            {
                var data = await _kindOfRoomRepository.GetById(Id);
                var model = AutoMapper.Mapper.Map<KindOfRoom, KindOfRoomsViewModel>(data);
               
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
                var data = await _kindOfRoomRepository.Contains(x=>x.Name==name);
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