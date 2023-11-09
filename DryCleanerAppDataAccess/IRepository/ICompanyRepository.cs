using DryCleanerAppDataAccess.Entities;
using DryCleanerAppDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.IRepository
{
    public interface ICompanyRepository
    {
        Task<string> CreateCompany(CompanyEntity param);
        Task<IEnumerable<CompanyEntity>> GetAll(CommonSearchParam param);
        Task<CompanyEntity> GetCompanyById(int companyId);
        Task<string> DeleteCompanyById(int companyId, int updatedBy);
        Task<string> UpdateCompany(CompanyListModel company);
        Task<string?> GetUserFirstName(int userid);
    }
}
