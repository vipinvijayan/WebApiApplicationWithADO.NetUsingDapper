using DrCleanerAppWebApis.CustomMiddleware;
using DryCleanerAppBuisinessLogic;
using DryCleanerAppBuisinessLogic.Implementation;
using DryCleanerAppBuisinessLogic.Interfaces;
using DryCleanerAppBussinessLogic.Implementation;
using DryCleanerAppBussinessLogic.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//To allow cross-origin request.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed((hosts) => true));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Add this configuration to enable authorize tab for token in swagger UI
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "DrCleaner Web API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//Added JWT Authentication Configuration 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = "DryCleaner",
        ValidIssuer = "DryCleaner",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSignInKey"]))

    };
});
// Read the connection string from appsettings.
string dbConnectionString = builder.Configuration.GetConnectionString("AppConnection");
//Injected MySql Connection String
builder.Services.AddTransient<IDbConnection>(sql => new MySqlConnection(dbConnectionString));

builder.Services.AddRepositoryDependencies(); //Repository Layer Injection
builder.Services.AddScoped<ICompanyB, CompanyB>(); // Custom Buisiness layer injection
builder.Services.AddScoped<IUserB, UserB>();// Custom Buisiness layer injection
builder.Services.AddSingleton<ISecurityB, SecurityB>();// Custom Buisiness layer injection using add singleton to use in custom middleware for token status check from DB
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();//this checks the user is valid
app.UseAuthorization();//this checks user has valid roles
//Use this middle ware we are validating the token from the header is valid on Database.
app.UseMiddleware<ValidateTokenMiddleware>();
app.MapControllers();

app.Run();
