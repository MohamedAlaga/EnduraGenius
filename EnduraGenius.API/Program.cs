using System.Reflection;
using System.Text;
using EnduraGenius.API.Data;
using EnduraGenius.API.Mappings;
using EnduraGenius.API.Middlewares;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Repositories;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.EmailSenderRepository;
using EnduraGenius.API.Repositories.InbodyRepository;
using EnduraGenius.API.Repositories.MuscleRepositories;
using EnduraGenius.API.Repositories.PlanRepositories;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using EnduraGenius.API.Repositories.TokenRepositories;
using EnduraGenius.API.Repositories.UserRepository;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Error()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "EnduraGenius API",
            Version = "v1"
        });
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
            new OpenApiSecurityScheme{
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            },
            Scheme = "Oauth2",
            Name = JwtBearerDefaults.AuthenticationScheme,
            In = ParameterLocation.Header
            },
            new List<string>()
            }
        });
        options.IncludeXmlComments(xmlPath);
    });
builder.Services.Configure<EmailSettingsModel>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddOptions();

builder.Services.AddDbContext<EnduraGeniusDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("mysql")));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IWorkoutsRepository, SQLWorkoutsRepository>();
builder.Services.AddScoped<IMuscleRepository, SQLMuscleRepository>();
builder.Services.AddScoped<IPlanRepository, SQLPLansRepository>();
builder.Services.AddScoped<IPlansUsersRepository, SQLPlansUsersRepository>();
builder.Services.AddScoped<IPlanWorkoutsRepository, SQLPlanWorkoutRepository>();
builder.Services.AddScoped<IUserWorkoutRepository,SQLUserWorkoutRepository>();
builder.Services.AddScoped<IInbodyRepository,SQLInbodyRepository>();
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("EnduraGenius")


    .AddEntityFrameworkStores<EnduraGeniusDBContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles(
    new StaticFileOptions { FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")) ,
    RequestPath = "/Images"});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
