using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateAPIDomainModel
{
    public class UserCredential:Base<string>
    {
        public required string PasswordHash { set; get; }
        public required string PasswordSalt { set; get; }
        public virtual User User { set; get; }
        public required string UserId { set; get; }
        

    }
}
