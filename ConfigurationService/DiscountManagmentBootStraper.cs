using Application;
using Application.Contract.ViewModel.ColleagueDiscountVM;
using Application.Contract.ViewModel.CustomerDiscountVM;
using DisCountDoamin.ColleagueDiscountAgg;
using DisCountDoamin.CustomerDisountAgg;
using Infractracer.EF;
using Infractracer.EF.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConfigurationService
{
    public class DiscountManagmentBootStraper
    {
        public static void Configure(IServiceCollection service,string connectionstring)
        {
            service.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
            service.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();

            service.AddTransient<IColleagueApplication, ColleagueDiscountApplication > ();
            service.AddTransient<IColleagueRepository, ColleagueDiscountRepository>();

            service.AddDbContext<DisCountContext>(x=>x.UseSqlServer(connectionstring));
        }
    }
}
