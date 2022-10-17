using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmAssignment.Common.Enums
{
    public enum PriceSource
    {
        [Description("Bitfinex")]
        Bitfinex = 1,
        [Description("Bitstamp")]
        Bitstamp = 2
    }
}
