using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDTOs.UserDTOs
{
    public class GetUser
    {
        public string UserId { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public string UserEmail { get; set; } = String.Empty;
       
    }
}
