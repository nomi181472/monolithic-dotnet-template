using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateAPIDomainModel;

namespace TemplateAPIDataAccess.DBContext
{
    public class TemplateDBContext: DbContext
    {
        public DbSet<Course> courses { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserCredential> userCredentials { get; set; }  
        public DbSet<UserRegisteredCourse> userRegisteredCourse { get; set; }   
        public TemplateDBContext(DbContextOptions<TemplateDBContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRegisteredCourse>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRegisteredCourses)
                .HasForeignKey(x => x.UserId);


            builder.Entity<UserRegisteredCourse>()
                .HasOne(x=>x.Course)
                .WithMany(x=>x.UserRegisteredCourses)
                .HasForeignKey(x=>x.CourseId);


            builder.Entity<User>()
                .HasOne(x => x.Credential)
                .WithOne(x => x.User)
                .HasForeignKey<UserCredential>(x => x.UserId);


        }

    }
}
