using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.Common
{
    public class InvalidCredentialsException:Exception
    {
        public InvalidCredentialsException() { }
        public InvalidCredentialsException(string m):base(m)
        {
            
        }
    }
}
