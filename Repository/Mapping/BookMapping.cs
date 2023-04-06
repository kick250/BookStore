using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;

public class BookMapping : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(448);
        builder.Property(x => x.ISBN).IsRequired().HasMaxLength(448);
        builder.Property(x => x.ReleaseDate).IsRequired();
    }
}
