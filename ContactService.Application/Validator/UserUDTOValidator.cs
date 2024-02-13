using ContactService.Application.Models.Request;
using FluentValidation;

namespace ContactService.Application.Validator {
    public class UserUDTOValidator : AbstractValidator<UserUDTO> {
        public UserUDTOValidator() {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Lastname).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Company).NotEmpty().MaximumLength(255);
        }
    }
}
