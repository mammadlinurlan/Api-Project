using ApiProject_Nurlan.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.DisplayStatus).HasDefaultValue(true);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.Image).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Language).HasMaxLength(50).IsRequired();
            builder.Property(c => c.PageCount).IsRequired();
            builder.Property(c => c.CostPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(c => c.SalePrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(c => c.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
