using Auth.IAuthServices;
using TemplateResponseModel.UserResponse;

namespace Template.API.Helpers
{
    public class TOKENDTO
    {
        public required string Email { get; set; }
    }
    public class TokenGenerator
    {

        public static string GetJWTToken(TOKENDTO result,IAuthTokenGenerator _tokenService)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Email", result.Email);
            string token = _tokenService.GenerateJWTToken(dict);
            return token;
        }
    }
}
