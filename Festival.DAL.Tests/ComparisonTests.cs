using System;
using Festival.DAL.Entities;
using Festival.DAL.Factories;
using Nemesis.Essentials.Design;
using NUnit.Framework;

namespace Festival.DAL.Tests
{
    public class ComparisonTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BandEntityEquals()
        {
            var band1 = new BandEntity
            {
                Country = "Bolívie",
                Description = "nějací křováci idk",
                Genre = "metal",
                Id = Guid.NewGuid(),
                ImgUrl = "asdada",
                Name = "asdad",
                ShortDescription = "dsadasd",


            };
            band1.Events = new ValueCollection<EventEntity>(EventEntity.EventWithoutStageAndBandComparer)
                {
                    new EventEntity
                    {
                        BandId = band1.Id,
                        EndTime = new DateTime(265615665),
                        StartTime = new DateTime(15151656)
                    }
                };
            var band2 = new BandEntity
            {
                Country = band1.Country,
                Description = band1.Description,
                Genre = band1.Genre,
                Id = band1.Id,
                ImgUrl = band1.ImgUrl,
                Name = band1.Name,
                ShortDescription = band1.ShortDescription,
                Events = band1.Events
            };
            band2.Events = new ValueCollection<EventEntity>(EventEntity.EventWithoutStageAndBandComparer)
                {
                    new EventEntity
                    {
                        BandId = band1.Id,
                        EndTime = new DateTime(265615665),
                        StartTime = new DateTime(15151656)
                    }
                };

            Assert.True(band2 == band1);
        }
    }
}