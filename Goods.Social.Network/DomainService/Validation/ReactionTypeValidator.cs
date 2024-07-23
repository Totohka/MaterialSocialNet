using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class ReactionTypeValidator : AbstractValidator<ReactionType>
    {
        public ReactionTypeValidator() {
            RuleFor(u => u.Name).NotEmpty().MinimumLength(1).WithMessage("Название реакции не может быть пустым!")
                                .MaximumLength(20).WithMessage("Название реакции не может превышать 20 символов!");
            RuleFor(u => u.Description).NotEmpty().MinimumLength(1).WithMessage("Описание реакции не может быть пустым!")
                                       .MaximumLength(100).WithMessage("Название реакции не может превышать 100 символов!");
        }
    }
}
