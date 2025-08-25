namespace ExternalIdApp.Services;

public class ClientService(HttpClient client)
{
    public Task<HttpResponseMessage> GetResponse()
    {
        return client.GetAsync("/ping");
    }
}