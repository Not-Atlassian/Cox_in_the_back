using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.Models;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly TeamRepository _teamRepository;
        public UserController(JyrosContext context)
        {
            _userRepository = new UserRepository(context);
            _teamRepository = new TeamRepository(context);
        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }
        [Route("GetByTeamId")]
        [HttpGet]
        public async Task<IActionResult> GetByTeamId(int teamId)
        {
            
            var team = await _teamRepository.GetById(teamId);
            return Ok(team.Users);
        }
    }
}