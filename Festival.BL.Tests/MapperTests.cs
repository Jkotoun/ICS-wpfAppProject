using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Festival.BL.Models;
using Festival.DAL.Entities;

namespace Festival.BL.Tests
{
    public class MapperTests
    {
        AutoMapper.IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            var cfg = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(new Mappers.MapperProfile()));
            _mapper = new AutoMapper.Mapper(cfg);
        }

        [Test]
        public void MapBandToDetail()
        {
            var band = new BandEntity
            {
                Country = "Bolívie",
                Description = "nějací křováci idk",
                Genre = "metal",
                ImgUrl = "asdada",
                Name = "asdad",
                ShortDescription = "dsadasd",
            };
            var detailModel = _mapper.Map<Models.BandDetailModel>(band);
            Assert.AreEqual(band.Country, detailModel.Country);
            Assert.AreEqual(band.Description, detailModel.Description);
            Assert.AreEqual(band.Genre, detailModel.Genre);
            Assert.AreEqual(band.ImgUrl, detailModel.ImgUrl);
            Assert.AreEqual(band.Name, detailModel.Name);
            Assert.AreEqual(band.ShortDescription, detailModel.ShortDescription);
        }
        [Test]
        public void MapBandToList()
        {
            var band = new BandEntity
            {
                Country = "Bolívie",
                Description = "nějací křováci idk",
                Genre = "metal",
                ImgUrl = "asdada",
                Name = "asdad",
                ShortDescription = "dsadasd",
            };
            var listModel = _mapper.Map<Models.BandListModel>(band);
            Assert.AreEqual(listModel.Genre, band.Genre);
            Assert.AreEqual(listModel.Name, band.Name);
            Assert.AreEqual(listModel.Id, band.Id);
        }

        [Test]
        public void MapStageToDetail()
        {
            var stage = new StageEntity()
            {
                Description = "Stage u vchodu",
                Id = Guid.NewGuid(),
                Name = "Stage c. 1",
                ImgUrl = "obr.png",
            };
            stage.Events = new List<EventEntity>()
            {
                new EventEntity()
                {
                    Id = Guid.NewGuid(),
                    StartTime = new DateTime(2021, 5, 24, 20, 50, 0),
                    EndTime = new DateTime(2021, 5, 24, 21, 50, 0),
                },
                new EventEntity()
                {
                    Id = Guid.NewGuid(),
                    StartTime = new DateTime(2021, 5, 24, 21, 50, 0),
                    EndTime = new DateTime(2021, 5, 24, 22, 50, 0),
                }
            };
            var stageDetailModel = _mapper.Map<Models.StageDetailModel>(stage);

            Assert.AreEqual(stage.Description, stageDetailModel.Description);
            Assert.AreEqual(stage.Id, stageDetailModel.Id);
            Assert.AreEqual(stage.Name, stageDetailModel.Name);
            Assert.AreEqual(stage.ImgUrl, stageDetailModel.ImgUrl);
            Assert.AreEqual(stage.Events.First().StartTime, stageDetailModel.Events.First().StartTime);
        }
        [Test]
        
        public void EventToDetail()
        { 
            var ev = new EventEntity()
            {
                Id = Guid.NewGuid(),
                Band = new BandEntity()
                {
                    Id = Guid.NewGuid(),
                    Country = "Bolívie",
                    Description = "nějací křováci idk",
                    Genre = "metal",
                    ImgUrl = "asdada",
                    Name = "asdad",
                    ShortDescription = "dsadasd",
                },
                Stage = new StageEntity()
                {
                    Description = "Stage u vchodu",
                    Id = Guid.NewGuid(),
                    Name = "Stage c. 1",
                    ImgUrl = "obr.png"
                },
                StartTime = new DateTime(2021, 5, 24, 21, 50, 0),
                EndTime = new DateTime(2021, 5, 24, 22, 50, 0)
            };
            ev.StageId = ev.Stage.Id;
            ev.BandId= ev.Band.Id;
            var eventModel = _mapper.Map<Models.EventDetailModel>(ev);
            Assert.AreEqual(eventModel.Band.Name, ev.Band.Name);
            Assert.AreEqual(eventModel.Stage.Name, ev.Stage.Name);
            Assert.AreEqual(eventModel.StartTime, ev.StartTime);
            Assert.AreEqual(eventModel.EndTime, ev.EndTime);
            Assert.AreEqual(eventModel.StageId, ev.StageId);
            Assert.AreEqual(eventModel.BandId, ev.BandId);
            Assert.AreEqual(eventModel.Id, ev.Id);
        }

        [Test]
        public void MapEventDetailToEventEntity()
        {
            var ev = new EventDetailModel()
            {
                Id = Guid.NewGuid(),
                Band = new BandDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Country = "Bolívie",
                    Description = "nějací křováci idk",
                    Genre = "metal",
                    ImgUrl = "asdada",
                    Name = "asdad",
                    ShortDescription = "dsadasd",
                },
                Stage = new StageDetailModel()
                {
                    Description = "Stage u vchodu",
                    Id = Guid.NewGuid(),
                    Name = "Stage c. 1",
                    ImgUrl = "obr.png"
                },
                StartTime = new DateTime(2021, 5, 24, 21, 50, 0),
                EndTime = new DateTime(2021, 5, 24, 22, 50, 0)
            };
            ev.StageId = ev.Stage.Id;
            ev.BandId = ev.Band.Id;
            var eventEntity = _mapper.Map<EventEntity>(ev);
            Assert.AreEqual(eventEntity.Band.Name, ev.Band.Name);
            Assert.AreEqual(eventEntity.Stage.Name, ev.Stage.Name);
            Assert.AreEqual(eventEntity.StartTime, ev.StartTime);
            Assert.AreEqual(eventEntity.EndTime, ev.EndTime);
            Assert.AreEqual(eventEntity.StageId, ev.StageId);
            Assert.AreEqual(eventEntity.BandId, ev.BandId);
            Assert.AreEqual(eventEntity.Id, ev.Id);

        }

    }
}