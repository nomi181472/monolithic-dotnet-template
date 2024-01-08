using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateResponseModel.UserResponse
{
    public class ResponseUserGet
    {
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public required string CreatedBy { get; set; }
        public required string UpdatedBy { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime UpdateDate { get; set; }
        public required bool IsActive { get; set; }
    }
}
