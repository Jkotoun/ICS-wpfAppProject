using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Festival.DAL.Factories
{
    public class SqlServerContextFactory : IDesignTimeDbContextFactory<FestivalDbContext>, IDbContextFactory<FestivalDbContext>
    {
        public FestivalDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = ProjectDB;Integrated Security=True");
            return new FestivalDbContext(builder.Options);
        }

        public FestivalDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = ProjectDB;Integrated Security=True");
            return new FestivalDbContext(builder.Options);
        }
    }
}