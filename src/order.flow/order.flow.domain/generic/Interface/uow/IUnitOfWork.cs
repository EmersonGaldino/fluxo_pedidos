using System.Data;

namespace order.flow.domain.generic.Interface.uow;

public interface IUnitOfWork : IDisposable
{
    void Begin();
    void Commit();
    void RollBack();
    IDbConnection GetConnection();
    IDbTransaction GetTransaction();
    void Release();
}