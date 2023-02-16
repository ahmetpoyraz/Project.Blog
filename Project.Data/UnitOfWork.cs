using Project.Data.Authentication;
using Project.Data.Blog;
using Project.Data.Lesson;
using Project.Data.Module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
    
        public IBlogRepository  BlogRepository { get; }
        public IUserRepository UserRepository { get; }
        public ILessonRepository LessonRepository { get; }
        public ILessonPostRepository LessonPostRepository { get; }
        public IFileLinkRepository FileLinkRepository { get; }
        public IOperationClaimRepository OperationClaimRepository { get; }
        public IUserOperationClaimRepository UserOperationClaimRepository { get; }

        IDbTransaction _dbTransaction;

        public UnitOfWork(IDbTransaction dbTransaction,
            IBlogRepository blogRepository,
            IUserRepository userRepository,
            ILessonRepository lessonRepository,
            ILessonPostRepository lessonPostRepository,
            IFileLinkRepository fileLinkRepository,
            IOperationClaimRepository operationClaimRepository,
            IUserOperationClaimRepository userOperationClaimRepository
            )
        {
            _dbTransaction = dbTransaction;

            BlogRepository = blogRepository;
            UserRepository = userRepository;
            LessonRepository = lessonRepository;
            LessonPostRepository= lessonPostRepository;
            FileLinkRepository = fileLinkRepository;
            OperationClaimRepository = operationClaimRepository;
            UserOperationClaimRepository = userOperationClaimRepository;
         
        }

        public void Commit()
        {
            try
            {

                _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
        }
    }
}
