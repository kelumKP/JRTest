using CandidateAPI.Application;
using Microsoft.AspNetCore.Mvc;

namespace CandidateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public IActionResult GetCandidateInfo()
        {
            var candidateInfo = _candidateService.GetCandidateInfo();
            return Ok(candidateInfo);
        }
    }
}
