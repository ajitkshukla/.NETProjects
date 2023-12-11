using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Model.Context;
namespace StudentAPI
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

            builder.Services.AddDbContext<StudentAPIDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

            //adding hangfire dependencies
            builder.Services.AddHangfire((sp, config) =>
            {
                var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("SqlServerConnection");
                config.UseSqlServerStorage(connectionString);
            });
            builder.Services.AddHangfireServer();

            
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // configuration for Hangifire Dashboard
            app.UseHangfireDashboard("/hangfire/dashboard", new DashboardOptions
            {
                DashboardTitle = "Hangfire Dashboard",
                DarkModeEnabled = false,
                DisplayStorageConnectionString = false,
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = "admin",
                        Pass = "admin123"
                    }
                }
            });
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}