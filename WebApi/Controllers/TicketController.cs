using Microsoft.AspNetCore.Mvc;
using WebApi.Context;
using WebApi.Models;
using WebApi.RepositoryInterfaces;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly User user = Globals.curretUser;
        private readonly IStoryRepository _storyRepository;
        //private readonly StoryPointEstimator _storyPointEstimator;
        public TicketController(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
            user = new User
            {
                UserId = 1,
                Username = "alice_smith"
            };
            //_storyPointEstimator = new StoryPointEstimator();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _storyRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var story = await _storyRepository.GetById(id);

            return story != null ? Ok(story) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Story story)
        {
            try
            {
                story.CreatedBy = user.UserId;
                story.StoryId = default;
                story.ParentId = default;
                await _storyRepository.Add(story);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public struct StatusUpdate
        {
            public string Status { get; set; }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutAsync([FromBody] StatusUpdate status, int id)
        {
            try
            {
                var story = await _storyRepository.GetById(id);
                story.Status = status.Status;
                await _storyRepository.Update(story);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] Story story, int id)
        {
            try
            {
                story.StoryId = id;
                await _storyRepository.Update(story);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var story = await _storyRepository.GetById(id);
                if (story == null)
                {
                    return NotFound();
                }
                await _storyRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("EstimateStoryPoints")]
        //public async Task<IActionResult> EstimateStoryPointsAsync(string title, string? description)
        //{
        //    var stories = await _storyRepository.GetAll();
        //    var storyPoints = await _storyPointEstimator.EstimateStoryPoints(title, description ?? string.Empty);
        //    return Ok(storyPoints);
        //}
    }
}