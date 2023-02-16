using Microsoft.Extensions.DependencyInjection;
using Project.Data.Authentication;
using Project.Data.Blog;
using Project.Data.Lesson;
using Project.Data.Module;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.DependencyResolvers.System
{
    public static class DataAccessResolvers
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonPostRepository, LessonPostRepository>();
            services.AddScoped<IFileLinkRepository, FileLinkRepository>();
            services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();



            return services;
        }
    }
}
