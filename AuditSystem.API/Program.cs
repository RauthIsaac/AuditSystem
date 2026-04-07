using Audit_System.Domain.Interfaces;
using AuditSystem.Application;
using AuditSystem.Application.Interfaces;
using AuditSystem.Application.Mapping;
using AuditSystem.Infrastructure.BackgroundServices;
using AuditSystem.Infrastructure.Persistence;
using AuditSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        #region Dependency Injection

        #region Database Context
        builder.Services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(
                            builder.Configuration.GetConnectionString("DefaultConnection")));
        #endregion


        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        #region Repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICourseRepository, CourseRepository>();
        builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        #endregion

        #region MediatR 
        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.Load("AuditSystem.Application"));
        });
        #endregion


        #region AutoMapper
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        #endregion

        #region In-Memeory Caching
        builder.Services.AddSingleton<IAuditQueue, AuditQueue>();
        builder.Services.AddHostedService<AuditBackgroundWorker>();
        #endregion

        #endregion

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