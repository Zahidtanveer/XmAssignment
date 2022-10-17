using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using XmAssignment.Common.Enums;

namespace XmAssignment.Common.Entities
{
    public class BtcPrice
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
        public PriceSource Soruce { get; set; }
    }

   
   
}
