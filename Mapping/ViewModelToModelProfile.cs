using AutoMapper;
using TestAPI.Models;
using TestAPI.ViewModels;

namespace TestAPI.Mapping
{
    public class ViewModelToModelProfile : Profile
    {
        public ViewModelToModelProfile()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<PurchaseOrderViewModel, PurchaseOrder>();
            CreateMap<OrderDetailViewModel, OrderDetail>();
        }
    }
}
