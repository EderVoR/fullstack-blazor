using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        void IEntityTypeConfiguration<Category>.Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(80);

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasColumnType("varchar")
                .HasMaxLength(255);

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(1600);
        }
    }
}
