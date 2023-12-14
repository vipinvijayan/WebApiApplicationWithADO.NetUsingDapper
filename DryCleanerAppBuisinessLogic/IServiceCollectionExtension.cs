using DryCleanerAppDataAccess.IRepository;
using DryCleanerAppDataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppBuisinessLogic
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
        {
            //Always use Transient for multi threading purpose
            services.AddTransient(typeof(ICompanyRepository), typeof(CompanyRepository));
            services.AddTransient(typeof(ISecurityRepository), typeof(SecurityRepository));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            return services;
        }
    }
}
