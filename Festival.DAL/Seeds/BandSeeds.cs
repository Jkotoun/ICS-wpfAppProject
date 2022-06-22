using Festival.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.DAL.Seeds
{
    public static class BandSeeds
    {
        public static BandEntity Band1 = new BandEntity()
        {
            Id = Guid.NewGuid(),
            Name = "Banda 1",
            Description = "Mega super banda",
            ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Metallica_at_The_O2_Arena_London_2008.jpg/1200px-Metallica_at_The_O2_Arena_London_2008.jpg"
        };

        public static BandEntity Band2 = new BandEntity()
        {
            Id = Guid.NewGuid(),
            Name = "Banda 2",
            Description = "Fuj Fuj banda",
            ImgUrl = "https://scontent-prg1-1.xx.fbcdn.net/v/t1.6435-0/p526x296/45428212_322097681904122_8124368898846883840_n.jpg?_nc_cat=108&ccb=1-3&_nc_sid=8bfeb9&_nc_ohc=R2ImRaXPTQEAX_Avgv4&_nc_ht=scontent-prg1-1.xx&tp=6&oh=52dbae9db02f0817f70e76e26a65bc41&oe=60AF53FD",
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BandEntity>().HasData(new
            {
                Id=Band1.Id,
                Name = Band1.Name,
                Description = Band1.Description,
                ImgUrl = Band1.ImgUrl
            },new {
                Id = Band2.Id,
                Name = Band2.Name,
                Description = Band2.Description,
                ImgUrl = Band2.ImgUrl
            });
        }


        static BandSeeds()
        {
        }
    }
}
