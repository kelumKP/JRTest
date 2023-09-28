using CandidateAPI.Domain;

namespace CandidateAPI.Application
{
    /// <summary>
    /// Author: Kelum
    /// License: MIT
    /// </summary>
    public interface ICandidateService
    {
        /// <summary>
        /// Retrieves candidate information.
        /// </summary>
        /// <returns>Returns candidate information.</returns>
        Candidate GetCandidateInfo();
    }
}

