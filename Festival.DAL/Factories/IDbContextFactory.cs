using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.DAL.Factories
{
    public interface IDbContextFactory
    {
        public FestivalDbContext CreateDbContext();
    }
}
