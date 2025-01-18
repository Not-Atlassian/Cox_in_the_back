using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.RepositoryInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepository.GetById(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                user.UserId = default;
                await _userRepository.Add(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LogIn([FromBody]User user)
        {
            var userDb = await _userRepository.GetUserByName(user.Username);
            Console.WriteLine(user.Username);
            if (user == null)
            {
                return BadRequest(new { Message = "Invalid username" });
            }
            else if (user.Password != userDb.Password)
            {
                return BadRequest(new { Message = "Password is incorect" });
            }
            Globals.curretUser = user;
            return Ok(new {Message = "Login succes", User = userDb});
        }

        [HttpGet("currentUser")]
        public async Task<IActionResult> GetGlobalUser()
        {
            return Ok(Globals.curretUser);
        }

        [HttpGet("isLoggedIn")]
        public async Task<IActionResult> IsLoggedIn()
        {
            return Ok(Globals.curretUser.Username == null ? false : true);

        }

    }
}