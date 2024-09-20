using Company.G03.BLL;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Data.Contexts;
using Company.G03.PL.Mapping.Employees;
using Company.G03.PL.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace Company.G03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            //builder.Services.AddScoped<AppDbContext>(); //Allow DI For AppDbContext
            
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); //Allow DI For AppDbContext

            //builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>(); //Allow DI For DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //Allow DI For DepartmentRepository

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(EmployeeProfile));


            //Life Time
            //builder.Services.AddScoped();//Per Request, UnReachable Obj
            //builder.Services.AddSingleton();//Per App 
            //builder.Services.AddTransient();//Per Operations


            builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddTransient<ITransientService, TransientService>();
            builder.Services.AddSingleton<ISingletoneService, SingletoneService>();



            //builder.Services.AddScoped<DepartmentRepository>(); //Allow DI For DepartmentRepository



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
