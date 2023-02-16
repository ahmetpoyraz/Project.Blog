using Project.Data.Authentication;
using Project.Data.Blog;
using Project.Data.Lesson;
using Project.Data.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public interface IUnitOfWork
    {
        IBlogRepository BlogRepository { get; }
        IUserRepository UserRepository { get; }
        ILessonRepository LessonRepository { get; }
        ILessonPostRepository LessonPostRepository { get; }
        IFileLinkRepository FileLinkRepository { get; }
        IOperationClaimRepository OperationClaimRepository { get; }
        IUserOperationClaimRepository UserOperationClaimRepository { get; }
        void Commit();
        void Dispose();
        void Rollback();
    }
}
