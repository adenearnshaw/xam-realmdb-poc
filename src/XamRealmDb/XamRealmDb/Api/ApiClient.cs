using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace XamRealmDb.Api;

public class ApiClient
{
    public async Task<IEnumerable<DataObjectDto>> FetchLatestValue()
    {
        try
        {
            var client = new HttpClient(GetInsecureHandler());
            var request = new HttpRequestMessage(HttpMethod.Get, "https://10.0.2.2:7224/latest");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return new List<DataObjectDto>();

            return JsonSerializer.Deserialize<List<DataObjectDto>>(await response.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public async Task UpdateLatestValue()
    {
        try
        {
            var client = new HttpClient(GetInsecureHandler());
            var request = new HttpRequestMessage(HttpMethod.Post, "https://10.0.2.2:7224/latest?source=APP");

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private HttpClientHandler GetInsecureHandler()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
    }
}
