using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateAPIDomainModel
{
    public class User:Base<string>
    {
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public virtual UserCredential Credential { get; set; }
        public virtual ICollection<UserRegisteredCourse> UserRegisteredCourses { get; set; }

    }
}
