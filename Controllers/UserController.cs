using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    { private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController (IUserService userService , ILogger<UserController> logger) { 
            _userService = userService;
            _logger = logger;
        }
        

        [HttpGet("get-all")]
        [Authorize]
        public async Task <IActionResult> getAll()
        {
            try { var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception e) { 
            _logger.LogError(e.Message);
                return StatusCode(500 , e.Message);
            }
        }

    }
    
}
