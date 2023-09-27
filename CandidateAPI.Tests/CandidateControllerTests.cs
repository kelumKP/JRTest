using System;
using CandidateAPI.Application;
using CandidateAPI.Controllers;
using CandidateAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CandidateAPI.Tests
{
    public class CandidateControllerTests
    {
        [Fact]
        public void GetCandidateInfo_ReturnsCandidateInfo()
        {
            // Arrange
            var candidateServiceMock = new Mock<ICandidateService>();
            var expectedCandidate = new Candidate { Name = "test", Phone = "test" };
            candidateServiceMock.Setup(service => service.GetCandidateInfo()).Returns(expectedCandidate);

            var controller = new CandidateController(candidateServiceMock.Object);

            // Act
            var result = controller.GetCandidateInfo();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualCandidate = Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal(expectedCandidate.Name, actualCandidate.Name);
            Assert.Equal(expectedCandidate.Phone, actualCandidate.Phone);
        }
    }
}