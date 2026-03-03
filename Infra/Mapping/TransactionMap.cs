using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnType("char(36)");
            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(t => t.PersonId).HasColumnType("char(36)");
            builder.Property(t => t.CategoryId).HasColumnType("char(36)");

            builder.HasOne(t => t.Category)
               .WithMany()
               .HasForeignKey(t => t.CategoryId);

            builder.HasOne(t => t.Person)
                .WithMany()
                .HasForeignKey(t => t.PersonId);
        }
    }
}
