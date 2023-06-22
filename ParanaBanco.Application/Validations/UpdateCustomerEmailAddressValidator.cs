using FluentValidation;
using ParanaBanco.Application.DTO.Request;

namespace ParanaBanco.Application.Validations
{
    public class UpdateCustomerEmailAddressValidator : AbstractValidator<UpdateCustomerEmailAddressRequest>
    {
        public UpdateCustomerEmailAddressValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.EmailAddress).EmailAddress();
        }
    }
}
