
using Application.Helpers;
using Application.Interfaces.PlaceInterfaces;
using Application.Interfaces.RoomInterfaces;
using Application.Services.PlaceServices;
using Application.Services.RoomServices;
using Core.Interfaces;
using Infrastructure.Dbcontext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
                options.UseSqlServer(builder.Configuration.GetConnectionString("con1"))
                //for testing sql queries
                .LogTo(Console.WriteLine, LogLevel.Information);
            });

            builder.Services.AddScoped<IPlaceServices, PlaceService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IPlaceReviewService,PlaceReviewService>();
            builder.Services.AddScoped<IRoomReviewService,RoomReviewService>();
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Mapper));
            



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();

            }
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
