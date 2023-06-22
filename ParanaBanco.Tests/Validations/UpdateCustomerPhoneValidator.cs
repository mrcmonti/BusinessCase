using ParanaBanco.Application.DTO.Request;
using ParanaBanco.Application.DTO;
using ParanaBanco.Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ParanaBanco.Tests.Validations
{
    public class UpdateCustomerPhoneValidatorTest
    {
        private readonly UpdateCustomerPhoneValidator _validator;

        public UpdateCustomerPhoneValidatorTest()
        {
            _validator = new UpdateCustomerPhoneValidator();
        }


        [Fact(DisplayName = "Should return Success when data is valid")]
        [Trait("Validations", "UpdateCustomerPhoneValidator")]
        public void ShouldReturnSuccessWhenValidData()
        {
            var request = new UpdateCustomerPhoneRequest()
            {
                Id = Guid.NewGuid(),
                Phone = new PhoneDTO
                {
                    Id = Guid.NewGuid(),
                    AreaCode = "18",
                    PhoneNumber = "997241000",
                    Type = "Mobile"
                }
            };

            var result = _validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Should return Failure when customer id is invalid")]
        [Trait("Validations", "UpdateCustomerPhoneValidator")]
        public void ShouldReturnFailureWhenIdIsInvalid()
        {
            var request = new UpdateCustomerPhoneRequest()
            {
                Phone = new PhoneDTO
                {
                    Id = Guid.NewGuid(),
                    AreaCode = "18",
                    PhoneNumber = "997241000",
                    Type = "Mobile"
                }
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, nameof(request.Id));
        }

        [Fact(DisplayName = "Should return Failure when phone id is invalid")]
        [Trait("Validations", "UpdateCustomerPhoneValidator")]
        public void ShouldReturnFailureWhenPhoneIdIsInvalid()
        {
            var request = new UpdateCustomerPhoneRequest()
            {
                Id = Guid.NewGuid(),
                Phone = new PhoneDTO
                {
                    AreaCode = "18",
                    PhoneNumber = "997241000",
                    Type = "Mobile"
                }
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, $"{nameof(request.Phone)}.{nameof(request.Phone.Id)}");
        }

        [Fact(DisplayName = "Should return Failure when phone area code is invalid")]
        [Trait("Validations", "UpdateCustomerPhoneValidator")]
        public void ShouldReturnFailureWhenPhoneAreaCodeIsInvalid()
        {
            var request = new UpdateCustomerPhoneRequest()
            {
                Id = Guid.NewGuid(),
                Phone = new PhoneDTO
                {
                    Id = Guid.NewGuid(),
                    AreaCode = "",
                    PhoneNumber = "997241000",
                    Type = "Mobile"
                }
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, $"{nameof(request.Phone)}.{nameof(request.Phone.AreaCode)}");
        }

        [Fact(DisplayName = "Should return Failure when phone number is invalid")]
        [Trait("Validations", "UpdateCustomerPhoneValidator")]
        public void ShouldReturnFailureWhenPhoneNumberIsInvalid()
        {
            var request = new UpdateCustomerPhoneRequest()
            {
                Id = Guid.NewGuid(),
                Phone = new PhoneDTO
                {
                    Id = Guid.NewGuid(),
                    AreaCode = "18",
                    PhoneNumber = "",
                    Type = "Mobile"
                }
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, $"{nameof(request.Phone)}.{nameof(request.Phone.PhoneNumber)}");
        }

        [Fact(DisplayName = "Should return Failure when phone type is invalid")]
        [Trait("Validations", "UpdateCustomerPhoneValidator")]
        public void ShouldReturnFailureWhenPhoneTypeIsInvalid()
        {
            var request = new UpdateCustomerPhoneRequest()
            {
                Id = Guid.NewGuid(),
                Phone = new PhoneDTO
                {
                    Id = Guid.NewGuid(),
                    AreaCode = "18",
                    PhoneNumber = "997241000",
                    Type = ""
                }
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, $"{nameof(request.Phone)}.{nameof(request.Phone.Type)}");
        }
    }
}
