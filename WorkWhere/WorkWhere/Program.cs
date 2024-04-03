using Application.Services;
using Core.Application.Contract;
using Core.Entities;
using Core.Infrastructure.Contract;
using Infrastructure.Dbcontext;
using Infrastructure.Repositories;
using CloudinaryDotNet.Core;

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
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
            });

          //  builder.Services.AddTransient<CloudinaryService>();

            //Add the registration for GenericRepository<Course>

            builder.Services.AddScoped<GenericRepository<Course>>();
            builder.Services.AddScoped<GenericRepository<User>>();

            builder.Services.AddScoped<IUnitofwork, Unitofwork>();
            builder.Services.AddScoped<ICourseService, CourseService>();



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
