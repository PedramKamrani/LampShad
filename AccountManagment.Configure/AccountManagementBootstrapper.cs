using A.M.Application.AccountApp;
using A.M.Application.RoleApp;
using AccountManagment.Application.Contract.AccountVM;
using AccountManagment.Application.Contract.RoleVM;
using AccountManagmentDomain.AccountAgg;
using AccountManagmentDomain.RoleAgg;
using AccountMangment.Infracture.EFCore;
using AccountMangment.Infracture.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagment.Configure
{

    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountApplication , AccountApplication>();

            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }

    }
}

