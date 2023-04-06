using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;

public class AuthorMapping : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(248);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(448);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(448);
        builder.Property(x => x.Birthdate).IsRequired();

        builder.HasMany(x => x.Books).WithMany(x => x.Authors);
    }
}
