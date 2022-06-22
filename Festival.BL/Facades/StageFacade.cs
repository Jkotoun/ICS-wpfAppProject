using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Festival.BL.Models;
using Festival.DAL.Entities;
using Festival.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Festival.BL.Facades
{
    public class StageFacade: FacadeBase<StageEntity,StageDetailModel, StageListModel>
    {
        public StageFacade(RepositoryBase<StageEntity> repository, IMapper autoMapper) : base(repository, autoMapper)
        {
        }

        protected override Func<IQueryable<StageEntity>, IIncludableQueryable<StageEntity, object>>[] Includes
        {
            get;
        } =
        {
            entities => entities.Include(x => x.Events)
        };
        }
}
