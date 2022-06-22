using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Festival.BL.Facades;
using Festival.BL.Models;
using Festival.DAL.Entities;
using Festival.DAL.Factories;
using Festival.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Fest = Festival.BL.Models;

namespace Festival.BL.Tests
{
    class FacadeTests
    {
        private InMemoryContextFactory _contextFactory;
        private Mapper _mapper;
        
        [SetUp]
        public void Setup()
        {
            _contextFactory = new InMemoryContextFactory(nameof(FacadeTests));
            var cfg = new MapperConfiguration(cfg => cfg.AddProfile(new Mappers.MapperProfile()));
            _mapper = new Mapper(cfg);
        }

        [Test]
        public void InsertStage()
        {
            //arrange
            StageFacade stageFacade = new(new RepositoryBase<StageEntity>(_contextFactory), _mapper);
            StageDetailModel detailModel = new()
            {               
                Description = "Popis_test_insert_1",
                ImgUrl = "obr",
                Name = "stage 1",
                Events =
                {
                    new EventDetailModel()
                    {                       
                        StartTime = new DateTime(45665465),
                        EndTime = new DateTime(4554651456561)
                    }
                }
            };
            //act
            stageFacade.InsertOrUpdate(detailModel);

            //assert
            using var dbCont1 = _contextFactory.CreateDbContext();
            var result = dbCont1.Stages.Include(x=>x.Events).Single(x => x.Description == detailModel.Description);
            Assert.AreNotEqual(result.Id, Guid.Empty);
            Assert.AreEqual(detailModel.Name, result.Name);
            Assert.AreEqual(detailModel.Events.First().StartTime, result.Events.First().StartTime);
        }

        [Test]
        public void InsertAndUpdateBand()
        {
            //arrange
            BandFacade bandFacade = new(new RepositoryBase<BandEntity>(_contextFactory), _mapper);
            BandDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Country = "Starec",
                Description = "In the shower",
                Genre = "Popik",
                ImgUrl = "Vlasakovo facebook",
                Name = "18 Naked Cowboys",
                ShortDescription = "Vlasak is best fan",
                Events =
                {
                    new EventDetailModel()
                    {
                        Id = Guid.NewGuid(),
                        StartTime = new DateTime(123456789),
                        EndTime = new DateTime(1234567899)
                    }
                }
            };

            BandDetailModel detailModelChanged = new()
            {
                Id = detailModel.Id,
                Country = "Starec",
                Description = "In the showers of ram ranch",
                Genre = "Popik",
                ImgUrl = "Vlasakovo facebook",
                Name = "18 Naked Cowboys",
                ShortDescription = "Vlasak is fan",
                Events =
                {
                    new EventDetailModel()
                    {
                        Id = detailModel.Events.First().Id,
                        StartTime = new DateTime(123456789111),
                        EndTime = new DateTime(1234567899123458)
                    }
                }

            };

            using var dbAdd = _contextFactory.CreateDbContext();
            dbAdd.Bands.Add(_mapper.Map<BandEntity>(detailModel));
            dbAdd.SaveChanges();

            //act
            bandFacade.InsertOrUpdate(detailModelChanged);
            //assert

            using var dbCont2 = _contextFactory.CreateDbContext();
            var changedBand = dbCont2.Bands.Include(x => x.Events).Single(x => x.Id == detailModel.Id);
            Assert.AreEqual(changedBand.Description, detailModelChanged.Description);
            Assert.AreEqual(changedBand.Events.First().StartTime, detailModelChanged.Events.First().StartTime);

        }

