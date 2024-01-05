

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
    }
}
