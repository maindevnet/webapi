using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using QLKS.Service.IService;
using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.Conmon;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;
using System;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Data.Entity;

namespace QLKS.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository ProductRepository, IUnitOfWork unitOfWork)
        {
            _ProductRepository = ProductRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultMessage<bool>> Delete(int Id)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                _ProductRepository.Delete(Id);
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

        public async Task<ResultMessage<bool>> Edit(ProductsViewModel Products)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await _ProductRepository.GetById(Products.Id);
                var model = AutoMapper.Mapper.Map<ProductsViewModel, Product>(Products, data);
                _ProductRepository.Updated(model);
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

        public async Task<Pageding<ProductsViewModel>> GetAll(Serachmodel serachmodel)
        {
            Pageding<ProductsViewModel> result = new Pageding<ProductsViewModel>();
            try
            {
                var query = _ProductRepository.GetAll().Project().To<ProductsViewModel>();
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
                result.Items = await query.ToListAsync<ProductsViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Add(ProductsViewModel Products)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var model = AutoMapper.Mapper.Map<ProductsViewModel, Product>(Products);
                _ProductRepository.Created(model);
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

        public async Task<ResultMessage<ProductsViewModel>> GetById(int Id)
        {
            ResultMessage<ProductsViewModel> result = new ResultMessage<ProductsViewModel>();
            try
            {
                var data = await _ProductRepository.GetById(Id);
                var model = AutoMapper.Mapper.Map<Product, ProductsViewModel>(data);

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
                var data = await _ProductRepository.Contains(x => x.Name == name);
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