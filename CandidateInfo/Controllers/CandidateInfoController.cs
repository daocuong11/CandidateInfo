using CandidateInfo.Models;
using CandidateInfo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateInfoController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateInfoController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("GetCandidates")]
        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            return await _candidateService.GetCandidates();
        }

        [HttpPost("AddCandidate")]
        public async Task<ActionResult<bool>> AddCandidate([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _candidateService.UpsertCandidateInfo(candidate));
        }
    }
}
