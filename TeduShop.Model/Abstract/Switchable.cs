using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Abstract
{
    public abstract class Switchable : ISwitchable
    {
        public bool Status { get; set; }
    }
}
