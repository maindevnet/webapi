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
    public class InvoiceService: IInvoiceService
    {
        private readonly IInvoiceRepository _InvoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IInvoiceRepository InvoiceRepository, IUnitOfWork unitOfWork)
        {
            _InvoiceRepository = InvoiceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultMessage<bool>> Delete(int Id)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                _InvoiceRepository.Delete(Id);
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

        public async Task<ResultMessage<bool>> Edit(InvoiceViewModel Invoices)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await _InvoiceRepository.GetById(Invoices.Id);
                var model = AutoMapper.Mapper.Map<InvoiceViewModel, Invoice>(Invoices, data);
                _InvoiceRepository.Updated(model);
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

        public async Task<Pageding<InvoiceViewModel>> GetAll(Serachmodel serachmodel)
        {
            Pageding<InvoiceViewModel> result = new Pageding<InvoiceViewModel>();
            try
            {
                var query = _InvoiceRepository.GetAll().Project().To<InvoiceViewModel>();
                result.TotalPage = await query.CountAsync();
                result.TotalPage = (int)Math.Ceiling((result.TotalPage * 1.0 / serachmodel.pagesize));
                //Phan phaan trang
                query = query.OrderByDescending(x => x.Id).Skip(serachmodel.pagesize * (serachmodel.pageindex - 1)).
                    Take(serachmodel.pagesize);

                result.MessageType = true;
                result.Message = Notify.LOAD_SUCCESS;
                result.Items = await query.ToListAsync<InvoiceViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Add(InvoiceViewModel Invoices)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var model = AutoMapper.Mapper.Map<InvoiceViewModel, Invoice>(Invoices);
                _InvoiceRepository.Created(model);
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

        public async Task<ResultMessage<InvoiceViewModel>> GetById(int Id)
        {
            ResultMessage<InvoiceViewModel> result = new ResultMessage<InvoiceViewModel>();
            try
            {
                var data = await _InvoiceRepository.GetById(Id);
                var model = AutoMapper.Mapper.Map<Invoice, InvoiceViewModel>(data);

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