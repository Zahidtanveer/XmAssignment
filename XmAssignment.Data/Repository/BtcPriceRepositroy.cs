using XmAssignment.Common.Entities;
using XmAssignment.Data.Repository.Interface;

namespace XmAssignment.Data.Repository
{
    public class BtcPriceRepositroy : Repository<BtcPrice>, IBtcPriceRepositroy
    {
        public BtcPriceRepositroy(ApplicationDbContext context) : base(context)
        {
        }
    }
}
