using CandidateInfo.Models;
using CandidateInfo.Services.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CandidateInfo.Test
{
    public class CandidateServiceTest
    {
        private readonly CandidateService service;

        public CandidateServiceTest()
        {
            service = new CandidateService();
        }

        [Fact]
        public async Task UpsertCandidateInfo_ShouldSuccess_WhenAddingNewCandidateInfo()
        {
            var candidate = new Candidate()
            {
                Email = "user@gmail.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Comment = "Comment"
            };

            var result = await service.UpsertCandidateInfo(candidate);

            Assert.True(result);
        }


        [Fact]
        public async Task UpsertCandidateInfo_ShouldSuccess_WhenEditingCandidateInfo()
        {
            var candidate = new Candidate()
            {
                Email = "userEdit@gmail.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Comment = "Comment"
            };
            await service.UpsertCandidateInfo(candidate);
            candidate.FirstName = "New FirstName";

            var result = await service.UpsertCandidateInfo(candidate);

            Assert.True(result);

            var candidates = (List<Candidate>) await service.GetCandidates();
            var savedCandidate = candidates.FirstOrDefault(e => e.Email == candidate.Email);

            Assert.Equal(candidate.FirstName, savedCandidate.FirstName);
            
        }
    }
}
