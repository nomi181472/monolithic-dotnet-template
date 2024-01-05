using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateAPIDataAccess;
using TemplateAPIServices.IServices;

namespace TemplateAPIServices.Services
{
    public class UserService:IUserService
    {
        readonly IUnitOfWork _uof;
        public UserService(IUnitOfWork uof)
        {
            _uof = uof;
        }




    }
}
