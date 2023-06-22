using FluentValidation;
using ParanaBanco.Application.DTO;
using ParanaBanco.Application.DTO.Request;
using ParanaBanco.Domain.Enums;

namespace ParanaBanco.Application.Validations
{
    public class AddCustomerValidator : AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty();

            RuleFor(x => x.EmailAddress)
                .EmailAddress();

            When(x => x.Phones != null && x.Phones.Any(), () =>
            {
                RuleForEach(x => x.Phones)
                    .SetValidator(new AddPhoneValidator());
            });

        }
    }

    public class AddPhoneValidator : AbstractValidator<AddPhoneDTO>
    {
        private string[] TypesPhone = { nameof(PhoneType.Landline).ToLower(), nameof(PhoneType.Mobile).ToLower() };
        public AddPhoneValidator()
        {
            RuleFor(x => x.AreaCode)
                .NotEmpty()
                .Matches("^[0-9]*$")
                .Length(2);

            RuleFor(x => x.Type)
                .NotEmpty()
                .Must(x => TypesPhone.Contains(x.ToLower()));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches("^[0-9]*$")
                .Length(8)
                .When(x => x.Type.Equals(nameof(PhoneType.Landline), StringComparison.OrdinalIgnoreCase));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches("^[0-9]*$")
                .MinimumLength(8)
                .MaximumLength(9)
                .When(x => x.Type.Equals(nameof(PhoneType.Mobile), StringComparison.OrdinalIgnoreCase));
        }
    }
}
