using CandidateInfo.Controllers;
using CandidateInfo.Models;
using CandidateInfo.Services.Csv;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CandidateInfo.Test.Controllers
{
    public class CandidateControllerTest
    {
        private readonly CandidateInfoController controller;

        public CandidateControllerTest()
        {
            var service = new CandidateService();
            controller = new CandidateInfoController(service);
        }

        [Fact]
        public async Task AddCandidate_ShouldSuccess_WhenAddingNewCandidateInfo()
        {
            var candidate = new Candidate()
            {
                Email = "user@gmail.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Comment = "Comment"
            };

            var result = await controller.AddCandidate(candidate);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            Assert.True(returnValue);
        }
    }
}
