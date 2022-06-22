using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.BL.Models;
namespace Festival.App.Messages
{
    class DeleteMessage<T> : Message<T>
        where T: ModelBase
    {
    }
}