        [Test]
        public void InsertAndUpdateEvent()
        {
            //arrange
            EventFacade EventFacade = new(new EventRepository(_contextFactory), _mapper);
            Fest.EventDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Band = new Fest.BandDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Country = "Bolívie",
                    Description = "nějací křováci idk",
                    Genre = "metal",
                    ImgUrl = "asdada",
                    Name = "asdad",
                    ShortDescription = "dsadasd",
                },
                Stage = new Fest.StageDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Description = "Stage u vchodu",
                    Name = "Stage c. 1",
                    ImgUrl = "obr.png"
                },
                StartTime = new DateTime(2021, 5, 24, 21, 50, 0),
                EndTime = new DateTime(2021, 5, 24, 22, 50, 0)
            };
            Fest.EventDetailModel eventChanged = new()
            {
                Band = new Fest.BandDetailModel()
                {
                    Id = detailModel.Band.Id,
                    Country = "Bolívie",
                    Description = "borci",
                    Genre = "dnb",
                    ImgUrl = "asdada",
                    Name = "Bozeny",
                    ShortDescription = "dsadasd",
                },
                Stage = new Fest.StageDetailModel()
                {
                    Id = detailModel.Stage.Id,
                    Description = "Stage u vchodu",
                    Name = "Stage u vychodu",
                    ImgUrl = "obr.png"
                },
                StartTime = new DateTime(2021, 5, 24, 21, 55, 0),
                EndTime = new DateTime(2021, 5, 24, 22, 50, 0),
                Id = detailModel.Id
            };
            using var dbAdd = _contextFactory.CreateDbContext();
            dbAdd.Events.Add(_mapper.Map<EventEntity>(detailModel));
            dbAdd.SaveChanges();

            //act
            EventFacade.InsertOrUpdate(eventChanged);

            //assert
            using var dbRetrieved = _contextFactory.CreateDbContext();
            var retrieved = dbRetrieved.Events.Include(x => x.Stage).Include(x => x.Band)
                .Single(x => x.Id == detailModel.Id);
            Assert.AreEqual(retrieved.StartTime, eventChanged.StartTime);
            Assert.AreEqual(retrieved.Stage.Name, eventChanged.Stage.Name);
            Assert.AreEqual(retrieved.Band.Name, eventChanged.Band.Name);
            Assert.AreEqual(retrieved.Band.Genre, eventChanged.Band.Genre);
        }

        [Test]
        public void DeleteStageById()
        {
            //arrange
            StageFacade stageFacade = new(new RepositoryBase<StageEntity>(_contextFactory), _mapper);
            StageDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Description = "Popis",
                ImgUrl = "obr",
                Name = "stage 1",
                Events =
                {
                    new EventDetailModel()
                    {
                        StartTime = new DateTime(45665465),
                        EndTime = new DateTime(4554651456561)
                    }
                }
            };
            using var dbAdd = _contextFactory.CreateDbContext();
            dbAdd.Stages.Add(_mapper.Map<StageEntity>(detailModel));
            dbAdd.SaveChanges();
            //act
            stageFacade.Delete(detailModel.Id);
            //assert
            using var dbCont1 = _contextFactory.CreateDbContext();
            var result = dbCont1.Stages.Count(x => x.Id == detailModel.Id);
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void DeleteBandById()
        {
            //arrange
            BandFacade bandFacade = new(new RepositoryBase<BandEntity>(_contextFactory), _mapper);
            BandDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Country = "Starec",
                Description = "In the shower",
                Genre = "Popik",
                ImgUrl = "Vlasakovo facebook",
                Name = "18 Naked Cowboys",
                ShortDescription = "Vlasak is best fan",
                Events =
                {
                    new EventDetailModel()
                    {
                        StartTime = new DateTime(45665465),
                        EndTime = new DateTime(4554651456561)
                    }
                }
            };
            using var dbContTest = _contextFactory.CreateDbContext();
            dbContTest.Bands.Add(_mapper.Map<BandEntity>(detailModel));
            dbContTest.SaveChanges();
            //act
            bandFacade.Delete(detailModel.Id);
            //assert
            using var dbCont1 = _contextFactory.CreateDbContext();
            var result = dbCont1.Bands.Count(x => x.Id == detailModel.Id);
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void DeleteEventById()
        {
            //arrange
            EventFacade EventFacade = new(new EventRepository(_contextFactory), _mapper);
            Fest.EventDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Band = new Fest.BandDetailModel()
                {
                    Country = "Bolívie",
                    Description = "nějací křováci idk",
                    Genre = "metal",
                    ImgUrl = "asdada",
                    Name = "asdad",
                    ShortDescription = "dsadasd",
                },
                Stage = new Fest.StageDetailModel()
                {
                    Description = "Stage u vchodu",
                    Name = "Stage c. 1",
                    ImgUrl = "obr.png"
                },
                StartTime = new DateTime(2021, 5, 24, 21, 50, 0),
                EndTime = new DateTime(2021, 5, 24, 22, 50, 0)
            };
            using var contAdd = _contextFactory.CreateDbContext();
            contAdd.Events.Add(_mapper.Map<EventEntity>(detailModel));
            contAdd.SaveChanges();

            //act
            EventFacade.Delete(detailModel.Id);

            //assert
            using var dbCont1 = _contextFactory.CreateDbContext();
            var result = dbCont1.Events.Count(x => x.Id == detailModel.Id);
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void DeleteStageByDetailModel()
        {
            //arrange
            StageFacade stageFacade = new(new RepositoryBase<StageEntity>(_contextFactory), _mapper);
            StageDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Description = "Popis",
                ImgUrl = "obr",
                Name = "stage 1",
                Events =
                {
                    new EventDetailModel()
                    {
                        StartTime = new DateTime(45665465),
                        EndTime = new DateTime(4554651456561)
                    }
                }
            };
            using var dbContAdd = _contextFactory.CreateDbContext();
            dbContAdd.Stages.Add(_mapper.Map<StageEntity>(detailModel));
            dbContAdd.SaveChanges();

            //act
            stageFacade.Delete(detailModel);

            //assert
            using var dbCont1 = _contextFactory.CreateDbContext();
            var result = dbCont1.Stages.Count(x => x.Id == detailModel.Id);
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void DeleteBandByDetailModel()
        {
            //arrange
            BandFacade bandFacade = new(new RepositoryBase<BandEntity>(_contextFactory), _mapper);
            BandDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Country = "Starec",
                Description = "In the shower",
                Genre = "Popik",
                ImgUrl = "Vlasakovo facebook",
                Name = "18 Naked Cowboys",
                ShortDescription = "Vlasak is best fan",
                Events =
                {
                    new EventDetailModel()
                    {
                        StartTime = new DateTime(45665465),
                        EndTime = new DateTime(4554651456561)
                    }
                }
            };
            using var dbContAdd = _contextFactory.CreateDbContext();
            dbContAdd.Bands.Add(_mapper.Map<BandEntity>(detailModel));
            dbContAdd.SaveChanges();

            //act
            bandFacade.Delete(detailModel);

            //assert
            using var dbCont1 = _contextFactory.CreateDbContext();
            var result = dbCont1.Bands.Count(x => x.Id == detailModel.Id);
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void DeleteStageByListModel()
        {
            //arrange
            StageFacade stageFacade = new(new RepositoryBase<StageEntity>(_contextFactory), _mapper);
            StageEntity stageEntity = new()
            {
                Id = Guid.NewGuid(),
                Description = "Popis",
                ImgUrl = "obr",
                Name = "stage 1",
                Events =
                {
                    new EventEntity()
                    {
                        StartTime = new DateTime(45665465),
                        EndTime = new DateTime(4554651456561)
                    }
                }
            };
            using var dbContAdd = _contextFactory.CreateDbContext();
            dbContAdd.Stages.Add(stageEntity);
            dbContAdd.SaveChanges();
            StageListModel listModel = _mapper.Map<StageListModel>(stageEntity);
            
            //act
            stageFacade.Delete(listModel);

            //assert
            using var dbCont1 = _contextFactory.CreateDbContext();
            var result = dbCont1.Stages.Count(x => x.Id == stageEntity.Id);
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void GetStageById()
        {
            //arrange
            StageFacade stageFacade = new(new RepositoryBase<StageEntity>(_contextFactory), _mapper);
            StageDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Description = "Popis",
                ImgUrl = "obr",
                Name = "stage 1",
            };
            using var dbAdd = _contextFactory.CreateDbContext();
            dbAdd.Stages.Add(_mapper.Map<StageEntity>(detailModel));
            dbAdd.SaveChanges();
            
            //act
            var retrieved = stageFacade.GetById(detailModel.Id);
            //assert

            Assert.AreEqual(retrieved.Id, detailModel.Id);
            Assert.AreEqual(detailModel.ImgUrl, retrieved.ImgUrl);
        }


        [Test]
        public void GetBandById()
        {
            //arrange
            BandFacade bandFacade = new(new RepositoryBase<BandEntity>(_contextFactory), _mapper);
            BandDetailModel detailModel = new()
            {
                Id = Guid.NewGuid(),
                Country = "Starec",
                Description = "In the shower",
                Genre = "Popik",
                ImgUrl = "Vlasakovo facebook",
                Name = "18 Naked Cowboys",
                ShortDescription = "Vlasak is best fan",
                Events =
                {
                    new EventDetailModel()
                    {
                        StartTime = new DateTime(45665465),
                        EndTime = new DateTime(4554651456561)
                    }
                }
            };
            using var bandAdd = _contextFactory.CreateDbContext();
            bandAdd.Bands.Add(_mapper.Map<BandEntity>(detailModel));
            bandAdd.SaveChanges();
            
            //act
            var result = bandFacade.GetById(detailModel.Id);
            
            //assert
            Assert.AreEqual(result.Id,detailModel.Id);
            Assert.AreEqual(detailModel.Name, result.Name);
            Assert.AreEqual(result.Events.First().StartTime, detailModel.Events.First().StartTime);
        }

        [Test]
        public void GetEventById()
        {
            //arrange
            EventFacade EventFacade = new(new EventRepository(_contextFactory), _mapper);
            Fest.EventDetailModel detailModel = new()
            {
                Id=Guid.NewGuid(),
                StartTime = new DateTime(2021, 5, 24, 21, 50, 0),
                EndTime = new DateTime(2021, 5, 24, 22, 50, 0),
                Band = new BandDetailModel()
                {
                    Id =  Guid.NewGuid()
                },
                Stage = new StageDetailModel()
                {
                    Id = Guid.NewGuid()
                }
            };
            using var dbContAdd = _contextFactory.CreateDbContext();
            dbContAdd.Events.Add(_mapper.Map<EventEntity>(detailModel));
            dbContAdd.SaveChanges();
            
            //act
            var retrieved = EventFacade.GetById(detailModel.Id);
            
            //assert
            Assert.AreEqual(retrieved.Id, detailModel.Id);
            Assert.AreEqual(retrieved.StartTime, detailModel.StartTime);
        }

        [Test]
        public void GetAllStages()
        {
            StageFacade facade = new StageFacade(new RepositoryBase<StageEntity>(_contextFactory), _mapper);
            StageEntity stage1 = new StageEntity()
            {
                Id = Guid.NewGuid(),
                Name = "jmeno1",
                Description = "popis stage 1",
                ImgUrl = "url1.png"
            };
            StageEntity stage2 = new StageEntity()
            {
                Id = Guid.NewGuid(),
                Name = "jmeno2",
                Description = "popis stage 2",
                ImgUrl = "url2.png"
            };
            using var dbContAdd = _contextFactory.CreateDbContext();
            dbContAdd.Stages.Add(stage1);
            dbContAdd.Stages.Add(stage2);
            dbContAdd.SaveChanges();

            //act
            var retrieved = facade.GetAllList();

            //assert
            Assert.True(retrieved.Count()>=2);
            Assert.AreEqual(retrieved.Single(x => x.Id == stage1.Id).Name, stage1.Name);
            Assert.AreEqual(retrieved.Single(x => x.Id == stage2.Id).Name, stage2.Name);
        }

        [Test]
        public void EventTimeOverlapInsert()
        {
            EventFacade facade = new EventFacade(new EventRepository(_contextFactory), _mapper);
            EventDetailModel existingModel = new EventDetailModel()
            {
                StartTime = new DateTime(2020, 10, 10, 10, 15, 0),
                EndTime = new DateTime(2020, 10, 10, 11, 15, 0),
                StageId = Guid.NewGuid()
            };
            EventDetailModel overlapEnd = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 10, 10, 10, 0),
                EndTime = new DateTime(2020, 10, 10, 11, 0, 0)
            };
            EventDetailModel overlapStart = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 10, 10, 30, 0),
                EndTime = new DateTime(2020, 10, 10, 11, 25, 0)
            };
            EventDetailModel insideOfExistingEvent = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 10, 10, 30, 0),
                EndTime = new DateTime(2020, 10, 10, 11, 0, 0)
            };
            EventDetailModel existingEventInside = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 10, 10, 0, 0),
                EndTime = new DateTime(2020, 10, 10, 12, 0, 0)
            };
            EventDetailModel sameStart = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 10, 10, 15, 0),
                EndTime = new DateTime(2020, 10, 10, 11, 00, 0)
            };
            EventDetailModel sameEnd = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 10, 10, 30, 0),
                EndTime = new DateTime(2020, 10, 10, 11, 15, 0)
            };
            EventDetailModel startSameAsEndOfExisting = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 10, 11, 15, 0),
                EndTime = new DateTime(2020, 10, 10, 12, 15, 0)
            };
            EventDetailModel endSameAsStartOfExisting = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 10, 9, 15, 0),
                EndTime = new DateTime(2020, 10, 10, 10, 15, 0)
            };
            EventDetailModel validEvent = new EventDetailModel()
            {
                StageId = existingModel.StageId,
                StartTime = new DateTime(2020, 10, 15, 9, 15, 0),
                EndTime = new DateTime(2020, 10, 15, 10, 15, 0)
            };
            EventDetailModel overlapDifferentStage = new EventDetailModel()
            {
                StageId = Guid.NewGuid(),
                StartTime = new DateTime(2020, 10, 10, 10, 10, 0),
                EndTime = new DateTime(2020, 10, 10, 11, 0, 0)
            };


            using var dbCont1 = _contextFactory.CreateDbContext();
            dbCont1.Events.Add(_mapper.Map<EventEntity>(existingModel));
            dbCont1.SaveChanges();

            Assert.Throws<ArgumentException>(() => facade.InsertOrUpdate(overlapEnd));
            Assert.Throws<ArgumentException>(() => facade.InsertOrUpdate(overlapStart));
            Assert.Throws<ArgumentException>(() => facade.InsertOrUpdate(insideOfExistingEvent));
            Assert.Throws<ArgumentException>(() => facade.InsertOrUpdate(existingEventInside));
            Assert.Throws<ArgumentException>(() => facade.InsertOrUpdate(sameStart));
            Assert.Throws<ArgumentException>(() => facade.InsertOrUpdate(sameEnd));
            Assert.DoesNotThrow(() => facade.InsertOrUpdate(startSameAsEndOfExisting));
            Assert.DoesNotThrow(() => facade.InsertOrUpdate(endSameAsStartOfExisting));
            Assert.DoesNotThrow(() => facade.InsertOrUpdate(validEvent));
            Assert.DoesNotThrow(() => facade.InsertOrUpdate(overlapDifferentStage));







        }


    }
}
