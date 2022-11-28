using AutoMapper.QueryableExtensions;
using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using QLKS.Service.IService;
using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.Conmon;
using QLKS.Utilities.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QLKS.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository UserRepository, IUnitOfWork unitOfWork)
        {
            _UserRepository = UserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultMessage<bool>> Delete(int Id)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                _UserRepository.Delete(Id);
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

        public async Task<ResultMessage<bool>> Edit(UsersViewModel Users)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await _UserRepository.GetById(Users.Id);
                var model = AutoMapper.Mapper.Map<UsersViewModel, User>(Users, data);
                _UserRepository.Updated(model);
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

        public async Task<Pageding<UsersViewModel>> GetAll(Serachmodel serachmodel)
        {
            Pageding<UsersViewModel> result = new Pageding<UsersViewModel>();
            try
            {
                var query = _UserRepository.GetAll().Project().To<UsersViewModel>();
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
                result.Items = await query.ToListAsync<UsersViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Add(UsersViewModel Users)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var model = AutoMapper.Mapper.Map<UsersViewModel, User>(Users);
                _UserRepository.Created(model);
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

        public async Task<ResultMessage<UsersViewModel>> GetById(int Id)
        {
            ResultMessage<UsersViewModel> result = new ResultMessage<UsersViewModel>();
            try
            {
                var data = await _UserRepository.GetById(Id);
                var model = AutoMapper.Mapper.Map<User, UsersViewModel>(data);

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
                var data = await _UserRepository.Contains(x => x.Name == name);
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

        /// <summary>
        /// Hàm login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ResultMessage<UsersViewModel>> Login(string userName, string password)
        {
            ResultMessage<UsersViewModel> result = new ResultMessage<UsersViewModel>();
            try
            {
                var data = await _UserRepository.Contains(x => x.Name == userName && x.Password == password);
                if (data)
                {
                    result.MessageType = true;
                    result.Message = Notify.LOGIN_SUCCESS;
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