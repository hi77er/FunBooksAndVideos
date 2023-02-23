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
            CreateMap<Entity.Customer, Customer>().ReverseMap();
            CreateMap<Entity.ShippingSlip, PurchaseOrder>().ReverseMap();
            CreateMap<Entity.Membership, Membership>().ReverseMap();
            CreateMap<Entity.ItemAttribute, ItemAttribute>().ReverseMap();
            CreateMap<Entity.Item, Item>().ReverseMap();
        }
    }
}
