using System;
using System.Linq;
using Festival.DAL.Entities;
using Festival.DAL.Factories;
using Festival.DAL.Repositories;
using NUnit.Framework;

namespace Festival.DAL.Tests
{
    class RepositoryTests
    {
     
        private InMemoryContextFactory _dbContextFactory;


        [SetUp]
        public void Setup()
        {
            _dbContextFactory = new InMemoryContextFactory(nameof(RepositoryTests));
        }

        [TearDown]
        public void TearDown()
        {
            using var dbContext =_dbContextFactory.CreateDbContext();

            foreach(var currentEvent in dbContext.Events)
            {
                dbContext.Events.Remove(currentEvent);
            }
            foreach (var currentStage in dbContext.Stages)
            {
                dbContext.Stages.Remove(currentStage);
            }
            foreach (var currentBand in dbContext.Bands)
            {
                dbContext.Bands.Remove(currentBand);
            }

            dbContext.SaveChanges();
        }


        // -- Band tests
        [Test, Order(1)]
        public void InsertBandTest()
        {
            RepositoryBase<BandEntity> repository = new RepositoryBase<BandEntity>(_dbContextFactory);

            var newBand = new BandEntity
            {
                Country = "Bolívie",
                Description = "nějací křováci idk",
                Genre = "metal",
                ImgUrl = "asdada",
                Name = "asdad",
                ShortDescription = "dsadasd"
            };

            repository.InsertOrUpdate(newBand);
            using var dbContext = _dbContextFactory.CreateDbContext();
            var selectedBand = dbContext.Bands.First();

            Assert.AreEqual(selectedBand, newBand);

        }

        [Test]
        public void UpdateBandTest()
        {
            RepositoryBase<BandEntity> repository = new RepositoryBase<BandEntity>(_dbContextFactory);

            var bandToBeUpdated = new BandEntity
            {
                Id = Guid.NewGuid(),
                Country = "Kolumbie",
                Description = "Nová progresivní skupina.",
                Genre = "Pop",
                ImgUrl = "asdada",
                Name = "Bombarďáci",
                ShortDescription = "lol",
            };

            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Bands.Add(bandToBeUpdated);
            dbContext1.SaveChanges();

            bandToBeUpdated.ShortDescription = "Změněný popisek.";

            repository.InsertOrUpdate(bandToBeUpdated);

            using var dbContext2 = _dbContextFactory.CreateDbContext();
            var selectedBand = dbContext2.Bands.Single(x => x.Id == bandToBeUpdated.Id);

            Assert.AreEqual(selectedBand, bandToBeUpdated);

        }

        [Test]
        public void DeleteBandTest()
        {
            RepositoryBase<BandEntity> repository = new RepositoryBase<BandEntity>(_dbContextFactory);

            var bandToBeDeleted = new BandEntity
            {
                Id = Guid.NewGuid(),
                Country = "Čína",
                Description = "Skupina bez názoru a informací.",
                Genre = "Folk",
                ImgUrl = "jady jada jada",
                Name = "Pů mazel",
                ShortDescription = "no no no",
            };

            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Bands.Add(bandToBeDeleted);
            dbContext1.SaveChanges();

            using var dbContext2 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext2.Bands.Count(x => x.Id == bandToBeDeleted.Id), 1);

            repository.Delete(bandToBeDeleted);

