using ParanaBanco.Application.DTO.Request;
using ParanaBanco.Application.DTO.Response;

namespace ParanaBanco.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<GetAllCustomersResponse> GetAll();
        Task<GetCustomerResponse> GetByPhone(string phone);
        Task<AddCustomerResponse> Add(AddCustomerRequest request);
        Task<UpdateCustomerEmailAddressResponse> UpdateEmailAddress(UpdateCustomerEmailAddressRequest request);
        Task<DeleteCustomerResponse> Delete(string emailAddress);
        Task<UpdateCustomerPhoneResponse> UpdatePhone(UpdateCustomerPhoneRequest request);
    }
}
