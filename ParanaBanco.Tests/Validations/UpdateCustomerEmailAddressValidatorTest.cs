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
    public class UpdateCustomerEmailAddressValidatorTest
    {
        private readonly UpdateCustomerEmailAddressValidator _validator;

        public UpdateCustomerEmailAddressValidatorTest()
        {
            _validator = new UpdateCustomerEmailAddressValidator();
        }


        [Fact(DisplayName = "Should return Success when data is valid")]
        [Trait("Validations", "UpdateCustomerEmailAddressValidatorTest")]
        public void ShouldReturnSuccessWhenValidData()
        {
            var request = new UpdateCustomerEmailAddressRequest()
            {
                Id = Guid.NewGuid(),
                EmailAddress = "teste@live.com"
            };

            var result = _validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Should return Failure when id is invalid")]
        [Trait("Validations", "UpdateCustomerEmailAddressValidatorTest")]
        public void ShouldReturnFailureWhenIdIsInvalid()
        {
            var request = new UpdateCustomerEmailAddressRequest()
            {
                EmailAddress = "teste@live.com"
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, nameof(request.Id));
        }

        [Fact(DisplayName = "Should return Failure when email adress is invalid")]
        [Trait("Validations", "UpdateCustomerEmailAddressValidatorTest")]
        public void ShouldReturnFailureWhenEmailAddressIsInvalid()
        {
            var request = new UpdateCustomerEmailAddressRequest()
            {
                Id = Guid.NewGuid(),
                EmailAddress = "teste"
            };

            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result?.Errors?.FirstOrDefault()?.PropertyName, nameof(request.EmailAddress));
        }
    }
}
