using AutoMapper;
using QLKS.Data.EF;
using QLKS.Utilities.ViewModel;

namespace QLKS.Service.Map
{
    public class EntityMapView
    {
        public static void Mapping()
        {
            Mapper.CreateMap<Customer, CustomersViewModel>();
            Mapper.CreateMap<Invoice, InvoiceViewModel>();
            Mapper.CreateMap<InvoiceDetail, InvoiceDetailViewModel>();
            Mapper.CreateMap<KindOfRoom, KindOfRoomsViewModel>();
            Mapper.CreateMap<Product, ProductsViewModel>();
            Mapper.CreateMap<Room, RoomsViewModel>();
            Mapper.CreateMap<TypeProduct, TypeProductsViewModel>();
            Mapper.CreateMap<User, UsersViewModel>();
        }
    }
}