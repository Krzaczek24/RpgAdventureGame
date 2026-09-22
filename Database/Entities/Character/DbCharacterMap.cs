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

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(x => x.CurrentAreaId)
                .HasColumnName("CurrentAreaId")
                .IsRequired();

            builder.HasOne(x => x.CurrentArea)
                .WithMany(a => a.Characters)
                .HasForeignKey(a => a.CurrentAreaId);
        }
    }
}
