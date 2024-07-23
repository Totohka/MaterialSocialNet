using FluentValidation;

namespace DomainServices.Validation
{
    public class ReactionData
    {
        public int entityId; 
        public int userId; 
        public int typeReactionId;
        public int typeReactionOldId;
        public bool IsUpdate = false;
    }
    public class ReactionValidator : AbstractValidator<ReactionData>
    {
        public ReactionValidator() {
            RuleFor(u => u.userId).GreaterThanOrEqualTo(1).WithMessage("Id пользователя не может быть меньше 1");
            RuleFor(u => u.entityId).GreaterThanOrEqualTo(1).WithMessage("Id оцениваемой сущности не может быть меньше 1");
            RuleFor(u => u.typeReactionId).GreaterThanOrEqualTo(1).WithMessage("Id типа реакции не может быть меньше 1");
            RuleFor(u => u.typeReactionOldId).GreaterThanOrEqualTo(1).WithMessage("Старый id типа реакции не может быть меньше 1").When(u => u.IsUpdate);
        }
    }
}
