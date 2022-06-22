using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.BL.Models
{
    public record BandDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string ShortDescription { get; set; }
        public ICollection<EventDetailModel> Events { get; set; } = new List<EventDetailModel>();
    }
}
