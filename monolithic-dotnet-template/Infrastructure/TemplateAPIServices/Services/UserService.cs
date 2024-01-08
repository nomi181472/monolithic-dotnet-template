
using CustomExceptions.Common;
using Microsoft.EntityFrameworkCore;

using TemplateAPIDataAccess;
using TemplateAPIDomainModel;
using TemplateAPIServices.IServices;
using TemplateRequestModel.User;
using TemplateResponseModel.UserResponse;

namespace TemplateAPIServices.Services
{
    public class UserService:IUserService
    {
        readonly IUnitOfWork _uof;
        public UserService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> ChangeUserName(RequestUserChangeUserName request, string userId)
        {
            var result = _uof.userRepoAccess.UpdateOnCondition(x => x.Id.Equals(userId) && x.IsActive,
                setter => setter.SetProperty(b => b.UserName, request.UserName)
                );
            if (result.result)
            {
                return true;
            }
            else
            {
                throw new UnHandledCustomException(result.Message);
            }
        }
        public async Task<ResponseUserGet> GetUserById(string userId)
        {
           var getter=await  _uof.userRepoAccess.GetAsync(x=>x.Id.Equals(userId) && x.IsActive);
            if (getter.Status)
            { var singleResponse = getter.Data.FirstOrDefault();
                if (singleResponse == null)
                {
                    throw new RecordNotFoundException($"userId:{userId} not found");
                }
                return new ResponseUserGet()
                {
                    CreatedBy = singleResponse.CreatedBy,
                    CreatedDate=singleResponse.CreatedDate,
                    IsActive = singleResponse.IsActive,
                    UpdateDate=singleResponse.UpdatedDate,
                    UpdatedBy=singleResponse.UpdatedBy,
                    UserEmail=singleResponse.UserEmail,
                    UserId=singleResponse.Id,
                    UserName=singleResponse.UserName
                    
                };
            }
            else
            {
                throw new UnHandledCustomException(getter.Message);
            }
        }

        public async Task<ResponseUserLogin> Login(RequestUserLogin request)
        {
            string passwordHash = request.Password;
            string passwordSalt = request.Password;
            string email = request.UserEmail;
            var query=_uof.userRepoAccess.GetQueryable();
            if (query.Status)
            {
               var data= await query.Data.Where(
                    x => x.UserEmail.ToLower().Equals(email.ToLower())
                    &&
                    x.IsActive
                    &&
                    x.Credential.PasswordHash.Equals(passwordHash)
                    &&
                    x.Credential.PasswordSalt.Equals(passwordSalt)
                    ).Include(x => x.Credential).FirstOrDefaultAsync();
                if (data == null)
                {
                    throw new InvalidCredentialsException("incorrect password or email");
                }
                return new ResponseUserLogin()
                {
                    IsRegistered = true,
                    UserEmail = email,
                    UserId = data.Id,
                    UserName = data.UserName,

                };

            }
            else
            {
                throw new UnHandledCustomException(query.Message);
            }


        }
        public async Task<bool> IsEmailUnique(string userEmail)
        {
            var result=await _uof.userRepoAccess.GetAsync(x=>x.UserEmail.Equals(userEmail));
            if (result.Status)
            {
                return result.Data.Count() == 0;
            }
            else
            {
                throw new UnHandledCustomException(result.Message);
            }

        }
        public async Task<ResponseUserRegistration> Registration(RequestUserAdd request)
        {
            if (await IsEmailUnique(request.Email))
            {
                return await AddUserAsync(request);
            }
            else
            {
                throw new RecordAlreadyExistException($"Email: {request.Email} already exist");
            }
           
            
        }

        private async Task<ResponseUserRegistration> AddUserAsync(RequestUserAdd request)
        {
            string userId = Guid.NewGuid().ToString();
            UserCredential credential = new UserCredential()
            {

                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
                Id = userId,
                IsActive = true,
                IsArchived = false,
                PasswordHash = request.Password,
                PasswordSalt = request.Password,
                UpdatedBy = userId,
                UpdatedDate = DateTime.UtcNow,
                UserId = userId,
            };
            User user = new User()
            {
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
                Id = userId,
                IsActive = true,
                IsArchived = false,
                UserEmail = request.Email,
                UserName = request.UserName,
                UpdatedBy = userId,
                UpdatedDate = DateTime.UtcNow,


            };

            var userSetter = await _uof.userRepoAccess.AddAsync(user, userId);
            var credsSetter = await _uof.userCredentialRepoAccess.AddAsync(credential, userId);
            if (credsSetter.result && userSetter.result)
            {
                await _uof.CommitAsync();
                return new ResponseUserRegistration()
                {
                    IsRegistered = true,
                    UserEmail = user.UserEmail,
                    UserId = userId,
                    UserName = user.UserName,
                };

            }
            else if (!userSetter.result)
            {
                throw new UnHandledCustomException(userSetter.Message);
            }
            else
            {
                throw new UnHandledCustomException(credsSetter.Message);
            }
        }
    }
}