            using var dbContext3 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext3.Bands.Count(x => x.Id == bandToBeDeleted.Id), 0);

        }

        [Test]
        public void DeleteByIdBandTest()
        {
            RepositoryBase<BandEntity> repository = new RepositoryBase<BandEntity>(_dbContextFactory);

            var bandToBeDeleted = new BandEntity
            {
                Id = Guid.NewGuid(),
                Country = "Francie",
                Description = "Žabí vzřeky a podivné zvuky.",
                Genre = "Jazz",
                ImgUrl = "pů dupupupu",
                Name = "Kvák",
                ShortDescription = "A je to tady",
            };

            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Bands.Add(bandToBeDeleted);
            dbContext1.SaveChanges();

            using var dbContext2 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext2.Bands.Count(x => x.Id == bandToBeDeleted.Id), 1);

            repository.DeleteById(bandToBeDeleted.Id);

            using var dbContext3 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext3.Bands.Count(x => x.Id == bandToBeDeleted.Id), 0);
        }

        [Test]
        public void GetByIdBandTest()
        {
            RepositoryBase<BandEntity> repository = new RepositoryBase<BandEntity>(_dbContextFactory);

            var bandToBeSelected = new BandEntity
            {
                Id = Guid.NewGuid(),
                Country = "Bolívie",
                Description = "Půdupů pupupup.",
                Genre = "rap",
                ImgUrl = "něco.někde.aco.jako",
                Name = "žbluňk",
                ShortDescription = "kratky",
            };

            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Bands.Add(bandToBeSelected);
            dbContext1.SaveChanges();

            var selectedBand=repository.GetById(bandToBeSelected.Id);
 
            Assert.AreEqual(selectedBand, bandToBeSelected);
        }

        [Test]
        public void GetAllEventsTest()
        {
            RepositoryBase<EventEntity> repository = new RepositoryBase<EventEntity>(_dbContextFactory);

            var bandToBeInserted = new BandEntity
            {
                Id = Guid.NewGuid(),
                Country = "Bolívie",
                Description = "Půdupů pupupup.",
                Events =
                {
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        StartTime = new DateTime(951357),
                        EndTime= new DateTime(951357)
                    },
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        StartTime = new DateTime(852369),
                        EndTime= new DateTime(741258)
                    },
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        StartTime = new DateTime(456321),
                        EndTime= new DateTime(985132)
                    }
                }
            };

            var stageToBeInserted = new StageEntity
            {
                Id = Guid.NewGuid(),
                Description = "nějaká stage",
                Events =
                {
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        StartTime = new DateTime(357681),
                        EndTime= new DateTime(56482)
                    },
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        StartTime = new DateTime(95254),
                        EndTime= new DateTime(21681)
                    },
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        StartTime = new DateTime(1658855),
                        EndTime= new DateTime(998877)
                    }
                }
            };


            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Stages.Add(stageToBeInserted);
            dbContext1.Bands.Add(bandToBeInserted);
            dbContext1.SaveChanges();

            using var dbContext2 = _dbContextFactory.CreateDbContext();
            var listOfAllEventsToBeSelected=dbContext2.Events.ToList();
            var listOfAllSelectedEvents = repository.GetAll();

            Assert.AreEqual(listOfAllSelectedEvents, listOfAllEventsToBeSelected);
        }

        [Test]
        public void EventsCascadeDeleteOnBandDelete()
        {
            RepositoryBase<BandEntity> repository = new RepositoryBase<BandEntity>(_dbContextFactory);

            var bandToBeDeleted = new BandEntity
            {
                Id = Guid.NewGuid(),
                Country = "Bolívie",
                Description = "Půdupů pupupup.",
                Genre = "rap",
                ImgUrl = "něco.někde.aco.jako",
                Name = "žbluňk",
                ShortDescription = "kratky",
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


            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Bands.Add(bandToBeDeleted);
            dbContext1.SaveChanges();

            using var dbContext2 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext2.Bands.Count(x => x.Id == bandToBeDeleted.Id), 1);
            Assert.AreEqual(dbContext2.Events.ToList().Count, 2);

            repository.Delete(bandToBeDeleted);

            //assert
            using var dbContext3 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext3.Bands.Count(x => x.Id == bandToBeDeleted.Id), 0);
            Assert.AreEqual(dbContext3.Events.ToList().Count, 0);

        }

        [Test]
        public void BandEventCascadeDeleteOnEventDelete()
        {
            RepositoryBase<EventEntity> repository = new RepositoryBase<EventEntity>(_dbContextFactory);

            var eventToBeDeleted = new EventEntity
            {
                Id = Guid.NewGuid(),
                EndTime = new DateTime(6874651),
                StartTime = new DateTime(64534)
            };

            var stage = new StageEntity
            {
                Id = Guid.NewGuid(),
                ImgUrl = "www.tojealestage.cz",
                Name = "Stage na kopcu",
                Description = "nejvíc nahře stage",
                Events =
                {
                    eventToBeDeleted,
                    new EventEntity
                    {
                        Id = Guid.NewGuid(),
                        EndTime = new DateTime(248524),
                        StartTime = new DateTime(2786834)
                    }
                }
            };


            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Stages.Add(stage);
            dbContext1.SaveChanges();

            using var dbContext2 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext2.Stages.Count(x => x.Id == stage.Id), 1);
            Assert.AreEqual(dbContext2.Events.ToList().Count, 2);

            repository.Delete(eventToBeDeleted);

            //assert
            using var dbContext3 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext3.Stages.Count(x => x.Id == stage.Id), 1);
            Assert.AreEqual(dbContext3.Events.ToList().Count, 1);

        }

        [Test]
        public void BandsCascadeUpdateOnEventBandInsert()
        {
            RepositoryBase<EventEntity> repository = new RepositoryBase<EventEntity>(_dbContextFactory);

            var BandToBeInserted = new BandEntity
            {
                Name = "Boříci",
                ImgUrl = "www.tojealestage.cz",
                Description = "Borci na druhou",
            };

            var eventToBeUpdated = new EventEntity
            {
                Id = Guid.NewGuid(),
                EndTime = new DateTime(6874651),
                StartTime = new DateTime(64534)
            };


            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Events.Add(eventToBeUpdated);
            dbContext1.SaveChanges();

            using var dbContext2 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext2.Events.Count(x => x.Id == eventToBeUpdated.Id), 1);
            Assert.AreEqual(dbContext2.Bands.ToList().Count, 0);

            eventToBeUpdated.Band = BandToBeInserted;

            repository.InsertOrUpdate(eventToBeUpdated);

            //assert
            using var dbContext3 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext3.Events.Count(x => x.Id == eventToBeUpdated.Id), 1);
            Assert.AreEqual(dbContext3.Bands.ToList().Count, 1);

        }

        [Test]
        public void StageEventCascadeUpdateOnEventUpdate()
        {
            RepositoryBase<EventEntity> repository = new RepositoryBase<EventEntity>(_dbContextFactory);
            
            var eventToBeUpdated = new EventEntity
            {
                Id = Guid.NewGuid(),
                EndTime = new DateTime(123456),
                StartTime = new DateTime(64534)
            };


            var StageToBeInserted = new StageEntity
            {
                Id = Guid.NewGuid(),
                Name = "updatovací stage",
                Description = "V teto stagi se zkontroluje, obsah jemu přiřazeného eventu.",
                ImgUrl = "fakt super obrazek",
                Events =
                {
                    eventToBeUpdated
                }
            };

            using var dbContext1 = _dbContextFactory.CreateDbContext();
            dbContext1.Stages.Add(StageToBeInserted);
            dbContext1.SaveChanges();

            using var dbContext2 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext2.Stages.Count(x => x.Id == StageToBeInserted.Id), 1);
            Assert.AreEqual(dbContext2.Events.ToList().Count, 1);

            eventToBeUpdated.EndTime = new DateTime(654321);

            repository.InsertOrUpdate(eventToBeUpdated);

            using var dbContext3 = _dbContextFactory.CreateDbContext();
            Assert.AreEqual(dbContext3.Stages.Count(x => x.Id == StageToBeInserted.Id), 1);
            Assert.AreEqual(dbContext3.Events.ToList().Count, 1);
            Assert.AreEqual(dbContext3.Events.First().EndTime, new DateTime(654321));
        }
    }
}
