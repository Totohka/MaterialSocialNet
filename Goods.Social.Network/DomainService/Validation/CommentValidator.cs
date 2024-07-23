using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator() {
            //RuleFor(c => c.Text).NotEmpty().MinimumLength(1).WithMessage("Комментарий не может быть пустым!")
                                //.MaximumLength(300).WithMessage("Длина комментария не может превышать 300 символов");
            RuleFor(c => c.UserId).GreaterThanOrEqualTo(1).WithMessage("Id пользователя не может быть меньше 1");
            //RuleFor(c => c.PostId).GreaterThanOrEqualTo(1).WithMessage("Id поста не может быть меньше 1");
        }
    }
}
