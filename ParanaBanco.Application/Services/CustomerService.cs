using AutoMapper;
using ParanaBanco.Application.DTO;
using ParanaBanco.Application.DTO.Request;
using ParanaBanco.Application.DTO.Response;
using ParanaBanco.Application.Interfaces.Services;
using ParanaBanco.Application.Validations;
using ParanaBanco.Domain.Entities.Customers;
using ParanaBanco.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ParanaBanco.Domain.Entities.Phones;
using ParanaBanco.Domain.Enums;

namespace ParanaBanco.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IServiceProvider _provider;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper, IServiceProvider provider)
        {
            _repository = repository;
            _mapper = mapper;
            _provider = provider;
        }

        public async Task<GetAllCustomersResponse> GetAll()
        {
            try
            {
                var response = new GetAllCustomersResponse();

                var customers = await _repository.GetAll();

                response.Customers = _mapper.Map<List<CustomerDTO>>(customers);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<GetCustomerResponse> GetByPhone(string phone)
        {
            try
            {
                var response = new GetCustomerResponse();

                var customer = await _repository.GetByPhone(phone);

                response.Customer = _mapper.Map<CustomerDTO>(customer);

                return response;
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public async Task<AddCustomerResponse> Add(AddCustomerRequest request)
        {
            try
            {
                var response = new AddCustomerResponse();
                var validator = new AddCustomerValidator();
                response.ValidationResult = validator.Validate(request);

                if (!response.ValidationResult.IsValid)
                    return response;

                var existingCustomer = await _repository.GetByEmailAddress(request.EmailAddress);

                if (existingCustomer != null)
                {
                    response.ErrorCode = nameof(ErrorCode.CUSTOMER_ALREADY_EXISTS);
                    response.Success = false;
                    return response;
                }

                var entity = _mapper.Map<Customer>(request);
                var newCustomer = await _repository.InsertAsync(entity);

                response.Customer = _mapper.Map<CustomerDTO>(newCustomer);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UpdateCustomerEmailAddressResponse> UpdateEmailAddress(UpdateCustomerEmailAddressRequest request)
        {
            try
            {
                var response = new UpdateCustomerEmailAddressResponse();
                var validator = new UpdateCustomerEmailAddressValidator();
                response.ValidationResult = validator.Validate(request);

                if (!response.ValidationResult.IsValid)
                    return response;

                var customer = await _repository.SelectAsync(request.Id);

                if (customer is null) 
                {
                    response.ErrorCode = nameof(ErrorCode.CUSTOMER_NOT_FOUND);
                    response.Success = false;
                    return response;
                }

                customer.ChangeEmailAddress(request.EmailAddress);

                var updatedCustomer = await _repository.UpdateAsync(customer);

                response.Customer = _mapper.Map<CustomerDTO>(updatedCustomer);

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DeleteCustomerResponse> Delete(string emailAddress)
        {
            try
            {
                var response = new DeleteCustomerResponse();

                var customer = await _repository.GetByEmailAddress(emailAddress: emailAddress);

                if (customer is null)
                {
                    response.ErrorCode = nameof(ErrorCode.CUSTOMER_NOT_FOUND);
                    response.Success = false;
                    return response;
                }

                var result = await _repository.DeleteAsync(customer.Id);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UpdateCustomerPhoneResponse> UpdatePhone(UpdateCustomerPhoneRequest request)
        {
            try
            {
                var response = new UpdateCustomerPhoneResponse();
                var validator = new UpdateCustomerPhoneValidator();

                response.ValidationResult = validator.Validate(request);

                if (!response.ValidationResult.IsValid)
                {
                    response.Success = false;
                    return response;
                }

                var customer = await _repository.SelectAsync(request.Id);

                if (customer is null)
                {
                    response.ErrorCode = nameof(ErrorCode.CUSTOMER_NOT_FOUND);
                    response.Success = false;
                    return response;
                }

                var repositoryPhone = _provider.GetService<IPhoneRepository>();
                var phone = _mapper.Map<Phone>(request.Phone);

                phone.CustomerId = request.Id;
                var phoneUpdated = await repositoryPhone.UpdateAsync(phone);

                if (phoneUpdated is null)
                {
                    response.Success = false;
                    response.ErrorCode = nameof(ErrorCode.PHONE_NOT_FOUND);
                    return response;
                }

                response.Phone = _mapper.Map<PhoneDTO>(phone);

                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
