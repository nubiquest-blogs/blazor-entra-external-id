using AppContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

public class PingController : ControllerBase
{
    [HttpGet("/ping")]
    [Authorize]
    public IActionResult Ping()
    {
        var claims = User.Claims.ToList();
        PingResponse toReturn = new()
        {
            Claims = claims.Select(c => new PingClaim()
            {
                Type = c.Type,
                Value = c.Value
            }).ToList(),
            Username = User.Identity?.Name ?? "unknown",
        };
        return Ok(toReturn);
    }
}