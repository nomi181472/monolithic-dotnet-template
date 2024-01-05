using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace ResponseModel.Common
{
    public class ApiResult : ObjectResult
    {
        public ApiResult(ModelStateDictionary keyValuePairs) : base(new ValidationResultModel(keyValuePairs) )
        {
        }
        public ApiResult(ApiResponse value) : base(value)
        {
        }
    }

    public class ApiResponse
    {
       

        public bool IsApiHandled  { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
        public Object exception { get; set; } =new List<string>();
        
    }
    
}
