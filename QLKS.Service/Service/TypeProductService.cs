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
    public class TypeProductService : ITypeProductService
    {
        private readonly ITypeProductRepository _TypeProductRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TypeProductService(ITypeProductRepository TypeProductRepository, IUnitOfWork unitOfWork)
        {
            _TypeProductRepository = TypeProductRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultMessage<bool>> Delete(int Id)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                _TypeProductRepository.Delete(Id);
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

        public async Task<ResultMessage<bool>> Edit(TypeProductsViewModel TypeProducts)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await _TypeProductRepository.GetById(TypeProducts.Id);
                var model = AutoMapper.Mapper.Map<TypeProductsViewModel, TypeProduct>(TypeProducts, data);
                _TypeProductRepository.Updated(model);
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

        public async Task<Pageding<TypeProductsViewModel>> GetAll(Serachmodel serachmodel)
        {
            Pageding<TypeProductsViewModel> result = new Pageding<TypeProductsViewModel>();
            try
            {
                var query = _TypeProductRepository.GetAll().Project().To<TypeProductsViewModel>();
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
                result.Items = await query.ToListAsync<TypeProductsViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Add(TypeProductsViewModel TypeProducts)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var model = AutoMapper.Mapper.Map<TypeProductsViewModel, TypeProduct>(TypeProducts);
                _TypeProductRepository.Created(model);
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

        public async Task<ResultMessage<TypeProductsViewModel>> GetById(int Id)
        {
            ResultMessage<TypeProductsViewModel> result = new ResultMessage<TypeProductsViewModel>();
            try
            {
                var data = await _TypeProductRepository.GetById(Id);
                var model = AutoMapper.Mapper.Map<TypeProduct, TypeProductsViewModel>(data);

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
                var data = await _TypeProductRepository.Contains(x => x.Name == name);
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