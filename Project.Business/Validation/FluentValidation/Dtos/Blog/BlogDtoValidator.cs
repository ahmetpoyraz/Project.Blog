using FluentValidation;
using Project.Entity.Dtos.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validation.FluentValidation.Dtos.Blog
{
    public class BlogDtoValidator :AbstractValidator<BlogDto>
    {
        public BlogDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5);
        }
    }
}
