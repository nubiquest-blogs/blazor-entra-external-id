using AppContracts;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

public class PingController : ControllerBase
{

    [HttpGet("/ping")]
    public IActionResult Ping()
    {
        PingResponse toReturn = new()
        {
            Username = "Unknown"
        };
        return Ok(toReturn);
    }
}