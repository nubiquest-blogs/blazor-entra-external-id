namespace AppContracts;

public class PingResponse
{
    public List<(string, string)> Claims { get; set; } = [];
    
    public required string Username { get; set; }
}