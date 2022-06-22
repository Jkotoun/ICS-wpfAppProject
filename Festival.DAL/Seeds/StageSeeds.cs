using Festival.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.DAL.Seeds
{
    public static class StageSeeds
    {
        public static StageEntity Stage1 = new StageEntity()
        {
            Id = Guid.NewGuid(),
            Description = "Stage s výhledem",
            ImgUrl = "https://c8.alamy.com/comp/BWHHRB/view-from-the-top-of-the-hill-at-the-top-of-the-park-stage-at-glastonbury-BWHHRB.jpg",
            Name = "Stage na kopcu"
        };
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StageEntity>().HasData(new
            {
                Id = Stage1.Id,
                Description = Stage1.Description,
                ImgUrl = Stage1.ImgUrl,
                Name = Stage1.Name
            });
            modelBuilder.Entity<StageEntity>().HasData(new
            {
                Id = Guid.NewGuid(),
                Description = "Stage se záchodem",
                ImgUrl = "https://shirokuma.blob.core.windows.net/osc/images-1/20140821solti-andras-a-toitoi-ceg.jpg",
                Name = "Stage na hajzlu"
            });
            modelBuilder.Entity<StageEntity>().HasData(new
            {
                Id = Guid.NewGuid(),
                Description = "Stage2",
                ImgUrl = "https://shirokuma.blob.core.windows.net/osc/images-1/20140821solti-andras-a-toitoi-ceg.jpg",
                Name = "Stage2"
            });
        }


        static StageSeeds()
        {
        }
    }
}
