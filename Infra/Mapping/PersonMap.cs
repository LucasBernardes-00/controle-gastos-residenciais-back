using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnType("char(36)");
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.Birthday)
                .IsRequired();
        }
    }
}
