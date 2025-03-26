using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Models;

namespace Shelf.API.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        void IEntityTypeConfiguration<Transaction>.Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
       
            builder.HasKey(x => x.ID);

            builder.Property(x => x.Title)
                .IsRequired(true)
                .HasMaxLength(80)
                .HasColumnType("NVARCHAR");

            builder.Property(x => x.UserID)
                .IsRequired(true)
                .HasMaxLength(160)
                .HasColumnType("VARCHAR");

            builder.Property(x => x.Type)
                .IsRequired(true)
                .HasColumnType("SMALLINT"); // 0 - 255

            builder.Property(x => x.Amount)
                .IsRequired(true)
                .HasColumnType("MONEY");

            builder.Property(x => x.CreatedAt)
               .IsRequired(true); // DateTime2  

            builder.Property(x => x.PaidOrReceivedAt)
               .IsRequired(false);  
        }
    }
}
