using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("good")]
        public async Task<IActionResult> GetGood()
        {
            return Ok(await _userRepository.GetAllGood());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepository.GetById(id);
            return user != null ? Ok(user) : NotFound();
        }

        // get /user/availability_points/id
        [HttpGet("availability_points/{id}")]
        public async Task<IActionResult> GetAvailabilityPoints(int id)
        {
            return Ok(await _userRepository.GetAvailabilityPoints(id));
        }
    }
}