using order.flow.utils.enums;

namespace order.flow.utils.service.htpp;

public interface IWebRequest
{
    Task<T?> RequestJsonSerialize<T>(
        string url,
        object jsonData,
        ETypeMethods method,
        string token = null,
        List<KeyValuePair<string, string>>? headers = null,
        IEnumerable<KeyValuePair<string, string>> parameters = null) where T : class;
}