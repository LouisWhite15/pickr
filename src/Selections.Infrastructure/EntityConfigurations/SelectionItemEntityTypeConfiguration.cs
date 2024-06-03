using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Selections.Domain.Aggregates.SelectionAggregate;

namespace Selections.Infrastructure.EntityConfigurations;

public class SelectionItemEntityTypeConfiguration : IEntityTypeConfiguration<SelectionItem>
{
    public void Configure(EntityTypeBuilder<SelectionItem> builder)
    {
        builder.ToTable("selectionItems");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(b => b.Id);
    }
}