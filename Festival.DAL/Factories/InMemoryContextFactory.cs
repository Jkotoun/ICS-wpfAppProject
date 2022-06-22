using Microsoft.EntityFrameworkCore;

namespace Festival.DAL.Factories
{
    public class InMemoryContextFactory : IDbContextFactory<FestivalDbContext>
    {
        private readonly string _dbName;
        public InMemoryContextFactory(string databaseName = "testDB")
        {
            _dbName = databaseName;
        }
        public FestivalDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(_dbName);
            return new FestivalDbContext(builder.Options);
        }
    }
}
