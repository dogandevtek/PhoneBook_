using ContactService.Application.Models.Request;
using FluentValidation;

namespace ContactService.Application.Validator {
    public class UserContactUDTOValidator : AbstractValidator<UserContactUDTO> {
        public UserContactUDTOValidator() {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.E_mail).NotEmpty().MaximumLength(100).EmailAddress();
            RuleFor(x => x.Location).NotEmpty().MaximumLength(100);
        }
    }
}
