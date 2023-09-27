using Microsoft.AspNetCore.Mvc;
using CandidateAPI.Domain;

namespace CandidateAPI.Application
{ 
    public class CandidateService : ICandidateService
    {
        public Candidate GetCandidateInfo()
        {
            // Implement your business logic here
            return new Candidate
            {
                Name = "test",
                Phone = "test"
            };
        }
    }
}
