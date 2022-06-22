using Festival.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.App.Messages
{
    public class NewMessage<T> : Message<T>
        where T : ModelBase
    {
         
    }
}
