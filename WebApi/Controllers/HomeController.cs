using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly JyrosContext _context;

        public TestController(JyrosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var teams = await _context.Teams.Include(t => t.TeamLead).ToListAsync();
                return Ok(teams);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddRandomTeam")]
        public async Task<ActionResult> AddRandomTeam()
        {
            try
            {
                var random = new Random();
                var team = new Team
                {
                    TeamName = "Team " + random.Next(1, 1000),
                    TeamDescription = "Description for team " + random.Next(1, 1000),
                    TeamLeadId = 1
                };

                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                return Ok(team);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //add a method that gets a team id and returns the teamlead of that team
        [HttpGet("GetTeamLead/{teamId}")]
        public async Task<ActionResult> GetTeamLead(int teamId)
        {
            try
            {
                var team = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);
                return Ok(team?.TeamLead);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //add a methode that adds a team
        [HttpPost("AddTeam")]
        public async Task<ActionResult> AddTeam(Team team)
        {
            try
            {
                _context.Teams.Add(team);
                await _context.SaveChangesAsync();
                return Ok(team);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //get all users
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //get users in a team
        [HttpGet("GetUsersInTeam/{teamId}")]
        public async Task<ActionResult> GetUsersInTeam(int teamId)
        {
            try
            {
                var team = await _context.Teams.Include(t => t.Users).FirstOrDefaultAsync(t => t.TeamId == teamId);
                return Ok(team?.Users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}