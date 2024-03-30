
//using Core.Entities;
//using Core.Infrastructure.Contract;
//using Core.Infrastructure.Contract.Course;
//using Infrastructure.Dbcontext;
//using Infrastructure.Repositories;
//using Infrastructure.Repositories.Course;
//using Microsoft.EntityFrameworkCore;

//namespace WorkWhere
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.

//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at 
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//            {
//                options.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
//            });

//            // Add the registration for GenericRepository<Course>
//            builder.Services.AddScoped<GenericRepository<Course>>();
//            builder.Services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
//            builder.Services.AddScoped<IUnitofwork,Unitofwork>();


//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }
//    }
//}


using Core.Entities;
using Core.Infrastructure.Contract;
using Core.Infrastructure.Contract.Course;
using Infrastructure.Dbcontext;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Course;
using Microsoft.EntityFrameworkCore;

namespace WorkWhere
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
            });

            //Add the registration for GenericRepository<Course>

            builder.Services.AddScoped<GenericRepository<Course>>();
            builder.Services.AddScoped<GenericRepository<User>>();

            builder.Services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
            builder.Services.AddScoped<IUnitofwork, Unitofwork>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
