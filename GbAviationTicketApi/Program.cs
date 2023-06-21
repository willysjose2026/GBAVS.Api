using GbAviationTicketApi;
using GbAviationTicketApi.Data;
using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Extentions;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Repository;
using GbAviationTicketApi.Repository.IRepository;
using GbAviationTicketApi.Security;
using GbAviationTicketApi.Services.LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthorizationHandler, UserAgentAuthorizationHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DeviceCheckPolicy", policy =>
    {
        var clients = new List<string>
        {
            builder.Configuration.GetValue<string>("Clients:GbavsDesktopReports") ?? "",
            builder.Configuration.GetValue<string>("Clients:GbavsMobileReports") ?? ""
        };

        policy.AddRequirements(new UserAgentRequirement(clients));
    });
});

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentity<GbavsUser, IdentityRole>()
    .AddEntityFrameworkStores<GbavsContext>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new()
    {
        Description = 
            "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
            " Enter 'Bearer' [space] and your token in the next input below \r\n\r\n" +
            " Example: \"Bearer asmamcaldskm\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new()
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
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddDbContext<GbavsContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("GBAVS")));

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret") ?? "";

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IGbavsContext, GbavsContext>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

var app = builder.Build();


app.ConfigureExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
