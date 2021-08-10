using IM.Inventory.EF.Mapping;
using inventoryManagmentDomain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using System;

namespace IM.InventoryEF
{
    public class InventoryContext:DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options):base(options)
        {

        }
        public DbSet<InventoryDM> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assmbly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assmbly);
        }
    }
}
