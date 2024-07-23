using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class ChatRoomValidator : AbstractValidator<ChatRoom>
    {
        public ChatRoomValidator() {
            RuleFor(cr => cr.Name).NotEmpty().MinimumLength(1).WithMessage("Название чата не может быть пустым!")
                                  .MaximumLength(50).WithMessage("Название чата не должно превышать 50 символов!");
            RuleFor(cr => cr.Description).MaximumLength(100).WithMessage("Описание чата не может превышать 100 символов!");
        }
    }
}
