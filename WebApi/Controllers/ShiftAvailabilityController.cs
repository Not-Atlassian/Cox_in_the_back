using Microsoft.AspNetCore.Mvc;
using WebApi.RepositoryInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ShiftAvailabilityController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamMemberAvailabilityRepository _teamMemberAvailabilityRepository;
        private readonly ISprintRepository _sprintRepository;

        public ShiftAvailabilityController(IUserRepository userRepository, ITeamMemberAvailabilityRepository teamMemberAvailabilityRepository, ISprintRepository sprintRepository)
        {
            _userRepository = userRepository;
            _teamMemberAvailabilityRepository = teamMemberAvailabilityRepository;
            _sprintRepository = sprintRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _teamMemberAvailabilityRepository.GetAll());
        }

        [HttpGet("{sprintId}/{userId}")]
        public async Task<IActionResult> GetAvailability(int sprintId, int userId)
        {
            var teamMemberAvailability = await _teamMemberAvailabilityRepository.GetBySprintIdAndUserId(sprintId, userId);
            return teamMemberAvailability != null ? Ok(teamMemberAvailability) : NotFound();
        }

    }
}