namespace ExternalIdApp.Services;

public class ClientService(HttpClient client)
{
    public Task<HttpResponseMessage> Call(string address)
    {
        return client.GetAsync(address);
    }
   
}