using FluentValidation;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(IValidator<Post> postValidator) {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email написан неверно!");
            RuleFor(u => u.FirstName).NotEmpty().MinimumLength(1).MaximumLength(25).WithMessage("Проверьте длину имени");
            RuleFor(u => u.LastName).NotEmpty().MinimumLength(1).MaximumLength(25).WithMessage("Проверьте длину фамилиии");
            RuleFor(u => u.Phone).Matches("^(\\+)?((\\d{2,3}) ?\\d|\\d)(([ -]?\\d)|( ?(\\d{2,3}) ?)){5,12}\\d$").WithMessage("Неверное написан номер телефона!").When(u => u.Phone != string.Empty);
            RuleFor(u => u.Status).MaximumLength(200).WithMessage("Превышена максимальная длина статуса!");
            //RuleFor(u => u.Status).Matches("(?:(?:(?:у|[нз]а|(?:хитро|не)?вз?[ыьъ]|с[ьъ]|(?:и|ра)[зс]ъ?|(?:о[тб]|п[оа]д)[ьъ]?|(?:.\\B)+?[оаеи-])-?)?(?:[её](?:б(?!о[рй]|рач)|п[уа](?:ц|тс))|и[пб][ае][тцд][ьъ]).*?|(?:(?:н[иеа]|ра[зс]|[зд]?[ао](?:т|дн[оа])?|с(?:м[еи])?|а[пб]ч)-?)?ху(?:[яйиеёю]|л+и(?!ган)).*?|бл(?:[эя]|еа?)(?:[дт][ьъ]?)?|\\S*?(?:п(?:[иеё]зд|ид[аое]?р|ед(?:р(?!о)|[аое]р|ик)|охую)|бля(?:[дбц]|тс)|[ое]ху[яйиеё]|хуйн).*?|(?:о[тб]?|про|на|вы)?м(?:анд(?:[ауеыи](?:л(?:и[сзщ])?[ауеиы])?|ой|[ао]в.*?|юк(?:ов|[ауи])?|е[нт]ь|ища)|уд(?:[яаиое].+?|е?н(?:[ьюия]|ей))|[ао]л[ао]ф[ьъ](?:[яиюе]|[еёо]й))|елд[ауые].*?|ля[тд]ь|(?:[нз]а|по)х)").WithMessage("В статусе присутствует нецензурная лексика!").When(u => u.Status != string.Empty); ;

            RuleForEach(u => u.Posts).SetValidator(postValidator);
            
        }
    }
}
