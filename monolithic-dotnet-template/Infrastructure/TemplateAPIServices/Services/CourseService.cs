using CustomExceptionModel.Common;
using Microsoft.EntityFrameworkCore;
using TemplateAPIDataAccess;
using TemplateAPIDomainModel;
using TemplateAPIServices.IServices;
using TemplateRequestModel.Course;
using TemplateResponseModel.Course;

namespace TemplateAPIServices.Services
{
    public class CourseService : ICourseService
        
    {
        readonly IUnitOfWork _uof;
        public CourseService(IUnitOfWork unit)
        {
            _uof = unit;
        }
        public async Task<bool> Add(RequestCourseAdd request, string userId)
        {
            Course course = new Course()
            {
                CourseName = request.CourseName,
                CourseType = request.CourseType,
                CreatedBy=userId,
                CreatedDate = DateTime.Now,
                Id=Guid.NewGuid().ToString(),
                IsActive=true,
                IsArchived=false,
                UpdatedBy=userId,
                UpdatedDate=DateTime.Now,
                
                
            };
            var result=await _uof.courseRepoAccess.AddAsync(course, userId);

            if (result.result)
            {
                await _uof.CommitAsync();
                return result.result;
            }
            else 
            {
                throw new UnHandledCustomException(result.Message);
            }
        }

        public async Task<bool> DeleteById(string courseId, string userId)
        {
            var result = _uof.courseRepoAccess.UpdateOnCondition(
                filter => filter.Id == courseId,
                setter => setter
                .SetProperty(s=>s.UpdatedBy,userId)
                .SetProperty(s => s.UpdatedDate, DateTime.UtcNow)
                .SetProperty(s=>s.IsActive,false)

             
                );
            if (result.result)
            {
                return result.result;
            }
            else
            {
                throw new UnHandledCustomException(result.Message);
            }
        }

        public async Task<ResponseCourseGet> GetById(string courseId)
        {
           var result= _uof.courseRepoAccess.Get(x=>x.IsActive && x.Id==courseId);
           if (result.Status)
            {
                var single = result.Data.FirstOrDefault();
                if (single == null)
                {
                    throw new RecordNotFoundException($"courseId: {courseId} not found");
                }
                

                ResponseCourseGet response = new ResponseCourseGet()
                {
                    CourseId = single.Id,
                    CourseName = single.CourseName,
                    CourseType = single.CourseType,
                    CreatedBy = single.CreatedBy,
                    UpdateDate = single.UpdatedDate,
                    CreatedDate = single.CreatedDate,
                    UpdatedBy = single.UpdatedBy,

                };
                return response;
            }
            else
            {
                throw new UnHandledCustomException(result.Message);
            }
        }

        public   async Task<ResponseCourseList> List()
        {
            var result= await _uof.courseRepoAccess.GetAsync(x=>x.IsActive);
            if (result.Status)
            {
                ResponseCourseList response = new ResponseCourseList()
                {
                    Courses = result.Data.Select(x => new ResponseCourseGet() 
                    {
                        CourseType=x.CourseType,
                        CourseName  =x.CourseName,
                        CreatedBy=x.CreatedBy,
                        CreatedDate=x.CreatedDate,
                        UpdateDate=x.UpdatedDate,
                        UpdatedBy=x.UpdatedBy,
                        CourseId = x.Id 
                    }
                    ).ToList()
                };
                return response;
            }
            else
            {
                throw new UnHandledCustomException(result.Message);
            }

        }

        public async Task<bool> Update(RequestCourseUpdate request,string courseId, string userId)
        {
            var result = _uof.courseRepoAccess.UpdateOnCondition(
               filter => filter.Id == courseId,
               setter => setter
               .SetProperty(s => s.UpdatedBy, userId)
               .SetProperty(s => s.UpdatedDate, DateTime.UtcNow)
               .SetProperty(s=>s.CourseName,request.CourseName)
               .SetProperty(s=>s.CourseType,request.CourseType)
               );
            if (result.result)
            {

                return result.result;
            }
            else
            {
                throw new UnHandledCustomException(result.Message);
            }
        }
    }
}
