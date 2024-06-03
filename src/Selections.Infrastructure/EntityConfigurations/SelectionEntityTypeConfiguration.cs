using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Selections.Domain.Aggregates.SelectionAggregate;

namespace Selections.Infrastructure.EntityConfigurations;

public class SelectionEntityTypeConfiguration : IEntityTypeConfiguration<Selection>
{
    public void Configure(EntityTypeBuilder<Selection> builder)
    {
        builder.ToTable("selections");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(b => b.Id);
        
        builder
            .Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(30);
    }
}