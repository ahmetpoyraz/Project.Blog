using Microsoft.Extensions.DependencyInjection;
using Project.Business.Authentication;
using Project.Business.Blog;
using Project.Business.Services.Lesson;
using Project.Business.Services.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.DependencyResolvers.System
{
    public static class BusinessResolvers
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ILessonService,LessonService>();
            services.AddScoped<IFileLinkService, FileLinkService>();

            return services;
        }
    }
}
