using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgAdventureGame.Database.SQLite.Base;

namespace RpgAdventureGame.Database.SQLite.Entities.Area
{
    internal class DbAreaMap : DbTableMap<DbArea>
    {
        public override void Configure(EntityTypeBuilder<DbArea> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired();
        }
    }
}
