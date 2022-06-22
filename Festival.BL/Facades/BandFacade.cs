using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Festival.BL.Models;
using Festival.DAL.Entities;
using Festival.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace Festival.BL.Facades
{
    public class BandFacade: FacadeBase<BandEntity,BandDetailModel, BandListModel>
    {
        public BandFacade(RepositoryBase<BandEntity> repository, IMapper autoMapper) : base(repository, autoMapper)
        {
        }

        protected override Func<IQueryable<BandEntity>, IIncludableQueryable<BandEntity, object>>[] Includes
        {
            get;
        } =
        {
            entities => entities.Include(x => x.Events)
        };
    }
}
