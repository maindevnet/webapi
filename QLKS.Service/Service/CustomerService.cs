using AutoMapper.QueryableExtensions;
using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using QLKS.Repository.Repository;
using QLKS.Service.IService;
using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.Conmon;
using QLKS.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QLKS.Service.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IInvoiceRepository _InvoiceRepository;
        private readonly IProductRepository _IProductRepository;
        private readonly IInvoiceDetailRepository _InvoiceDetailRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IInvoiceRepository invoiceRepository,
            IInvoiceDetailRepository invoiceDetailRepository,
            IProductRepository productRepository )
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _InvoiceRepository = invoiceRepository;
            _InvoiceDetailRepository = invoiceDetailRepository;
            _IProductRepository = productRepository;
        }
        public async Task<ResultMessage<bool>> Delete(int Id)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                var data = await _customerRepository.GetById(Id);
                _customerRepository.Delete(Id);
                await _InvoiceRepository.DeleteByExpression(x=>x.Customers_Id==Id);
                //Xoa sanr phaam cux ddi
                var data_invo = await _InvoiceRepository.GetByExpression(x=>x.Customers_Id==Id);
                var data_InvoDetail = await _InvoiceDetailRepository.GetListByExpression(x => x.Invoice_Id == data_invo.Id).ToListAsync();
                foreach (var item in data_InvoDetail)
                {
                    _InvoiceDetailRepository.Delete(item.Id);
                }
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

        public async Task<ResultMessage<bool>> Edit(CustomersInvoice Customers)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                //chinh sua khach hang //
                CustomersViewModel customersView = new CustomersViewModel()
                {
                    Address = Customers.Address,
                    DateOfBirth = Customers.DateOfBirth,
                    DateOfHire = Customers.DateOfHire,
                    Name = Customers.Name,
                    Passport = Customers.Passport,
                    Sex = Customers.Sex,
                    Status = Customers.Status,
                    Id = Customers.Id,
                };

                var data = await _customerRepository.GetById(Customers.Id);
                var model = AutoMapper.Mapper.Map<CustomersViewModel, Customer>(customersView, data);
                _customerRepository.Updated(model);
                //chinhr suwar hoa don 
                InvoiceViewModel invoiceView = new InvoiceViewModel()
                {
                    DateOfPayment = Customers.DateOfPayment,
                    NV_Id = Customers.NV_Id,
                    Price = Customers.Price,
                    Rooms_Id = Customers.Rooms_Id,
                    Customers_Id = model.Id,
                    Id = model.Id,
                   // InvoiceDetails = Customers.InvoiceDetails
                };
                var data_invo = await _InvoiceRepository.GetById(Customers.Hoadon_Id);
                var model_invo = AutoMapper.Mapper.Map<InvoiceViewModel, Invoice>(invoiceView, data_invo);
                _InvoiceRepository.Updated(model_invo);
                //Xoa sanr phaam cux ddi
                var data_InvoDetail = await _InvoiceDetailRepository.GetListByExpression(x => x.Invoice_Id == Customers.Hoadon_Id).ToListAsync();
                foreach (var item in data_InvoDetail)
                {
                    _InvoiceDetailRepository.Delete(item.Id);
                }
                //Them nhieu san pham vao trong phong//
                foreach (var item in Customers.InvoiceDetails)
                {
                    InvoiceDetailViewModel invoiceDetailView = new InvoiceDetailViewModel()
                    {
                        Amount = item.Amount,
                        DateService = item.DateService,
                        Invoice_Id = Customers.Hoadon_Id,
                        Product_Id = item.Product_Id,
                        Status = item.Status,
                        TotalAmount = item.TotalAmount,
                        Unit = item.Unit,
                        UnitPrice = item.UnitPrice
                    };
                    var modelDetailInvoice = AutoMapper.Mapper.Map<InvoiceDetailViewModel, InvoiceDetail>(invoiceDetailView);
                    _InvoiceDetailRepository.Created(modelDetailInvoice);
                }
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
            return result;
        }

        public async Task<Pageding<CustomersViewModel>> GetAll(Serachmodel serachmodel)
        {
            Pageding<CustomersViewModel> result = new Pageding<CustomersViewModel>();
            try
            {
                var query = _customerRepository.GetAll().Project().To<CustomersViewModel>();
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
                result.Items = await query.ToListAsync<CustomersViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultMessage<bool>> Add(CustomersInvoice Customers)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            try
            {
                //Them moi khach hang //
                CustomersViewModel customersView = new CustomersViewModel()
                {
                    Address = Customers.Address,
                    DateOfBirth = Customers.DateOfBirth,
                    DateOfHire = Customers.DateOfHire,
                    Name = Customers.Name,
                    Passport = Customers.Passport,
                    Sex = Customers.Sex,
                    Status = Customers.Status
                };
                var model = AutoMapper.Mapper.Map<CustomersViewModel, Customer>(customersView);
                _customerRepository.Created(model);
                await _unitOfWork.CommitAsync();
                //Them moi hoa don 
                InvoiceViewModel invoiceView = new InvoiceViewModel()
                {
                    DateOfPayment = Customers.DateOfPayment,
                    NV_Id = Customers.NV_Id,
                    Price = Customers.Price,
                    Rooms_Id = Customers.Rooms_Id,
                    Customers_Id = model.Id
                };
                var modelInvoice = AutoMapper.Mapper.Map<InvoiceViewModel, Invoice>(invoiceView);
                _InvoiceRepository.Created(modelInvoice);
                //Them nhieu san pham vao trong phong//
                foreach (var item in Customers.InvoiceDetails)
                {
                    InvoiceDetailViewModel invoiceDetailView = new InvoiceDetailViewModel()
                    {
                        Amount = item.Amount,
                        DateService = item.DateService,
                        Invoice_Id = modelInvoice.Id,
                        Product_Id = item.Product_Id,
                        Status = item.Status,
                        TotalAmount = item.TotalAmount,
                        Unit = item.Unit,
                        UnitPrice = item.UnitPrice
                    };
                     var modelDetailInvoice = AutoMapper.Mapper.Map<InvoiceDetailViewModel, InvoiceDetail>(invoiceDetailView);
                    _InvoiceDetailRepository.Created(modelDetailInvoice);
                }
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

        public async Task<ResultMessage<CustomersInvoice>> GetById(int Id)
        {
            ResultMessage<CustomersInvoice> result = new ResultMessage<CustomersInvoice>();
            try
            {
                result.Result = new CustomersInvoice();

                var data = await _customerRepository.GetById(Id);
                var data_Ince = await _InvoiceRepository.GetByExpression(x=>x.Customers_Id==data.Id);
                var data_InceDetail = await _InvoiceDetailRepository.GetListByExpression(x => x.Invoice_Id == data_Ince.Id).ToListAsync();
                var modelInvoice = AutoMapper.Mapper.Map<List<InvoiceDetail>, List<InvoiceDetailViewModel>>(data_InceDetail);

                //Khach hangf
                result.Result.Id = data.Id;
                result.Result.Name = data.Name;
                result.Result.DateOfBirth = data.DateOfBirth.Value;
               
                result.Result.Sex = data.Sex;
                result.Result.Address = data.Address;
                result.Result.Passport = data.Passport;
                result.Result.DateOfHire = data.DateOfHire;
                
                result.Result.Status = data.Status;
                //Hoa don
                result.Result.Hoadon_Id = data_Ince.Id;
                result.Result.NV_Id = data_Ince.NV_Id;
                result.Result.Price = data_Ince.Price;
                result.Result.DateOfPayment = data_Ince.DateOfPayment;
               
                result.Result.Rooms_Id = data_Ince.Rooms_Id;
                // Chi tieets hoa don
                result.Result.InvoiceDetails = modelInvoice;
                result.MessageType = true;
                result.Message = Notify.LOAD_SUCCESS;
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
                var data = await _customerRepository.Contains(x => x.Name == name);
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