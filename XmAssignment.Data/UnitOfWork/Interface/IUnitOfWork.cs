using XmAssignment.Data.Repository.Interface;

namespace XmAssignment.Data.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBtcPriceRepositroy BtcPrice { get; }
        Task<int> SaveChanges();
    }
}
