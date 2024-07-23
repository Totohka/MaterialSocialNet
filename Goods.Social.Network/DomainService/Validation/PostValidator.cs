using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator() {
            //RuleFor(p => p.Title).Matches("^(?:(?!(пидор|хуй|бля)).)*$")
            //                        .WithMessage("В заголовке присутствует нецензурная лексика!")
            //                        .NotEmpty().WithMessage("Проверьте есть ли заголовок!")
            //                        .MaximumLength(100).WithMessage("Слишком большая длина у заголовка!");

            //RuleFor(p => p.Text).Matches("^(?:(?!(пидор|хуй|бля)).)*$")
            //                    .WithMessage("В тексте присутствует нецензурная лексика!")
            //                    .NotEmpty().MinimumLength(1).WithMessage("Проверьте количество символов в тексте!");

            //RuleFor(u => u.Tags).Matches("^(?:(?!(пидор|хуй|бля)).)*$")             
            //                    .WithMessage("В теге присутствует нецензурная лексика!")
            //                    .NotEmpty().MinimumLength(1).WithMessage("Проверьте количество символов в теге!")
            //                    .MaximumLength(100).WithMessage("Слишком большая длина тега");


        }
    }
}
