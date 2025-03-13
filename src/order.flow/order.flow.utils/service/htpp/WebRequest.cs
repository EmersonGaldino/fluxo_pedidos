using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using order.flow.utils.enums;
using order.flow.utils.helpers;

namespace order.flow.utils.service.htpp;


public class WebRequest : IWebRequest
{
    private readonly HttpClient api;
    public WebRequest(HttpClient httpClient, HttpClient api)
    {
        this.api = httpClient;
    }
    public async Task<T?> RequestJsonSerialize<T>( 
            string url,
            object jsonData,
            ETypeMethods method,
            string? token = null,
            List<KeyValuePair<string, string>>? headers = null,
            IEnumerable<KeyValuePair<string, string>>? parameters = null) where T : class
        {
            Dispose();
            HttpResponseMessage? ret = null;
           
                if (!string.IsNullOrEmpty(token)) api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                        api.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
                
                ret = method switch
                {
                    ETypeMethods.GET => await api.GetAsync(url),
                    ETypeMethods.POST => await api.PostAsync(url,
                        new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json")),
                    ETypeMethods.PUT => await api.PutAsync(url,
                        new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json")),
                    ETypeMethods.DELETE => await api.DeleteAsync(url),
                    _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
                };
            var returnStr = await ret.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (ret.StatusCode != HttpStatusCode.OK && !ret.IsSuccessStatusCode)
            {
             
                throw new RequestException((int)ret.StatusCode, $"A chamada do servico retornou o erro {ret.StatusCode}.");
            }

            if (!ret.IsSuccessStatusCode || ret.StatusCode == HttpStatusCode.NoContent)
                throw new RequestException((int)ret.StatusCode, "Não foi possivel deserializar o objeto retornado pelo servico");

            if (string.IsNullOrEmpty(returnStr) && !ret.IsSuccessStatusCode) throw new RequestException((int)ret.StatusCode, returnStr);

            return JsonConvert.DeserializeObject<T>(returnStr);

        }

    #region .::Private Methods
    private void Dispose() => api.DefaultRequestHeaders.Clear();
    #endregion
}