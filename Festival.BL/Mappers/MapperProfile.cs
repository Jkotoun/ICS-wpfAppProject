using AutoMapper;
using Festival.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.BL.Mappers
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<BandEntity, Models.BandDetailModel>();
            CreateMap<BandEntity, Models.BandListModel>();
            CreateMap<Models.BandDetailModel, BandEntity>();

            CreateMap<StageEntity, Models.StageDetailModel>();
            CreateMap<StageEntity, Models.StageListModel>();
            CreateMap<Models.StageDetailModel, StageEntity>();

            CreateMap<Models.EventDetailModel, EventEntity>();
            CreateMap<EventEntity, Models.EventDetailModel>();
            CreateMap<EventEntity, Models.EventListModel>();
        }
    }
}
