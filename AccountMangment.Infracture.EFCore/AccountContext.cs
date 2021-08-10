using AccountManagmentDomain.AccountAgg;
using AccountManagmentDomain.RoleAgg;
using AccountMangment.Infracture.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AccountMangment.Infracture.EFCore
{
    public class AccountContext:DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options):base(options)
        {

        }

        public DbSet<AccountDomain> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Assmbely = typeof(AccountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(Assmbely);
        }
    }
}
