using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateAPIDataAccess.DBContext;
using TemplateAPIDataAccess.Repositories.CommonRepositories;
using TemplateAPIDomainModel;

namespace TemplateAPIDataAccess
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly TemplateDBContext _db;
        public UnitOfWork(TemplateDBContext db)
        {
            _db = db;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }


        public virtual void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);

        }


        public IGenericRepository<User, string> userRepoAccess
        {
            get
            {
                return new GenericRepository<User, string>(_db);
            }
        }

        public IGenericRepository<Course, string> courseRepoAccess
        {
            get
            {
                return new GenericRepository<Course, string>(_db);
            }
        }

        public IGenericRepository<UserCredential, string> userCredentialRepoAccess
        {
            get
            {
                return new GenericRepository<UserCredential, string>(_db);
            }
        }
        public IGenericRepository<UserRegisteredCourse, string> userRegisteredCourse
        {
            get
            {
                return new GenericRepository<UserRegisteredCourse, string>(_db);
            }
        }
    }
}
