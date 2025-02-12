using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<DataController> _logger;

        public DataController(IDataService dataService, ILogger<DataController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        /// <summary>
        /// Seeds the database
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/data/seed
        ///     
        /// </remarks>
        /// <returns>Returns success message</returns>
        /// <response code="200">Database seeded</response>
        /// <response code="500">Internal Server error</response>
        #region Annotation
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        #endregion
        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            try
            {
                await _dataService.SeedDataAsync();
                return Ok("Database seeded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                return StatusCode(500, $"Internal server error.");
            }
        }

        /// <summary>
        /// Resets database
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/data/reset
        ///     
        /// </remarks>
        /// <returns>Returns success message</returns>
        /// <response code="200">Database seeded</response>
        /// <response code="500">Internal Server error</response>
        #region Annotation
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        #endregion
        [HttpPost("reset")]
        public async Task<IActionResult> ResetData()
        {
            try
            {
                await _dataService.ResetDataAsync();
                return Ok("Database reset successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                return StatusCode(500, $"Internal server error.");
            }
        }
    }
}
