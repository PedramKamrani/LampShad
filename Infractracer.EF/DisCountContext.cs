using DisCountDoamin.ColleagueDiscountAgg;
using DisCountDoamin.CustomerDisountAgg;
using Infractracer.EF.Mapping;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infractracer.EF
{
    public class DisCountContext:DbContext
    {
        public DisCountContext(DbContextOptions<DisCountContext> options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assambely = typeof(CustomerdiscountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assambely);
        }
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }
    }
}
