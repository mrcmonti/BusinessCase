using AutoMapper;
using ParanaBanco.Application.DTO;
using ParanaBanco.Domain.Entities.Customers;
using ParanaBanco.Domain.Entities.Phones;

namespace ParanaBanco.Application.Configurations
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CustomerDTO, Customer>().ReverseMap();
                config.CreateMap<AddCustomerDTO, Customer>().ReverseMap();
                config.CreateMap<PhoneDTO, Phone>().ReverseMap();
                config.CreateMap<AddPhoneDTO, Phone>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
