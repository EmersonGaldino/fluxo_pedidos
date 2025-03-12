using System.Data;
using Npgsql;
using order.flow.domain.generic.Interface.uow;

namespace order.flow.persistence.configuration.uow;


public class UnitOfWork : IUnitOfWork
{
    #region Methods

    private readonly string _connectionString;
    private IDbConnection Connection;
    private IDbTransaction Transaction;

    protected UnitOfWork(string conectionString)
    {
        _connectionString = conectionString;
    }

    public void Begin()
    {
        if (Connection == null)
        {
            CreateConnection();
        }

        Transaction = Connection.BeginTransaction();
    }

    private void CreateConnection()
    {
        Connection = new NpgsqlConnection(_connectionString);
        Connection.Open();
    }

    public void Commit()
    {
        try
        {
            Transaction.Commit();
        }
        catch
        {
            Transaction?.Rollback();
            throw;
        }
        finally
        {
            Transaction?.Dispose();
            Transaction = null;
        }
    }

    public void RollBack()
    {
        try
        {
            Transaction.Rollback();
        }
        finally
        {
            Transaction?.Dispose();
            Transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Connection != null)
            {
                try
                {
                    Connection.Close();
                    Connection.Dispose();
                }
                catch
                {
                    //Não tratar
                }
            }
        }
    }

    ~UnitOfWork()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IDbConnection GetConnection()
    {
        AwaitCommand();
        if (Connection != null) return Connection;
        CreateConnection();
        return Connection;
    }

    public IDbTransaction GetTransaction()
    {
        return Transaction;
    }

    public void Release()
    {
        AwaitCommand();
    }

    private void AwaitCommand()
    {
        if (Connection == null) return;
        while (Connection.State == ConnectionState.Executing || Connection.State == ConnectionState.Fetching)
        {
            System.Threading.Thread.Sleep(500);
        }
    }

    #endregion
}