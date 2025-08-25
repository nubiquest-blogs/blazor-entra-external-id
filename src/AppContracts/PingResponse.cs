namespace AppContracts;

public class PingResponse
{
    public List<PingClaim> Claims { get; set; } = [];
    
    public required string Description { get; set; }
    
    public required string Username { get; set; } 
}

public class PingClaim
{
    public required string Type { get; set; } = string.Empty;
    public required string Value { get; set; } = string.Empty;
}