using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.BL.Models
{
    public record EventDetailModel:ModelBase
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public StageDetailModel Stage { get; set; }
        public Guid StageId { get; set; }
        public BandDetailModel Band { get; set; }
        public Guid BandId { get; set; }
    }
}