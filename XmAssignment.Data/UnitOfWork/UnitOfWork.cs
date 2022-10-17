using XmAssignment.Common.Entities;
using XmAssignment.Data.Repository;
using XmAssignment.Data.Repository.Interface;
using XmAssignment.Data.UnitOfWork.Interface;

namespace XmAssignment.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            BtcPrice = new BtcPriceRepositroy(_context);
        }
        
        public IBtcPriceRepositroy BtcPrice { get; private set; }
        
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
