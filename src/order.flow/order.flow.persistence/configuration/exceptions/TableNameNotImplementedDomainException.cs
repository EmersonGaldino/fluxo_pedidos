using System.Runtime.Serialization;

namespace order.flow.persistence.configuration.exceptions;

public class TableNameNotImplementedDomainException : Exception
{
    #region Methods
    public TableNameNotImplementedDomainException(string message) : base(message) { }
    public TableNameNotImplementedDomainException() : base() { }
    private TableNameNotImplementedDomainException(SerializationInfo info, StreamingContext context) { }
    #endregion
}