using AutoMapper;
using QLKS.Data.EF;
using QLKS.Utilities.ViewModel;

namespace QLKS.Service.Map
{
    public class ViewMapEntity
    {
        public static void Mapping()
        {
            Mapper.CreateMap<CustomersViewModel, Customer>();
            Mapper.CreateMap<InvoiceViewModel, Invoice>();
            Mapper.CreateMap<InvoiceDetailViewModel, InvoiceDetail>();
            Mapper.CreateMap<KindOfRoomsViewModel, KindOfRoom>();
            Mapper.CreateMap<ProductsViewModel, Product>();
            Mapper.CreateMap<RoomsViewModel,Room>();
            Mapper.CreateMap<TypeProductsViewModel, TypeProduct>();
            Mapper.CreateMap<UsersViewModel, User>();
        }
    }
}