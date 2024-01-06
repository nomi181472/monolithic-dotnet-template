using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateAPIDataAccess;
using TemplateAPIServices.IServices;
using TemplateRequestModel.User;

namespace TemplateAPIServices.Services
{
    public class UserService:IUserService
    {
        readonly IUnitOfWork _uof;
        public UserService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public Task<bool> ChangeUserName(RequestUserChangeUserName request, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Registration(RequestUserAdd request)
        {
            throw new NotImplementedException();
        }
    }
}
