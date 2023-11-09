using Dapper;
using DryCleanerAppDataAccess.Entities;
using DryCleanerAppDataAccess.Infrastructure;
using DryCleanerAppDataAccess.IRepository;
using DryCleanerAppDataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Repository
{
    public class CompanyRepository : ConnectionBase, ICompanyRepository
    {
        public CompanyRepository(string connectionString) : base(connectionString)
        {

        }
        public CompanyRepository(IDbConnection connection) : base(connection)
        {

        }
        public async Task<string> CreateCompany(CompanyEntity param)
        {
            DynamicParameters parm = new DynamicParameters();
            parm.Add("CompanyName", param.CompanyName);
            parm.Add("CompanyDescription", param.CompanyDescription);
            parm.Add("CompanyAddress", param.CompanyAddress);
            parm.Add("CompanyCity", param.CompanyCity);
            parm.Add("CompanyState", param.CompanyState);
            parm.Add("CompanyPhone", param.CompanyPhone);
            parm.Add("CompanyCountry", param.CompanyCountry);
            parm.Add("CompanyEmail", param.CompanyEmail);
            parm.Add("LandMark", param.LandMark);
            parm.Add("Place", param.Place);
            parm.Add("CreatedBy", param.CreatedBy);
            using (var Connection = GetConnection())
            {
                string Result = await Connection.ExecuteScalarAsync<string>("CreateCompany", parm, null, null, CommandType.StoredProcedure);
                return Result;
            }
        }
        public async Task<CompanyEntity?> GetCompanyById(int companyId)
        {
            string query = "SELECT `Id`,`CompanyName`,`CompanyDescription`,`CompanyAddress`,`CompanyCity`,`CompanyState`,`CompanyCountry`,`CompanyPhone`,`CompanyEmail`,`CreatedBy`," +
                   "`UpdatedBy`,`CreatedOn`,`UpdatedOn`,`IsDeleted`,`IsActive`,`LandMark`,`Place` FROM `companyEntities` where `companyEntities`.`Id`=" + companyId;
            using (var Connection = GetConnection())
            {
                var result = await Connection.QueryFirstOrDefaultAsync<CompanyEntity>(query);
                return result;
            }

        }
        public async Task<IEnumerable<CompanyEntity>> GetAll(CommonSearchParam param)
        {

            string query = "SELECT `Id`,`CompanyName`,`CompanyDescription`,`CompanyAddress`,`CompanyCity`,`CompanyState`,`CompanyCountry`,`CompanyPhone`,`CompanyEmail`,`CreatedBy`," +
                   "`UpdatedBy`,`CreatedOn`,`UpdatedOn`,`IsDeleted`,`IsActive`,`LandMark`,`Place` FROM `companyEntities` where `companyEntities`.`IsDeleted`=b'0' and `companyEntities`.`IsActive`=b'1'";

            if (param.TakeAll)
            {
                query += "  group BY companyEntities.`ID`  order by companyEntities.`Id` Desc";
            }
            else
            {
                query += "  group BY o.`ID`  order by o.`CreatedOn` Desc LIMIT " + param.SkipCount + " , " + param.TakeCount;

            }
            using (var Connection = GetConnection())
            {
                var result = await Connection.QueryAsync<CompanyEntity>(query);
                return result;
            }

        }
        public async Task<string?> GetUserFirstName(int userId)
        {

            string query = "SELECT `FirstName` FROM `users` where `users`.`Id`=" + userId;


            using (var Connection = GetConnection())
            {
                var result = await Connection.QueryFirstOrDefaultAsync<string>(query);
                return result;
            }


        }
        public async Task<string> UpdateCompany(CompanyListModel param)
        {
            DynamicParameters parm = new DynamicParameters();
            parm.Add("Id", param.Id);
            parm.Add("CompanyName", param.CompanyName);
            parm.Add("CompanyDescription", param.CompanyDescription);
            parm.Add("CompanyAddress", param.CompanyAddress);
            parm.Add("CompanyCity", param.CompanyCity);
            parm.Add("CompanyState", param.CompanyState);
            parm.Add("CompanyPhone", param.CompanyPhone);
            parm.Add("CompanyCountry", param.CompanyCountry);
            parm.Add("CompanyEmail", param.CompanyEmail);
            parm.Add("LandMark", param.LandMark);
            parm.Add("Place", param.Place);
            parm.Add("UpdatedBy", param.CreatedBy);
            using (var Connection = GetConnection())
            {
                string Result = await Connection.ExecuteScalarAsync<string>("UpdateCompany", parm, null, null, CommandType.StoredProcedure);
                return Result;
            }

        }
        public async Task<string> DeleteCompanyById(int companyId, int createdBy)
        {

            DynamicParameters parm = new DynamicParameters();
            parm.Add("Id", companyId);

            parm.Add("UpdatedBy", createdBy);
            using (var Connection = GetConnection())
            {
                string Result = await Connection.ExecuteScalarAsync<string>("DeleteCompany", parm, null, null, CommandType.StoredProcedure);
                return Result;
            }
        }

    }
}
