using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityManagement.Application;
using UniversityManagement.Domain.Base;
using UniversityManagement.Infrastructure.HangFire;
using UniversityManagement.Infrastructure.Presistence;
using UniversityManagement.Infrastructure.Repositories.Base;
using UniversityManagement.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
ApplicationServiceRegistration.AddApplicationServices(builder.Services, builder.Configuration);
AuthServiceRegistation.ConfigureJwt(builder.Services, builder.Configuration);
builder.Services.AddHangFireService(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalException>();

//RecurringJobService.AddRecurringJobs();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSwaggerGen(setupAction: c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "University Management API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "MyApp Jobs Dashboard",
    StatsPollingInterval = 5000
});

//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
