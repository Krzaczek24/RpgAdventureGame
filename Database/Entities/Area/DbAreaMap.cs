using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgAdventureGame.Backend.Database.SQLite.Base;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.Area
{
    internal class DbAreaMap : DbTableMap<DbArea>
    {
        public override void Configure(EntityTypeBuilder<DbArea> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.HasMany(x => x.Characters)
                .WithOne(c => c.CurrentArea)
                .HasForeignKey(c => c.CurrentAreaId);

            builder.HasMany(x => x.OutgoingPaths)
                .WithOne(p => p.StartArea)
                .HasForeignKey(p => p.StartAreaId);
        }
    }
}
