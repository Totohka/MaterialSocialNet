using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator() {
            RuleFor(m => m.Msg).NotEmpty().MinimumLength(1).WithMessage("Сообщение не может быть пустым!");
        }
    }
}
