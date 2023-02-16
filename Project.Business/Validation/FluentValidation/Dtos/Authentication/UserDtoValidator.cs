using FluentValidation;
using Project.Entity.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validation.FluentValidation.Dtos.Authentication
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator(bool isUpdate)
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).WithName("İsim");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).WithName("Soyisim");
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).WithName("Kullanıcı Adı");
            RuleFor(x => x.Email).NotEmpty().WithName("Email").EmailAddress();
            //RuleFor(x => x.ProfilPhoto.File).NotNull().WithName("Profil Fotoğrafı");
            if (!isUpdate)
            {
                RuleFor(x => x.Password).NotEmpty().WithName("Parola").MinimumLength(8);
            }
        }
    }
}
