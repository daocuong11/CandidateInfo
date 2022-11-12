using CandidateInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateInfo.Services
{
    public interface ICandidateService
    {
        Task<bool> UpsertCandidateInfo(Candidate candidate);

        Task<IEnumerable<Candidate>> GetCandidates();

    }
}
