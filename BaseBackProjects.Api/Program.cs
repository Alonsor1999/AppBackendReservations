using Application.Common.ConnectionString;
using Application.Common.Exceptions.Handle;
using Application.Common.Interfaces;
using Application.Common.Options;
using Application.Cqrs.User.Commands;
using Application.Services.CurrentUserService;
using BaseBackProjects.Api.Configurations;
using BaseBackProjects.Api.Filters;
using Core.Filter;
using FluentValidation.AspNetCore;
using Infra.Data.Context;
using Infra.Ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

var ConnectionString = new ConnectionString(Configuration);
var Connection = Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<AplicationDBContext>(options => {
    options.UseSqlServer(Connection);
    //options.UseSqlServer(ConnectionString.DecryptConnection());
    //options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"));
    //options.UseMySql(Connection, ServerVersion.AutoDetect(Connection));

});

builder.Services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
    };
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(PostUserCommand).Assembly));

builder.Services.AddCors();
builder.Services.RegisterAutoMapper();

RegisterServices(builder.Services);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


//if (app.Environment.IsProduction())
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);

    services.AddMvc(options =>
    {
        options.Filters.Add<ValidationFilter>();
        options.Filters.Add<ApiExceptionFilterAttribute>();

    }).AddFluentValidation(options =>
    {
        //options.RegisterValidatorsFromAssemblyContaining<PostOvertimeCommandValidator>();
    });
}
