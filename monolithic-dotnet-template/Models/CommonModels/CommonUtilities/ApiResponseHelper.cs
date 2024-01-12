

namespace ResponseModel.Common
{
    public static class ApiResponseHelper
    {
        public static ApiResponse Convert(bool IsRequestHandled,bool status, string message, int statusCode, Object data)
        {
            ApiResponse model = new ApiResponse();
            model.IsApiHandled = IsRequestHandled;
            model.Success = status;
            model.StatusCode = statusCode;
            model.Message = message;
            model.Data = data;
            
            return model;
        }
        public static ApiResponseToken Convert(bool IsRequestHandled, bool status, string message, int statusCode, Object data,string token)
        {
            ApiResponseToken model = new ApiResponseToken();
            model.IsApiHandled = IsRequestHandled;
            model.Success = status;
            model.StatusCode = statusCode;
            model.Message = message;
            model.Data = data;
            model.Token=token;

            return model;
        }
    }
}
