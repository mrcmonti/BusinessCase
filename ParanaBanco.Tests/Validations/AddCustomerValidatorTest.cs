using ParanaBanco.Application.DTO;
using ParanaBanco.Application.DTO.Request;
using ParanaBanco.Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Tests.Validations
{
    public class AddCustomerValidatorTest
    {
        private readonly AddCustomerValidator _validator;

        public AddCustomerValidatorTest()
        {
            _validator = new AddCustomerValidator();
        }


        [Fact(DisplayName = "Should return Success when data is valid")]
        [Trait("Validations", "AddCustomerValidatorTest")]
        public void ShouldReturnSuccessWhenValidData()
        {
            var request = new AddCustomerRequest()
            {
                FullName = "Test",
                EmailAddress = "teste@live.com",
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

            var result = _validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Should return Failure when full name is invalid")]
        [Trait("Validations", "AddCustomerValidatorTest")]
        public void ShouldReturnFailureWhenFullNameIsInvalid()
        {
            var request = new AddCustomerRequest()
            {
                FullName = "",
                EmailAddress = "teste@live.com",
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

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, nameof(request.FullName));
        }

        [Fact(DisplayName = "Should return Failure when email address is invalid")]
        [Trait("Validations", "AddCustomerValidatorTest")]
        public void ShouldReturnFailureWhenEmailAddressIsInvalid()
        {
            var request = new AddCustomerRequest()
            {
                FullName = "Mauricio Monti",
                EmailAddress = "teste",
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

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, nameof(request.EmailAddress));
        }

        [Fact(DisplayName = "Should return Failure when phone area code is invalid")]
        [Trait("Validations", "AddCustomerValidatorTest")]
        public void ShouldReturnFailureWhenAnyPhoneAreaCodeIsInvalid()
        {
            var request = new AddCustomerRequest()
            {
                FullName = "Mauricio Monti",
                EmailAddress = "teste@live.com",
                Phones = new[]
                {
                    new AddPhoneDTO
                    {
                        AreaCode = "",
                        PhoneNumber = "997241000",
                        Type = "Mobile"
                    }
                }
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName.Split('.')[1], nameof(AddPhoneDTO.AreaCode));
        }

        [Fact(DisplayName = "Should return Failure when phone number is invalid")]
        [Trait("Validations", "AddCustomerValidatorTest")]
        public void ShouldReturnFailureWhenAnyPhoneNumberIsInvalid()
        {
            var request = new AddCustomerRequest()
            {
                FullName = "Mauricio Monti",
                EmailAddress = "teste@live.com",
                Phones = new[]
                {
                    new AddPhoneDTO
                    {
                        AreaCode = "18",
                        PhoneNumber = "997241",
                        Type = "Mobile"
                    }
                }
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName.Split('.')[1], nameof(AddPhoneDTO.PhoneNumber));
        }

        [Fact(DisplayName = "Should return Failure when phone type is invalid")]
        [Trait("Validations", "AddCustomerValidatorTest")]
        public void ShouldReturnFailureWhenAnyPhoneTypeInvalid()
        {
            var request = new AddCustomerRequest()
            {
                FullName = "Mauricio Monti",
                EmailAddress = "teste@live.com",
                Phones = new[]
                {
                    new AddPhoneDTO
                    {
                        AreaCode = "18",
                        PhoneNumber = "997241000",
                        Type = "Invalid"
                    }
                }
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName.Split('.')[1], nameof(AddPhoneDTO.Type));
        }

    }
}
