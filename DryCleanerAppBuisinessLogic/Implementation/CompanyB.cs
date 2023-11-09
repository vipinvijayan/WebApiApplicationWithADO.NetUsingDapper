using DryCleanerAppBuisinessLogic.Interfaces;
using DryCleanerAppDataAccess.Entities;
using DryCleanerAppDataAccess.IRepository;
using DryCleanerAppDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppBuisinessLogic.Implementation
{
    public class CompanyB : ICompanyB
    {
        readonly ICompanyRepository _companyRepository;
        public CompanyB(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public static string UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime.ToShortDateString();
        }

        public async Task<string> CreateCompany(CompanyParam param)
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
        public async Task<CompanyListModel> GetCompanyById(int companyId)
        {
            CompanyListModel companyMaster = new CompanyListModel();
            CompanyEntity companyData = await _companyRepository.GetCompanyById(companyId);
            if (companyData != null)
            {
                string? companyEmail = companyData.CompanyEmail;

                companyMaster.CompanyAddress = companyData.CompanyAddress;
                companyMaster.CompanyCity = companyData.CompanyCity;
                companyMaster.CompanyCountry = companyData.CompanyCountry;
                companyMaster.CompanyDescription = companyData.CompanyDescription;
                companyMaster.CompanyName = companyData.CompanyName;
                companyMaster.CompanyEmail = companyEmail;
                companyMaster.CompanyPhone = companyData.CompanyPhone;
                companyMaster.CompanyState = companyData.CompanyState;
                companyMaster.CreatedBy = companyData.CreatedBy;
                companyMaster.CreatedOn = UnixTimeStampToDateTime(companyData.CreatedOn);
                companyMaster.Id = companyData.Id;
                companyMaster.IsActive = companyData.IsActive ? "Active" : "Not Active";
                companyMaster.CreatedByName = companyData.CreatedBy > 0 ? await _companyRepository.GetUserFirstName(companyData.CreatedBy) : "";
                companyMaster.LandMark = companyData.LandMark;
                companyMaster.Place = companyData.Place;
            }
            return companyMaster;
        }
        public async Task<IEnumerable<CompanyListModel>> GetAll(CommonSearchParam param)
        {
            List<CompanyListModel> companyMasters = new List<CompanyListModel>();
            var companies = await _companyRepository.GetAll(param);
            foreach (var company in companies)
            {
                string? companyEmail = company.CompanyEmail;
                companyMasters.Add(new CompanyListModel()
                {
                    CompanyAddress = company.CompanyAddress,
                    CompanyCity = company.CompanyCity,
                    CompanyCountry = company.CompanyCountry,
                    CompanyDescription = company.CompanyDescription,
                    CompanyName = company.CompanyName,
                    CompanyEmail = companyEmail,
                    CompanyPhone = company.CompanyPhone,
                    CompanyState = company.CompanyState,
                    CreatedBy = company.CreatedBy,
                    CreatedOn = UnixTimeStampToDateTime(company.CreatedOn),
                    Id = company.Id,
                    IsActive = company.IsActive ? "Active" : "Not Active",
                    CreatedByName = company.CreatedBy > 0 ? await _companyRepository.GetUserFirstName(company.CreatedBy) : "",
                    LandMark = company.LandMark,
                    Place = company.Place
                });
            }
            return companyMasters;
        }
        public async Task<string> UpdateCompany(CompanyListModel company)
        {
            return await _companyRepository.UpdateCompany(company);
        }
        public async Task<string> DeleteCompanyById(int companyId, int createdBy)
        {
            return await _companyRepository.DeleteCompanyById(companyId, createdBy);
        }


    }
}
