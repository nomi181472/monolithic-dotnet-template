using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateResponseModel.UserResponse
{
    public  class ResponseUserLogin
    {
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public bool IsRegistered { get; set; }
        public required string UserId { get; set; }
    }
}
