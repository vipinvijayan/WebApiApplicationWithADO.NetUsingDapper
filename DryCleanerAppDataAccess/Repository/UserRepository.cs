using Dapper;
using DryCleanerAppDataAccess.Entities;
using DryCleanerAppDataAccess.Infrastructure;
using DryCleanerAppDataAccess.IRepository;
using DryCleanerAppDataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Repository
{
    public class UserRepository : ConnectionBase, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {

        }
        public UserRepository(IDbConnection connection) : base(connection)
        {

        }
        public async Task<string> RegisterUser(UserEntity param, int companyId)
        {

            DynamicParameters parm = new DynamicParameters();
            parm.Add("FirstName", param.FirstName);
            parm.Add("LastName", param.LastName);
            parm.Add("Email", param.Email);
            parm.Add("MobileNo", param.MobileNo);
            parm.Add("CreatedBy", param.CreatedBy);
            parm.Add("Username", param.Username);
            parm.Add("Password", param.Password);
            parm.Add("CompanyId", companyId);

            parm.Add("CreatedBy", param.CreatedBy);
            using (var Connection = GetConnection())
            {
                string Result = await Connection.ExecuteScalarAsync<string>("UserRegistration", parm, null, null, CommandType.StoredProcedure);
                return Result;
            }
        }


        public async Task<UserProfileModel?> UserLogin(UserLoginParams param, int companyId)
        {

            DynamicParameters parm = new DynamicParameters();
            parm.Add("UserName", param.Username);
            parm.Add("Password", param.Password);
            parm.Add("UserAgent", param.UserAgent);
            parm.Add("CompanyId", companyId);


            using (var Connection = GetConnection())
            {
                UserProfileModel? Result = await Connection.QueryFirstOrDefaultAsync<UserProfileModel>("UserLogin", parm, null, null, CommandType.StoredProcedure);
                return Result;
            }
        }
        public async Task<string> DeleteAllRefreshTokenOfUser(int userId)
        {

            DynamicParameters parm = new DynamicParameters();
            parm.Add("UserId", userId);


            using (var Connection = GetConnection())
            {
                return await Connection.ExecuteScalarAsync<string>("DeleteAllRefreshTokenOfUser", parm, null, null, CommandType.StoredProcedure);

            }
        }
    }
}
