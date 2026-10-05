using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgAdventureGame.Backend.Database.SQLite.Base;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob
{
    internal class DbEngineWorkerJobMap : DbTableMap<DbEngineWorkerJob>
    {
        public override void Configure(EntityTypeBuilder<DbEngineWorkerJob> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(x => x.Active)
                .HasColumnName("Active")
                .IsRequired();

            builder.Property(x => x.LastRunTimestamp)
                .HasColumnName("LastRunTimestamp");

            builder.Property(x => x.LastSuccessfullRunTimestamp)
                .HasColumnName("LastSuccessfullRunTimestamp");

            builder.Property(x => x.NextRunTimestamp)
                .HasColumnName("NextRunTimestamp");

            builder.Property(x => x.ErrorCounter)
                .HasColumnName("ErrorCounter")
                .IsRequired();

            builder.Property(x => x.CronExpression)
                .HasColumnName("CronExpression")
                .IsRequired();
        }
    }
}
