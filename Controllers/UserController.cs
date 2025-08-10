using bookFlow.Dto;
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
        [Authorize (Roles = "Admin")]
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


        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var result = await _userService.DeleteAsync(user);
                if (!result)
                {
                    return StatusCode(500, "An error occurred while deleting the user.");
                }

                return Ok($"User with ID {id} deleted successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while deleting user with ID {Id}", id);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound("User not found");

                // Update only allowed fields
                user.Username = dto.Username;
                user.FullName = dto.FullName;
                user.Email = dto.Email;

                var updated = await _userService.UpdateAsync(user);
                if (updated)
                    return Ok("User updated successfully");
                else
                    return StatusCode(500, "Failed to update user");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound("User not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user by ID");
                return StatusCode(500, ex.Message);
            }
        }




    }

}
