using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.IAuthServices
{
    public interface IAuthTokenGenerator
    {
        string GenerateJWTToken(Dictionary<string, string> parameters);
    }
}
