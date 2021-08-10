using Commentmanagment.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CommenetManagmenrt.Infractracer.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Commentmanagment.Domain.CommentAgg.Comment>
    {
        public void Configure(EntityTypeBuilder<Commentmanagment.Domain.CommentAgg.Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Email).HasMaxLength(500);
            builder.Property(x => x.Website).HasMaxLength(500);
            builder.Property(x => x.Message).HasMaxLength(1000);
            
        }
    }
}
