using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnType("char(36)");
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(c => c.Goal)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
