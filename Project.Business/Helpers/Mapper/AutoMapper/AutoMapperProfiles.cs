using AutoMapper;
using Project.Core.Core.Entities.Security;
using Project.Entity.Dtos.Authentication;
using Project.Entity.Dtos.Blog;
using Project.Entity.Dtos.Lesson;
using Project.Entity.Dtos.Module;
using Project.Entity.Tables.EvrimDbMSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Business.Helpers.Mapper.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, Core.Core.Entities.Security.User>().ReverseMap();
            CreateMap<UserDto, Core.Core.Entities.Security.User>().ReverseMap();
            CreateMap<BlogDto, Entity.Tables.EvrimDbMSSQL.Blog>().ReverseMap();
            CreateMap<LessonDto, Lesson>().ReverseMap();
            CreateMap<LessonPostDto, LessonPost>().ReverseMap();
            CreateMap<FileLinkDto, FileLink>().ReverseMap();
            CreateMap<OperationClaimDto, OperationClaim>().ReverseMap();


        }
    }
}
