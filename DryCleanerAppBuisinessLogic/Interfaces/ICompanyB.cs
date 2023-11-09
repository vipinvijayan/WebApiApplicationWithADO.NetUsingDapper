using DryCleanerAppDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppBuisinessLogic.Interfaces
{
    public interface ICompanyB
    {
        Task<string> CreateCompany(CompanyParam param);
        Task<IEnumerable<CompanyListModel>> GetAll(CommonSearchParam param);
        Task<CompanyListModel> GetCompanyById(int companyId);
        Task<string> DeleteCompanyById(int companyId, int createdBy);
        Task<string> UpdateCompany(CompanyListModel company);
    }
}
