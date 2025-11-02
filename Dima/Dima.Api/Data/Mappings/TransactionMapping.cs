using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        void IEntityTypeConfiguration<Transaction>.Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(255);

            builder.Property(x => x.Type)
                .HasColumnType("smallint");

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(x => x.CreatedAt)
                .IsRequired(true);

            builder.Property(x => x.PaidOrReceivedAt)
                .IsRequired(false);
            
            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(160);
        }
    }
}
