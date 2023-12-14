
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
    public class SecurityRepository : ConnectionBase, ISecurityRepository
    {

        public SecurityRepository(string connectionString) : base(connectionString)
        {

        }
        public SecurityRepository(IDbConnection connection) : base(connection)
        {

        }


        public async Task<string> SaveRefreshToken(RefreshTokenEntity param)
        {

            DynamicParameters parm = new DynamicParameters();
            parm.Add("UserId", param.UserId);
            parm.Add("RefreshToken", param.RefreshToken);
            parm.Add("Expires", param.Expires);
            parm.Add("CreatedByID", param.CreatedByID);
            parm.Add("ReplaceByToken", param.ReplaceByToken);
            parm.Add("CompanyId", param.CompanyId);

            using (var Connection = GetConnection())
            {
                string Result = await Connection.ExecuteScalarAsync<string>("SaveRefreshToken", parm, null, null, CommandType.StoredProcedure);
                return Result;
            }
        }

        public async Task<bool> GetActiveStatusOfToken(string token)
        {

            string query = "SELECT count(id) FROM refreshTokenEntities where refreshToken='" + token + "' and IsActive=1 and IsDeleted=0;";


            using (var Connection = GetConnection())
            {
                var result = await Connection.ExecuteScalarAsync<int>(query);

                return result != 0;
            }


        }
    }
}
