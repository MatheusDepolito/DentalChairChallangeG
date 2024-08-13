using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using DentalChairChallangeG.Services;
using Microsoft.AspNetCore.Mvc;

namespace DentalChairChallangeG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChairController(ChairService chairService) : ControllerBase
    {
        private readonly ChairService _chairService = chairService;

        // POST api/chair
        [HttpPost("create")]
        public async Task<IActionResult> CreateChair([FromBody] CreateChairDTO createChairDTO)
        {
            if (createChairDTO == null)
            {
                return BadRequest("Invalid chair data.");
            }

            try
            {
                var chair = await _chairService.CreateChair(createChairDTO);
                return Ok(chair);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/chair
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Chair>>> GetAllChairs()
        {
            var chairs = await _chairService.GetAllChairs();
            return Ok(chairs);
        }

        [HttpGet("getChairById/{id}")]
        public async Task<IActionResult> GetChairById(int id)
        {
            try
            {
                var chair = await _chairService.GetChairById(id);
                return Ok(chair);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/chair/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChair(int id, [FromBody] UpdateChairDTO updatedChairDTO)
        {
            if (updatedChairDTO == null)
            {
                return BadRequest("Invalid chair data.");
            }

            try
            {
                var existingChair = await _chairService.GetChairById(id);
                if (existingChair == null)
                {
                    return NotFound("Chair not found.");
                }

                existingChair.Number = updatedChairDTO.Number ?? existingChair.Number;
                existingChair.Description = updatedChairDTO.Description ?? existingChair.Description;
                existingChair.IsAvailable = updatedChairDTO.IsAvailable ?? existingChair.IsAvailable;

                var updatedChair = await _chairService.UpdateChair(existingChair);

                return Ok(updatedChair);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/chair/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChair(int id)
        {
            try
            {
                await _chairService.DeleteChair(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("allocate")]
        public async Task<IActionResult> AllocateChairs([FromBody] ChairAllocationInputDTO allocationInput)
        {
            if (allocationInput == null)
            {
                return BadRequest("Invalid allocation data.");
            }

            try
            {
                var allocationResult = await _chairService.AllocateChairs(allocationInput);
                return Ok(allocationResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error for unexpected issues
            }
        }
    }
}
