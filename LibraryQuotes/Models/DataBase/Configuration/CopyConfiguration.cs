using LibraryQuotes.Models.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryQuotes.Models.DataBase.Configuration
{
    public class CopyConfiguration
    {
        public CopyConfiguration(EntityTypeBuilder<Copy> entityBuilder)
        {
            entityBuilder.HasKey(entity => entity.CopyId);
            entityBuilder.Property(entity => entity.Name).IsRequired();
            entityBuilder.Property(entity => entity.Author).IsRequired();
            entityBuilder.Property(entity => entity.Price).IsRequired();
            entityBuilder.Property(entity => entity.Type).IsRequired();
        }
    }
}
