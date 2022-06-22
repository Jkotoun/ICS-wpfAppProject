using Festival.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.DAL.Seeds
{
    public static class EventSeeds
    {
        public static readonly EventEntity Event1 = new()
        {
            Id = Guid.NewGuid(),
            EndTime = new DateTime(2015, 10, 5, 15, 0, 0),
            StartTime = new DateTime(2015, 10, 5, 16, 0, 0),
            StageId = StageSeeds.Stage1.Id,
            BandId = BandSeeds.Band1.Id
            
        };

        public static readonly EventEntity Event2 = new()
        {
            Id = Guid.NewGuid(),
            EndTime = new DateTime(2015, 8, 5, 7, 0, 0),
            StartTime = new DateTime(2015, 5, 5, 8, 0, 0),
            StageId = StageSeeds.Stage1.Id,
            BandId = BandSeeds.Band2.Id
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventEntity>().HasData(new 
            {
                Id = Event1.Id,
                StartTime = Event1.StartTime,
                EndTime = Event1.EndTime,
                BandId = Event1.BandId,
                StageId = Event1.StageId
            }, new
            {
                Id = Event2.Id,
                StartTime = Event2.StartTime,
                EndTime = Event2.EndTime,
                BandId = Event2.BandId,
                StageId = Event2.StageId
            });
        }
    }
}
