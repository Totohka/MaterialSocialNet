using FluentValidation;

namespace DomainServices.Validation
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator() {
            RuleFor(p => p).GreaterThanOrEqualTo(1).WithMessage("Id сущности не может быть меньше 1");
        }
    }
}
