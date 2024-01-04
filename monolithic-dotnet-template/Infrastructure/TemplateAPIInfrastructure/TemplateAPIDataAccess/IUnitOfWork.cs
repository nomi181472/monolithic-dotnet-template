using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateAPIDataAccess.Repositories.CommonRepositories;
using TemplateAPIDomainModel;

namespace TemplateAPIDataAccess
{
    public interface IUnitOfWork
    {
        IGenericRepository<User, string> userRepoAccess { get; }
        IGenericRepository<Course, string> courseRepoAccess { get; }
        /*  IGenericRepository<UserProfileCredential,string> userCredentialRepoAccess { get; }*/
        IGenericRepository<UserCredential, string> userCredentialRepoAccess { get; }
        IGenericRepository<UserRegisteredCourse, string> userRegisteredCourse { get; }
      


        void Commit();
        Task CommitAsync();
    }
}
