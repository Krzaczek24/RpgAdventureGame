using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgAdventureGame.Backend.Database.SQLite.Base;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.WorkerJob
{
    internal class DbWorkerJobTableMap : DbTableMap<DbWorkerJobTable>
    {
        public override void Configure(EntityTypeBuilder<DbWorkerJobTable> builder)
        {
            base.Configure(builder);
        }
    }
}
