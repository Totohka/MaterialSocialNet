using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class UserFriendValidator : AbstractValidator<UserFriend>
    {
        public UserFriendValidator() {
            RuleFor(u => u.UserId).GreaterThanOrEqualTo(1).WithMessage("Id пользователя меньше или равен 0");
            RuleFor(u => u.UserFriendId).GreaterThanOrEqualTo(1).WithMessage("Id пользователя меньше или равен 0");          
        }
    }
}
