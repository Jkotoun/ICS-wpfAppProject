using Festival.DAL;
using Microsoft.EntityFrameworkCore;

namespace Festival.App.Factories
{
    class SqlServerDbContextFactory : IDbContextFactory<FestivalDbContext>
    {
        public FestivalDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = ProjectDB;Integrated Security=True");
            return new FestivalDbContext(builder.Options);
        }
    }
}
