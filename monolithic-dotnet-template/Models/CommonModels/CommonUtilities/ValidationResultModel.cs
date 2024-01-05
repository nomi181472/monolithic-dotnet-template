using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepoResult.Common;


namespace ResponseModel.Common
{
    public class ValidationResultModel : ApiResponse
    {




        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Success = false;
            Data = null;
            IsApiHandled  = true;
            StatusCode = HTTPStatusCode400.BadRequest;
            Message = "Invalid Request";


            exception = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();

        }
    }

    public class ValidationError
    {
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty && field != null ? field : null;
            Message = message;
        }
    }
}
