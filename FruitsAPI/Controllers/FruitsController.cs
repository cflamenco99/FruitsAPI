using BusinessLogic.Fruits;
using BusinessLogic.Utils;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FruitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        private readonly IBLFruit _bLFruit;

        // Constructor injection
        public FruitsController(IBLFruit bLFruit)
        {
            _bLFruit = bLFruit ?? throw new ArgumentNullException(nameof(bLFruit));
        }

        // POST: /fruits
        [HttpPost]
        public async Task<ActionResult<FruitDTO>> CreateFruit(FruitDTO fruitDto)
        {
            var response = await _bLFruit.SaveFruit(fruitDto);

            if (!response.Ok)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response.Message);
            }

            return CreatedAtAction(nameof(FindFruitById), new { id = fruitDto.Id }, fruitDto);
        }

        // GET: /fruits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitDTO>>> FindAllFruits()
        {
            var response = await _bLFruit.FindAllFruits();

            if (!response.Ok)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response.Message);
            }

            return Ok(response.Data);
        }

        // GET: /fruits/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FruitDTO>> FindFruitById(int id)
        {
            var response = await _bLFruit.FindFruitById(id);

            if (response.NotFoundData)
            {
                return NotFound(NotFoundResponse.Get(response.Message));
            }

            if (!response.Ok)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response.Message);
            }

            return Ok(response.Data);
        }

        // PUT: /fruits/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFruit(int id, FruitDTO fruitDto)
        {
            if (id != fruitDto.Id)
            {
                return BadRequest(Messages.MSF009);
            }

            var response = await _bLFruit.UpdateFruit(fruitDto);

            if (response.NotFoundData)
            {
                return NotFound(NotFoundResponse.Get(response.Message));
            }

            if (!response.Ok)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response.Message);
            }

            return NoContent();
        }

        // DELETE: /fruits/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFruit(int id)
        {
            var response = await _bLFruit.DeleteFruit(id);

            if (response.NotFoundData)
            {
                return NotFound(NotFoundResponse.Get(response.Message));
            }

            if (!response.Ok)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response.Message);
            }

            return NoContent();
        }
    }
}