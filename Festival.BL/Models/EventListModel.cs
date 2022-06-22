using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.BL.Models
{
    public record EventListModel : ModelBase
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public StageListModel Stage { get; set; }
        public Guid StageId { get; set; }
        public BandListModel Band { get; set; }
        public Guid BandId { get; set; }
    }
}