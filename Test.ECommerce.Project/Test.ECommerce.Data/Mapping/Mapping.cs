using AutoMapper;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductContract>().ReverseMap();
        }
    }
}