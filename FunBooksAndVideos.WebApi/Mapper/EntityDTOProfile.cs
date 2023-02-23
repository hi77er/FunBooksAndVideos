using AutoMapper;
using FunBooksAndVideos.DTOs;
using Entity = FunBooksAndVideos.DAL.Entities;

namespace FunBooksAndVideos.WebApi.Mapper
{
    public class EntityDTOProfile : Profile
    {
        public EntityDTOProfile()
        {
            CreateMap<Entity.PurchaseOrder, PurchaseOrder>().ReverseMap();
            CreateMap<Entity.PurchaseOrderItem, PurchaseOrderItem>().ReverseMap();

            //CreateMap<Entity.PurchaseOrder, PurchaseOrder>()
            //    .ForMember(d => d.PurchaseOrderItems, s => s.MapFrom(m => m.PurchaseOrderItems));
            //CreateMap<PurchaseOrder, Entity.PurchaseOrder>()
            //    .ForMember(d => d.PurchaseOrderItems, s => s.MapFrom(m => m.PurchaseOrderItems));

            CreateMap<Entity.Customer, Customer>().ReverseMap();
            CreateMap<Entity.ShippingSlip, PurchaseOrder>().ReverseMap();
            CreateMap<Entity.Membership, Membership>().ReverseMap();
            CreateMap<Entity.ItemAttribute, ItemAttribute>().ReverseMap();

            CreateMap<Entity.Item, Item>().ReverseMap();
            //CreateMap<Entity.Item, Item>()
            //    .ForMember(d => d.Attributes, s => s.MapFrom(m => m.Attributes));
            //CreateMap<Item, Entity.Item>()
            //    .ForMember(d => d.Attributes, s => s.MapFrom(m => m.Attributes));


        }
    }
}
