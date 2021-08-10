using CommenetManagmenrt.Infractracer.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System;

namespace CommenetManagmenrt.Infractracer.EFCore
{
    public class CommentContext :DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options):base(options)
        {

        }
        public DbSet<Commentmanagment.Domain.CommentAgg.Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assmbery = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assmbery);
        }
      
    }
}
