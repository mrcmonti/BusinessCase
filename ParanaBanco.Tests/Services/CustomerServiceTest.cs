using AutoMapper;
using Castle.Core.Resource;
using Moq;
using ParanaBanco.Application.Configurations;
using ParanaBanco.Application.DTO;
using ParanaBanco.Application.DTO.Request;
using ParanaBanco.Application.Services;
using ParanaBanco.Domain.Entities.Customers;
using ParanaBanco.Domain.Entities.Phones;
using ParanaBanco.Domain.Enums;
using ParanaBanco.Domain.Interfaces.Repositories;
using System.Runtime.InteropServices;

namespace ParanaBanco.Tests.Services
{
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IPhoneRepository> _mockPhoneRepository;
        private CustomerService _service;
        private readonly IMapper _mapper;
        private Mock<IServiceProvider> _serviceProvider;

        public CustomerServiceTest()
        {
            _mockCustomerRepository = new();
            _mockPhoneRepository = new Mock<IPhoneRepository>();
            _serviceProvider = new Mock<IServiceProvider>();
            _mapper = MapperConfig.RegisterMaps().CreateMapper();
        }

        [Fact(DisplayName = "Should return Success when add new customer")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldAddNewCustomer()
        {
            var request = new AddCustomerRequest()
            {
                EmailAddress = "teste@live.com",
                FullName = "Mauricio Monti",
                Phones = new[]
                {
                    new AddPhoneDTO
                    {
                        AreaCode = "18",
                        PhoneNumber = "997241000",
                        Type = "Mobile"
                    }
                }
            };

            _mockCustomerRepository
                .Setup(x => x.GetByEmailAddress(request.EmailAddress))
                .ReturnsAsync((Customer)null);

            var entity = _mapper.Map<Customer>(request);
            entity.Id = Guid.NewGuid();

            _mockCustomerRepository
                .Setup(x => x.InsertAsync(It.IsAny<Customer>()))
                .ReturnsAsync(entity);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.Add(request);

            Assert.True(result.Success);
            Assert.True(result.ValidationResult.IsValid);
            Assert.NotNull(result.Customer);
            Assert.Equal(request.FullName, result.Customer.FullName);
            Assert.Equal(request.EmailAddress, result.Customer.EmailAddress);
        }

        [Fact(DisplayName = "Should return Success when get all customers")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldGetAllCustomer()
        {
            var customerId = Guid.NewGuid();

            var customers = new List<Customer>()
            {
                new Customer
                {
                    Id = customerId,
                    FullName = "Name 1",
                    EmailAddress = "test@live.com",
                    Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "997241000",
                            CustomerId = customerId,
                            Type = PhoneType.Mobile
                        }
                    }
                },
                new Customer
                {
                    Id = customerId,
                    FullName = "Name 2",
                    EmailAddress = "test2@live.com",
                    Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "33333333",
                            CustomerId = customerId,
                            Type = PhoneType.Landline
                        }
                    }
                }
            };

            _mockCustomerRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(customers);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.GetAll();

            Assert.True(result.Success);
            Assert.True(result.Customers.Any());
            Assert.True(result.Customers.Count() == 2);
        }


        [Fact(DisplayName = "Should return Success when get customer by phone")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldGetCustomerByPhone()
        {
            var phone = "18997241000";
            var customerId = Guid.NewGuid();

            var customer = new Customer
            {
                Id = customerId,
                FullName = "Name 1",
                EmailAddress = "test@live.com",
                Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "997241000",
                            CustomerId = customerId,
                            Type = PhoneType.Mobile
                        }
                    }
            };

            _mockCustomerRepository
                .Setup(x => x.GetByPhone(phone))
                .ReturnsAsync(customer);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.GetByPhone(phone);

            Assert.True(result.Success);
            Assert.NotNull(result.Customer);
        }

        [Fact(DisplayName = "Should return Success when update customer email address")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldUpdateEmailAddress()
        {
            var customerId = Guid.NewGuid();

            var request = new UpdateCustomerEmailAddressRequest()
            {
                Id = customerId,
                EmailAddress = "test@live.com"
            };

            
            var customer = new Customer
            {
                Id = customerId,
                FullName = "Name 1",
                EmailAddress = "test@live.com",
                Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "997241000",
                            CustomerId = customerId,
                            Type = PhoneType.Mobile
                        }
                    }
            };

            _mockCustomerRepository
                .Setup(x => x.SelectAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            _mockCustomerRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.UpdateEmailAddress(request);

            Assert.True(result.Success);
            Assert.NotNull(result.Customer);
            Assert.True(String.IsNullOrEmpty(result.ErrorCode));
            Assert.Equal(customer.Id, result.Customer.Id);
        }

        [Fact(DisplayName = "Should return Success when update phone")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldUpdatePhone()
        {
            var customerId = Guid.NewGuid();
            var phoneId = Guid.NewGuid();

            var request = new UpdateCustomerPhoneRequest
            {
                Id = customerId,
                Phone = new PhoneDTO
                {
                    Id = phoneId,
                    AreaCode = "18",
                    PhoneNumber = "997245000",
                    Type = "Mobile",
                }
            };

            var customer = new Customer
            {
                Id = customerId,
                FullName = "Name 1",
                EmailAddress = "test@live.com",
                Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "997245000",
                            CustomerId = customerId,
                            Type = PhoneType.Mobile
                        }
                    }
            };

            _mockCustomerRepository
                .Setup(x => x.SelectAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            _mockCustomerRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            _serviceProvider
                .Setup(provider => provider.GetService(typeof(IPhoneRepository)))
                .Returns(_mockPhoneRepository.Object);

            _mockPhoneRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Phone>()))
                .ReturnsAsync(customer.Phones.ToList()[0]);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.UpdatePhone(request);

            Assert.True(result.Success);
            Assert.NotNull(result.Phone);
            Assert.True(String.IsNullOrEmpty(result.ErrorCode));
            Assert.Equal(request.Phone.Id, result.Phone.Id);
        }

        [Fact(DisplayName = "Should return Success when delete customer")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldDeleteCustomerByEmailAddress()
        {
            var emailAddress = "test@live.com";
            var customerId = Guid.NewGuid();

            var customer = new Customer
            {
                Id = customerId,
                FullName = "Name 1",
                EmailAddress = "test@live.com",
                Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "997245000",
                            CustomerId = customerId,
                            Type = PhoneType.Mobile
                        }
                    }
            };

            _mockCustomerRepository
                .Setup(x => x.GetByEmailAddress(It.IsAny<string>()))
                .ReturnsAsync(customer);

            _mockCustomerRepository
                .Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.Delete(emailAddress); 
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(String.IsNullOrEmpty(result.ErrorCode));
        }

        [Fact(DisplayName = "Should return Failure when add customer and same email address already exists")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldFailureWhenAddCustomerAndCustomerAlreadyExists()
        {
            var request = new AddCustomerRequest()
            {
                EmailAddress = "test@live.com",
                FullName = "Mauricio Monti",
                Phones = new[]
                {
                    new AddPhoneDTO
                    {
                        AreaCode = "18",
                        PhoneNumber = "997241000",
                        Type = "Mobile"
                    }
                }
            };

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FullName = "Name 1",
                EmailAddress = "test@live.com",
            };

            _mockCustomerRepository
                .Setup(x => x.GetByEmailAddress(request.EmailAddress))
                .ReturnsAsync(customer);

            var entity = _mapper.Map<Customer>(request);
            entity.Id = Guid.NewGuid();

            _mockCustomerRepository
                .Setup(x => x.InsertAsync(It.IsAny<Customer>()))
                .ReturnsAsync(entity);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.Add(request);

            Assert.False(result.Success);
            Assert.Equal(result.ErrorCode, nameof(ErrorCode.CUSTOMER_ALREADY_EXISTS));
        }

        [Fact(DisplayName = "Should return Failure when update email address and customer not found")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldFailureWhenUpdateEmailAddressAndCustomerNotFound()
        {
            var customerId = Guid.NewGuid();

            var request = new UpdateCustomerEmailAddressRequest()
            {
                Id = customerId,
                EmailAddress = "test@live.com"
            };


            var customer = new Customer
            {
                Id = customerId,
                FullName = "Name 1",
                EmailAddress = "test@live.com",
                Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "997241000",
                            CustomerId = customerId,
                            Type = PhoneType.Mobile
                        }
                    }
            };

            _mockCustomerRepository
                .Setup(x => x.SelectAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Customer)null);

            _mockCustomerRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.UpdateEmailAddress(request);

            Assert.False(result.Success);
            Assert.Equal(result.ErrorCode, nameof(ErrorCode.CUSTOMER_NOT_FOUND));
        }

        [Fact(DisplayName = "Should return Failure when update phone and phone not found")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldFailureWhenUpdatePhoneAndPhoneNotFound()
        {
            var customerId = Guid.NewGuid();
            var phoneId = Guid.NewGuid();

            var request = new UpdateCustomerPhoneRequest
            {
                Id = customerId,
                Phone = new PhoneDTO
                {
                    Id = phoneId,
                    AreaCode = "18",
                    PhoneNumber = "997245000",
                    Type = "Mobile",
                }
            };

            var customer = new Customer
            {
                Id = customerId,
                FullName = "Name 1",
                EmailAddress = "test@live.com",
                Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "997245000",
                            CustomerId = customerId,
                            Type = PhoneType.Mobile
                        }
                    }
            };

            _mockCustomerRepository
                .Setup(x => x.SelectAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            _mockCustomerRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            _serviceProvider
                .Setup(provider => provider.GetService(typeof(IPhoneRepository)))
                .Returns(_mockPhoneRepository.Object);

            _mockPhoneRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Phone>()))
                .ReturnsAsync((Phone)null);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.UpdatePhone(request);

            Assert.False(result.Success);
            Assert.Equal(result.ErrorCode, nameof(ErrorCode.PHONE_NOT_FOUND));
        }

        [Fact(DisplayName = "Should return Failure when delete customer by email and customer not found")]
        [Trait("Services", "CustomerServiceTest")]
        public async Task ShouldFailureWhenDeleteCustomerByEmailAddressAndCustomerNotFound()
        {
            var emailAddress = "test@live.com";
            var customerId = Guid.NewGuid();

            var customer = new Customer
            {
                Id = customerId,
                FullName = "Name 1",
                EmailAddress = "test@live.com",
                Phones = new[]
                    {
                        new Phone
                        {
                            Id = Guid.NewGuid(),
                            AreaCode = "18",
                            PhoneNumber = "997245000",
                            CustomerId = customerId,
                            Type = PhoneType.Mobile
                        }
                    }
            };

            _mockCustomerRepository
                .Setup(x => x.GetByEmailAddress(It.IsAny<string>()))
                .ReturnsAsync((Customer)null);

            _service = new CustomerService(_mockCustomerRepository.Object, _mapper, _serviceProvider.Object);

            var result = await _service.Delete(emailAddress);
            Assert.False(result.Success);
            Assert.Equal(result.ErrorCode, nameof(ErrorCode.CUSTOMER_NOT_FOUND));
        }
    }
}
