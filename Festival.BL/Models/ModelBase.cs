using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.BL.Models
{
    public abstract record ModelBase
    {
        public Guid Id { get; set; }
    }
}
