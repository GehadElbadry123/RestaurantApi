using AutoMapper;
using Restaurant_API.DTOS.CategoryDTO;
using Restaurant_API.DTOS.OrderDTO;
using Restaurant_API.DTOS.OrderItemDTO;
using Restaurant_API.DTOS.ProductsDTO;
using Restaurant_API.Models;

namespace Restaurant_API.MapperConfig
{
    public class mappconfig:Profile
    {
        public mappconfig()
        {
            CreateMap<Product, ReadProduct>().AfterMap((src, dest) =>
            {
                dest.CategoryName = src.Category?.Name;

            }).ReverseMap();


            CreateMap<Category, ReadCategory>().AfterMap((src, dest) =>
            {
                dest.numofProducts = src.Products.Count();

            }).ReverseMap();

            CreateMap<Order, ReadOrder>().AfterMap((src, dest) =>
            {
                dest.UserName = src.User?.Name;

            }).ReverseMap();

            CreateMap<OrderItem, ReadOrderItem>().AfterMap((src, dest) =>
            {
                dest.ProductName = src.Product?.Name;

            }).ReverseMap();

        }
    }
}
