using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    public UserController()
    {
        
    }

    [HttpGet("GetUsers/{testValue}")]
    public IEnumerable<User> GetUsers(string testValue)
    {
       
    }
}
