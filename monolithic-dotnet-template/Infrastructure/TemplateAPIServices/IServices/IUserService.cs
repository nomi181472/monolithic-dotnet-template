using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateRequestModel.User;

namespace TemplateAPIServices.IServices
{
    public interface IUserService
    {
        Task<bool> Login (string username, string password);
        Task<bool> Registration (RequestUserAdd  request);
        Task<bool> ChangeUserName(RequestUserChangeUserName request, string userId);
    }
}
