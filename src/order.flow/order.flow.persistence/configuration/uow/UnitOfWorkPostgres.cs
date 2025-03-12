using order.flow.utils.shared;

namespace order.flow.persistence.configuration.uow;

public class UnitOfWorkPostgres(string connectionString) : UnitOfWork(connectionString), IConnectionPostgres;