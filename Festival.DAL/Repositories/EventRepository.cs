using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Festival.DAL.Repositories
{
    public class EventRepository:RepositoryBase<EventEntity>
    {

        public EventRepository(IDbContextFactory<FestivalDbContext> dbContextFactory) : base(dbContextFactory)
        {

        }

        public override EventEntity InsertOrUpdate(EventEntity entity)
        {
            using var dbContext = DbContextFactory.CreateDbContext();
            var otherEntities = dbContext.Set<EventEntity>()
                .Where(x => x.Id != entity.Id);
            bool isOverlapping = otherEntities.Any(x =>
                ((entity.StartTime >= x.StartTime && entity.StartTime < x.EndTime)
                 || (entity.EndTime > x.StartTime && entity.EndTime <= x.EndTime)
                 || (entity.StartTime <= x.StartTime && entity.EndTime >= x.EndTime))
                && entity.StageId == x.StageId);

            if (isOverlapping)
            {
                throw new ArgumentException("Čas vystoupení se překrývá s jiným vystoupením");
            }
            else if(entity.EndTime <= entity.StartTime)
            {
                throw new ArgumentException("Začátek vystoupení musí být dříve než konec");
            }
            
            return base.InsertOrUpdate(entity);
        }
    }
}
