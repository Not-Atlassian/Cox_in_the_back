using Microsoft.AspNetCore.Mvc;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacklogController : ControllerBase
    {
        private readonly User user;
        private readonly StoryRepository _storyRepository;

        public BacklogController(JyrosContext jyrosContext)
        {
            _storyRepository = new StoryRepository(jyrosContext);
            user = new User
            {
                UserId = 1,
                Username = "alice_smith"
            };
        }

        [HttpGet("{page}/{count}")]
        public async Task<IActionResult> GetAsync(int page, int count)
        {
            if (count <= 200)
                return Ok(await _storyRepository.GetPaginated(page, count));

            return BadRequest();
        }

        [HttpGet("details/{id}")]
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
                await _storyRepository.Add(story);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] Story story)
        {
            try
            {
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
                await _storyRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{searchKey}/{page}/{pageSize}")]
        public async Task<IActionResult> GetFilteredPaginated(string searchKey, int page, int pageSize)
        {
            if (pageSize <= 200)
                return Ok(await _storyRepository.GetFilteredPaginated(searchKey, page, pageSize));

            return BadRequest();
        }
    }
}
