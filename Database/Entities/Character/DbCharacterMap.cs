using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgAdventureGame.Database.SQLite.Base;

namespace RpgAdventureGame.Database.SQLite.Entities.Character
{
    internal class DbCharacterMap : DbTableMap<DbCharacter>
    {
        public override void Configure(EntityTypeBuilder<DbCharacter> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(e => e.CurrentAreaId)
                .HasColumnName("CurrentAreaId")
                .IsRequired();

            builder.HasOne(e => e.CurrentArea)
                .WithMany()
                .HasForeignKey(e => e.CurrentAreaId);
        }
    }
}
