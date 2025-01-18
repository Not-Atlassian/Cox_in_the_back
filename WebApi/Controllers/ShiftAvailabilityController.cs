using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
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
        private readonly IAdjustmentRepository _adjustmentRepository;

        public ShiftAvailabilityController(IUserRepository userRepository, ITeamMemberAvailabilityRepository teamMemberAvailabilityRepository, ISprintRepository sprintRepository, IAdjustmentRepository adjustmentRepository)
        {
            _userRepository = userRepository;
            _teamMemberAvailabilityRepository = teamMemberAvailabilityRepository;
            _sprintRepository = sprintRepository;
            _adjustmentRepository = adjustmentRepository;
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

        [HttpGet("{sprintId}/adjustment")]
        public async Task<IActionResult> GetAdjustmentLength(int sprintId)
        {
            var adjustments = await _adjustmentRepository.GetAll();
            var filteredAdjustments = adjustments.Where(a => a.SprintId == sprintId).ToList();

            var totalAdjustment = filteredAdjustments.Sum(a => a.AdjustmentPoints);

            return Ok(totalAdjustment);
        }

        [HttpGet("{sprintId}/adjustment/all")]
        public async Task<IActionResult> GetAllAdjustments(int sprintId)
        {
            var adjustments = await _adjustmentRepository.GetAll();
            var filteredAdjustments = adjustments.Where(a => a.SprintId == sprintId).ToList();
            return Ok(filteredAdjustments);
        }


        [HttpGet("sprints")]
        public async Task<IActionResult> GetSprints()
        {
            return Ok(await _sprintRepository.GetAll());
        }

        [HttpGet("sprints/{sprintId}/users")]
        public async Task<IActionResult> GetUsers(int sprintId)
        {
            return Ok(await _userRepository.GetUsersBySprintId(sprintId));
        }

        [HttpPost("{sprintId}/adjustment")]
        public async Task<IActionResult> PostAdjustment(int sprintId, [FromBody] Adjustment adjustment)
        {
            try
            {
                adjustment.SprintId = sprintId;
                adjustment.Id = default;
                await _adjustmentRepository.Add(adjustment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{sprintId}/{userId}")]
        public async Task<IActionResult> PutAvailability(int sprintId, int userId, [FromBody] TeamMemberAvailability teamMemberAvailability)
        {
            try
            {
                TeamMemberAvailability existingAvailability = await _teamMemberAvailabilityRepository.GetBySprintIdAndUserId(sprintId, userId);
                existingAvailability.AvailabilityPoints = teamMemberAvailability.AvailabilityPoints;
                await _teamMemberAvailabilityRepository.Update(existingAvailability);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}