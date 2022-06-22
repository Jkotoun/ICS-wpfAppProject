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
    public class EventFacade : FacadeBase<EventEntity,EventDetailModel, EventListModel>
    {
        public EventFacade(EventRepository repository, IMapper autoMapper) : base(repository, autoMapper)
        {
        }

        protected override Func<IQueryable<EventEntity>, IIncludableQueryable<EventEntity, object>>[] Includes
        {
            get;
        } = 
        {
            entities => entities.Include(x => x.Stage),
            entities => entities.Include(x =>x.Band)
        };
    }
}
