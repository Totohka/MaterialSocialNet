using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class SettingPrivacyValidator : AbstractValidator<SettingPrivacy>
    {
        public SettingPrivacyValidator() {
            RuleFor(sn => sn.InvateChats).LessThan(3).WithMessage("Прислано некорректное значение, больше 2!");
            RuleFor(sn => sn.InvateChats).GreaterThan(0).WithMessage("Прислано некорректное значение, меньше 0!");

            RuleFor(sn => sn.ShowPost).LessThan(3).WithMessage("Прислано некорректное значение, больше 2!");
            RuleFor(sn => sn.ShowPost).GreaterThan(0).WithMessage("Прислано некорректное значение, меньше 0!");

            RuleFor(sn => sn.ShowDateBirthday).LessThan(3).WithMessage("Прислано некорректное значение, больше 2!");
            RuleFor(sn => sn.ShowDateBirthday).GreaterThan(0).WithMessage("Прислано некорректное значение, меньше 0!");
        }
    }
}
