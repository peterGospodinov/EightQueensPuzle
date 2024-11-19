using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueensPuzle.Web.Data;
using QueensPuzzle.Application.Models;

namespace NQueensPuzzle.Web.Controllers
{
    [Route("api/Results")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ResultContext _context;

        public ResultController(ResultContext context)
        {
         
            _context = context;
        }

        /// <summary>
        /// Retrieves all solution results from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="SolutionResult"/> objects.
        /// Returns 204 No Content if no results are found.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolutionResult>>> GetResults()
        {
            try 
            {
                var results = await _context.SolutionResults.ToListAsync();
                if (results == null || !results.Any())
                {
                    return NoContent();
                }

                return Ok(results);
            }
            catch (Exception ex)
            {               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific solution result by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the solution result.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the <see cref="SolutionResult"/> if found.
        /// Returns 404 Not Found if the solution result does not exist.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SolutionResult>> GetResult(int id)
        {
            try 
            {
                var result = await _context.SolutionResults.FindAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception ex)
            {              
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}