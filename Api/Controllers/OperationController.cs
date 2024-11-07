using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetAllUsers() => Ok(_userService.GetAllUsers());

        [HttpGet("{id}")]
        public ActionResult<Users> GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            return user != null ? Ok(user) : NotFound("User not found.");
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] Users newUser)
        {
            if (!_userService.CreateUser(newUser))
                return BadRequest("User with this ID already exists.");
            
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPost("authenticate")]
        public ActionResult Authenticate([FromBody] LoginDto login)
        {
            return _userService.Authenticate(login.Email, login.PasswordHash)
                ? Ok("Authentication successful.")
                : Unauthorized("Invalid credentials.");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] Users updatedUser)
        {
            return _userService.UpdateUser(id, updatedUser) 
                ? NoContent() 
                : NotFound("User not found.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            return _userService.SoftDeleteUser(id) 
                ? NoContent() 
                : NotFound("User not found.");
        }
    }
}
