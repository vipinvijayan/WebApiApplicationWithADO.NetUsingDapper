using DryCleanerAppBuisinessLogic;
using DryCleanerAppBuisinessLogic.Implementation;
using DryCleanerAppBuisinessLogic.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

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
builder.Services.AddSwaggerGen();
// Read the connection string from appsettings.
string dbConnectionString = builder.Configuration.GetConnectionString("AppConnection");

builder.Services.AddTransient<IDbConnection>(sql => new MySqlConnection(dbConnectionString));

builder.Services.AddScoped<ICompanyB, CompanyB>(); // Custom Buisiness layer injection
builder.Services.AddRepositoryDependencies(); //Repository Layer Injection

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
