using FluentValidation;
using Project.Entity.Tables.EvrimDbMSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validation.FluentValidation.Tables.EvrimDbMSSQL
{
    public class UserValidator : AbstractValidator<Core.Core.Entities.Security.User>
    {
        public UserValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty();
            RuleFor(x=>x.FirstName).NotEmpty();
            RuleFor(x=>x.LastName).NotEmpty();
            RuleFor(x=>x.PasswordHash).NotEmpty();
            RuleFor(x => x.PasswordSalt).NotEmpty();
        }
    }
}
