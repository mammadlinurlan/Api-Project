using ApiProject_Nurlan.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.Image).HasMaxLength(100).IsRequired();
            builder.Property(c => c.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
