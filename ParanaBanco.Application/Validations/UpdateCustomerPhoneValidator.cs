using FluentValidation;
using ParanaBanco.Application.DTO.Request;
using ParanaBanco.Domain.Enums;

namespace ParanaBanco.Application.Validations
{
    public class UpdateCustomerPhoneValidator : AbstractValidator<UpdateCustomerPhoneRequest>
    {
        private string[] TypesPhone = { nameof(PhoneType.Landline).ToLower(), nameof(PhoneType.Mobile).ToLower() };
        public UpdateCustomerPhoneValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Phone.Id).NotEmpty();

            RuleFor(x => x.Phone.AreaCode)
                .NotEmpty()
                .Matches("^[0-9]*$")
                .Length(2);

            RuleFor(x => x.Phone.Type)
                .NotEmpty()
                .Must(x => TypesPhone.Contains(x.ToLower()));

            RuleFor(x => x.Phone.PhoneNumber)
                .NotEmpty()
                .Matches("^[0-9]*$")
                .Length(8)
                .When(x => x.Phone.Type.Equals(nameof(PhoneType.Landline), StringComparison.OrdinalIgnoreCase));

            RuleFor(x => x.Phone.PhoneNumber)
                .NotEmpty()
                .Matches("^[0-9]*$")
                .MinimumLength(8)
                .MaximumLength(9)
                .When(x => x.Phone.Type.Equals(nameof(PhoneType.Mobile), StringComparison.OrdinalIgnoreCase));
        }
    }
}
