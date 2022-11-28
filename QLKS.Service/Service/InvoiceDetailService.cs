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
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly IInvoiceDetailRepository _InvoiceDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceDetailService(IInvoiceDetailRepository InvoiceDetailRepository, 
            IUnitOfWork unitOfWork)
        {
            _InvoiceDetailRepository = InvoiceDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultMessage<bool>> Delete(int Id)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                _InvoiceDetailRepository.Delete(Id);
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

        public async Task<ResultMessage<bool>> Edit(InvoiceDetailViewModel InvoiceDetails)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await _InvoiceDetailRepository.GetById(InvoiceDetails.Id);
                var model = AutoMapper.Mapper.Map<InvoiceDetailViewModel, InvoiceDetail>(InvoiceDetails, data);
                _InvoiceDetailRepository.Updated(model);
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

        public async Task<Pageding<InvoiceDetailViewModel>> GetAll(Serachmodel serachmodel)
        {
            Pageding<InvoiceDetailViewModel> result = new Pageding<InvoiceDetailViewModel>();
            try
            {
                var query = _InvoiceDetailRepository.GetAll().Project().To<InvoiceDetailViewModel>();

                result.TotalPage = await query.CountAsync();
                result.TotalPage = (int)Math.Ceiling((result.TotalPage * 1.0 / serachmodel.pagesize));
                //Phan phaan trang
                query = query.OrderByDescending(x => x.Id).Skip(serachmodel.pagesize * (serachmodel.pageindex - 1)).
                    Take(serachmodel.pagesize);

                result.MessageType = true;
                result.Message = Notify.LOAD_SUCCESS;
                result.Items = await query.ToListAsync<InvoiceDetailViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Add(InvoiceDetailViewModel InvoiceDetails)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var model = AutoMapper.Mapper.Map<InvoiceDetailViewModel, InvoiceDetail>(InvoiceDetails);
                _InvoiceDetailRepository.Created(model);
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

        public async Task<ResultMessage<InvoiceDetailViewModel>> GetById(int Id)
        {
            ResultMessage<InvoiceDetailViewModel> result = new ResultMessage<InvoiceDetailViewModel>();
            try
            {
                var data = await _InvoiceDetailRepository.GetById(Id);
                var model = AutoMapper.Mapper.Map<InvoiceDetail, InvoiceDetailViewModel>(data);

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
    }
}