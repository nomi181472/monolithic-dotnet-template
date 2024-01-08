using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateRequestModel.User;
using TemplateResponseModel.UserResponse;

namespace TemplateAPIServices.IServices
{
    public interface IUserService
    {
        Task<ResponseUserLogin> Login (RequestUserLogin request);
        Task<ResponseUserRegistration> Registration (RequestUserAdd  request);
        Task<bool> ChangeUserName(RequestUserChangeUserName request, string userId);
        Task<ResponseUserGet> GetUserById(string userId);
    }
}
