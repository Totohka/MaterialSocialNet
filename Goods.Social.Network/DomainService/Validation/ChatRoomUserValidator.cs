using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class ChatRoomUserValidator : AbstractValidator<ChatRoomUser>
    {
        public ChatRoomUserValidator() {
            RuleFor(cr => cr.ChatRoomId).GreaterThanOrEqualTo(1).WithMessage("Id чата не может быть меньше 1!");
            RuleFor(cr => cr.UserId).GreaterThanOrEqualTo(1).WithMessage("Id пользователя не может быть меньше 1!");
        }
    }
}
