using CandidateAPI.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CandidateAPI.Controllers
{
    /// <summary>
    /// Author: Kelum
    /// License: MIT
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(ICandidateService candidateService, ILogger<CandidateController> logger)
        {
            _candidateService = candidateService;
            _logger = logger;
        }

        #region Actions

        /// <summary>
        /// Retrieves candidate information.
        /// </summary>
        /// <returns>Returns candidate information as JSON.</returns>
        [HttpGet]
        public IActionResult GetCandidateInfo()
        {
            try
            {
                // Call the service to retrieve candidate information
                var candidateInfo = _candidateService.GetCandidateInfo();

                // Log a successful request
                _logger.LogInformation("Successfully retrieved candidate information");

                // Return the candidate information as a JSON response
                return Ok(candidateInfo);
            }
            catch (Exception ex)
            {
                // Log the error if an exception occurs
                _logger.LogError(ex, "An error occurred while retrieving candidate information");

                // Handle the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        #endregion
    }
}

