# WebApiApplication With Ado.net Using Dapper
webapi  Sample Application with ADO.net using Dapper
Step 1
Create new webapi project from visual studio
![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/dfaf808a-de9d-4cf1-83c5-770c6380421f)

 
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/44289c1e-5202-4c10-b50d-301f741ef8b5)

Figure 2 – Select Web API Template

![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/44801f29-0b1f-4bc9-bad3-995e4d858d94)

 
Figure 3 – Give project name and select location for saving project files
![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/6d208f6e-e704-4c00-95d1-1c97e4c6f1ac)

 
Figure 4 – Select Faremwork (.Net6) 

 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/2960ffb4-764e-474b-ad55-2342f189ec56)

Figure 5 – Project created you can check on the Solution explorer Right Side
If getting build errors Install Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.Tools, MySql.Data Using nuget package manager

To enable cross-origin requests on the apis add the below code In the middleware on program.cs file
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed((hosts) => true));
});
Add Connection string details on the program.cs file
// Read the connection string from appsettings.
string dbConnectionString = builder.Configuration.GetConnectionString("AppConnection");

builder.Services.AddTransient<IDbConnection>(sql => new MySqlConnection(dbConnectionString));
Here we need to add the Mysql.Data in to the application to resolve the error.
Add Class Library for Business logic Layer



Step 2
Create New Class Library for business logic related files to save.
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/b3a9c40a-0fee-4b0e-8c81-b53a3ecd383f)

Figure 1 – Select class library from project template list
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/b258e1c0-17fc-4260-b453-f9b847b87f5a)

Figure 2 – Giving Proper name and selecting proper Folder for class library
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/28d58086-4d63-4a09-a3f3-1993677c75a3)

Figure 3 – Select Framework (.Net6)
If getting build errors Install Microsoft.Extensions.DependencyInjection.Abstractions using nuget package manager

Step 3 
Create Class library for Data Access 
![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/2bce3e14-ba2e-417d-8ce8-56a2571ac032)

  Figure 1 – Select class library from project template list
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/23504984-293f-48aa-83c7-e784d88d01ec)

Figure 2 – Giving Proper name and selecting proper Folder for class library
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/d1c5724a-ce90-42bb-a8fb-6fdf02077e10)

Figure 3 – Select Framework (.Net6)
If getting build errors  Install Microsoft.EntityFrameworkCore ,Pomelo.EntityFrameworkCore.MySql, Microsoft.EntityFrameworkCore.Relational, on the dataccess layer using nuget package manager
After creating projects we can start with creating Db Connectionstring 
Add New abstract class named ConnectionBase in the folder for database based functionalities using IDbConnection Interface.
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/77e7044a-ab31-4cee-af92-26b6d39edc92)

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Infrastructure
{
    public abstract class ConnectionBase : IDbConnection
    {
        readonly string injectedConnectionString;
        public ConnectionBase(string connectionString)
        {
            this.injectedConnectionString = connectionString;
        }

        protected ConnectionBase(IDbConnection connection)
        {
            Connection = connection;
        }

        private IDbConnection Connection { get; set; }

        // Verbose but necessary implementation of IDbConnection:
        #region "IDbConnection implementation"

        public string ConnectionString
        {
            get
            {
                return Connection.ConnectionString;
            }

            set
            {
                Connection.ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                return Connection.ConnectionTimeout;
            }
        }

        public string Database
        {
            get
            {
                return Connection.Database;
            }
        }

        public ConnectionState State
        {
            get
            {
                return Connection.State;
            }
        }

        public IDbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return Connection.BeginTransaction(isolationLevel);
        }

        public void ChangeDatabase(string databaseName)
        {
            Connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            Connection.Close();
        }

        public IDbCommand CreateCommand()
        {
            return Connection.CreateCommand();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public void Open()
        {
            Connection.Open();
        }

        #endregion

        protected IDbConnection GetConnection(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("conneciton string is null or empty");

            return new MySqlConnection(connectionString);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IDbConnection GetConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                return GetConnection(Connection.ConnectionString);
            }
            else
            {
                if (Connection == null)
                    throw new ArgumentNullException("conneciton unable to inject");

                return Connection;

            }



        }

    }
}

Here we need to add the Mysql.Data in to the application to resolve the error.

  ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/14d99104-4793-455d-982a-93cc28c7a33d)
You can create new classes for using in application controllers in Model folder 

 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/692e7307-fab8-4999-9631-bb3360822cef)
Then Create Interface for Repository files to access from bussinesslogic layer ICompanyrepositry.

![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/ad9c6a44-52e5-417d-b2a0-21d6f7c6d79f)

Then Create Repository files to access data from database using Entity Framework named Companyrepositry.

Then We need to create buissiness classes .
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/0450995c-9922-43b7-b246-721ea0529ee5)

Create business files (Interface and its implementation) as show in the above figure.
 ![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/344073f4-bd42-4fb2-b8ac-5b1d146d9b00)

Business login Implementation class.
Used IRepository interface for getting data from database to the business layer.
The below code is populating entity class from the company model getting from the controller method.
    public async Task<string> CreateCompany(CompanyModel param)
    {
        var companyData = new CompanyEntity()
        {
            CompanyAddress = param.CompanyAddress,
            CompanyCity = param.CompanyCity,
            CompanyName = param.CompanyName,
            CompanyPhone = param.CompanyPhone,
            CompanyCountry = param.CompanyCountry,
            CompanyState = param.CompanyState,
            CompanyDescription = param.CompanyDescription,
            CompanyEmail = param.CompanyEmail,
            LandMark = param.LandMark,
            Place = param.Place,
            CreatedBy = param.CreatedBy,
        };
        return await _companyRepository.CreateCompany(companyData);
    }


 
Interface class for the business logic layer.

![image](https://github.com/vipinvijayan/WebApiApplicationWithAdo.netanDapper/assets/8413745/2976522c-dcac-414d-84fc-10644309d0ca)

Then we can inject repository layer and business layer to our api application middle ware for running the application.
Code in Program.cs
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


