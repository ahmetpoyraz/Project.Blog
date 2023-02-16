using FluentValidation;
using Project.Entity.Dtos.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validation.FluentValidation.Dtos.Lesson
{
    public class LessonPostDtoValidator : AbstractValidator<LessonPostDto>
    {
        public LessonPostDtoValidator()
        {
            RuleFor(x => x.Caption).NotEmpty().MinimumLength(5).WithName("Başlık");
            RuleFor(x => x.Description).NotEmpty().MinimumLength(5).WithName("Açıklama");


        }
    }
}
