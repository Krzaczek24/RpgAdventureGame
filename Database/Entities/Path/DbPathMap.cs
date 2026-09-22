using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgAdventureGame.Database.SQLite.Base;

namespace RpgAdventureGame.Database.SQLite.Entities.Path
{
    internal class DbPathMap : DbTableMap<DbPath>
    {
        public override void Configure(EntityTypeBuilder<DbPath> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(x => x.StartAreaId)
                .HasColumnName("StartAreaId")
                .IsRequired();

            builder.HasOne(x => x.StartArea)
                .WithMany(a => a.OutgoingPaths)
                .HasForeignKey(a => a.StartAreaId);

            builder.Property(x => x.EndAreaId)
                .HasColumnName("EndAreaId")
                .IsRequired();

            builder.HasOne(x => x.EndArea)
                .WithMany()
                .HasForeignKey(a => a.EndAreaId);

            builder.Property(x => x.Distance)
                .HasColumnName("Distance")
                .IsRequired();

            builder.Property(x => x.DangerLevel)
                .HasColumnName("DangerLevel")
                .IsRequired();

            builder.Property(x => x.DangerProbability)
                .HasColumnName("DangerProbability")
                .IsRequired();
        }
    }
}
