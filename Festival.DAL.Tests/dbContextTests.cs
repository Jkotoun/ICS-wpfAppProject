using System;
using System.Linq;
using Festival.DAL.Entities;
using Festival.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Festival.DAL.Tests
{
    public class DbContextTests
    {
        private InMemoryContextFactory _dbContextFactory;
        [SetUp]
        public void Setup()
        {
            _dbContextFactory = new InMemoryContextFactory(nameof(DbContextTests));
        }

        [Test]
        public void InsertTestBandBasic()
        {
            //arrange
            var Band = new BandEntity
            {
                Country = "Cz",
                Description = "Kapela popis test",
                Genre = "dnb",
                Id = Guid.NewGuid(),
                ImgUrl = "png",
                Name = "kap",
                ShortDescription = "test"
            };
            //act
            using var context1 = _dbContextFactory.CreateDbContext();
            context1.Bands.Add(Band);
            context1.SaveChanges();

            //Assert
            using var context2 = _dbContextFactory.CreateDbContext();
            var retrievedBand = context2.Bands.Single(x => x.Id == Band.Id);
            Assert.AreEqual(Band, retrievedBand);

        }
        [Test]
        public void InsertTestBandRelatedEvents()
        {
            //arrange
            using var context1 = _dbContextFactory.CreateDbContext();
            var event_entity = new EventEntity
            {
                Id = Guid.NewGuid(),
                StartTime = new DateTime(15545646545),
                EndTime = new DateTime(548654987498),
                Band = new BandEntity()
                {
                    Country = "Cz",
                    Description = "Kapela popis test",
                    Genre = "dnb",
                    ImgUrl = "png",
                    Name = "kap",
                    ShortDescription = "test",
                    Id = Guid.NewGuid()
                },
                Stage = new StageEntity()
                {
                    Id = Guid.NewGuid(),
                    Description = "asd",
                    ImgUrl = "dg",
                    Name = "dsfg",
                }
            };
            //act
            context1.Events.Add(event_entity);
            context1.SaveChanges();
            //assert
            using var context2 = _dbContextFactory.CreateDbContext();
            var band_recv = context2.Bands.Include(ev => ev.Events).Single(x => x.Id == event_entity.Band.Id);
            Assert.AreEqual(band_recv.Events.First().StartTime, event_entity.StartTime);
        }

        [Test]
        public void BandUpdate()
        {
            //arrange
            var BandEntity = new BandEntity
            {
                Country = "cz",
                Description = "kapella",
                Genre = "rap",
                Id = Guid.NewGuid(),
                ImgUrl = "img",
                Name = "jmeno",
                ShortDescription = "popiskratky"
            };
            using var context1 = _dbContextFactory.CreateDbContext();
            context1.Bands.Add(BandEntity);
            context1.SaveChanges();
            //act
            using var context2 = _dbContextFactory.CreateDbContext();
            var band_selected = context2.Bands.Single(x => x.Id == BandEntity.Id);
            band_selected.Name = "prejmenovana";
            context2.SaveChanges();

            //assert
            using var context3 = _dbContextFactory.CreateDbContext();
            var changed_band = context3.Bands.Single(b => b.Id == BandEntity.Id);
            Assert.AreEqual(changed_band.Name, "prejmenovana");
            Assert.AreEqual(changed_band.ImgUrl, "img");
        }
        [Test]
        public void SimpleRemove()
        {
            //arrange
            using var cont1 = _dbContextFactory.CreateDbContext();
            var Stage2 = new StageEntity()
            {
                Id = Guid.NewGuid(),
                Description = "dh",
                ImgUrl = "dfhgdfh",
                Name = "dfgdfgh",
            };
            cont1.Stages.Add(Stage2);
            cont1.SaveChanges();

            using var cont2 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(cont2.Stages.Count(x => x.Id == Stage2.Id), 1);
            //act
            var removeIdObj = new StageEntity
            {
                Id = Stage2.Id
            };
            cont2.Remove(removeIdObj);
            cont2.SaveChanges();
            //assert
            using var cont3 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(cont3.Stages.Count(x => x.Id == Stage2.Id), 0);
        }

        [Test]
        public void CascadeRemove()
        {
            //arrange
            var stage = new StageEntity
            {
                Id = Guid.NewGuid(),
                Description = "popis2",
                ImgUrl = "sdf",
                Name = "jmenotest",
                Events =
                {
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        EndTime = new DateTime(856465),
                        StartTime = new DateTime(5645651)
                    },
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        EndTime = new DateTime(452453354),
                        StartTime = new DateTime(786768786876)
                    }
                }
            };
            using var cont1 = _dbContextFactory.CreateDbContext();
            cont1.Stages.Add(stage);
            cont1.SaveChanges();
            using var cont2 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(cont2.Events.ToList().Count, 2);

            //act
            var removeId = new StageEntity
            {
                Id = stage.Id
            };
            cont2.Remove(removeId);
            cont2.SaveChanges();

            //assert
            using var cont3 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(cont3.Events.ToList().Count, 0);
        }
    }
}