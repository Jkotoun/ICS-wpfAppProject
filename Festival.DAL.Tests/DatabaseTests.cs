using System.Collections.Generic;
using System.Linq;
using Festival.DAL.Entities;
using Festival.DAL.Factories;
using NUnit.Framework;

namespace Festival.DAL.Tests
{
    class DatabaseTests
    {
        FestivalDbContext _context;

        
        [SetUp]
        public void Setup()
        {

            _context = new SqlServerContextFactory().CreateDbContext();
        }
        [Test]
        public void BandConnection()
        {
           Assert.IsInstanceOf<List<BandEntity>>(_context.Bands.ToList());
        }

        [Test]
        public void EventConnection()
        {
            Assert.IsInstanceOf<List<EventEntity>>(_context.Events.ToList());
        }

        [Test]
        public void StageConnection()
        {
            Assert.IsInstanceOf<List<StageEntity>>(_context.Stages.ToList());
        }

    }
}
