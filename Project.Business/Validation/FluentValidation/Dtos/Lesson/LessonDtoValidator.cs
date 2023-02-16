using FluentValidation;
using Project.Entity.Dtos.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validation.FluentValidation.Dtos.Lesson
{
    public class LessonDtoValidator :AbstractValidator<LessonDto>
    {
        public LessonDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5);

        }
    }
}
