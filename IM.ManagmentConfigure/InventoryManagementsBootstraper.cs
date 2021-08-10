using _01_QueryLamshade.ContractQurey;
using _01_QueryLamshade.Contracts.Inventory;
using IM.InventoryEF;
using IM.InventoryEF.Repository;
using InventoryApplication;
using InventoryApplicationContract.InventoryViewModel;
using inventoryManagmentDomain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IM.ManagmentConfigure
{
    public class InventoryManagementsBootstraper
    {
        public static void Configure(IServiceCollection service, string connectionstring)
        {
            service.AddTransient<IInventoryRepository, InvonteryRepository>();
            service.AddTransient<IInventoryApplication, InventoryApp>();

            service.AddTransient<IInventoryQuery, InventoryQuery>();

            service.AddDbContext<InventoryContext>(op => op.UseSqlServer(connectionstring));
        }
    }
}
