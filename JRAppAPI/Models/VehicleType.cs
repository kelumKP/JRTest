/*
 * File Author: Kelum
 * License: MIT
 */

namespace JRAppAPI.Models
{
    /// <summary>
    /// Represents a type of vehicle.
    /// </summary>
    public class VehicleType
    {
        /// <summary>
        /// Gets or sets the name of the vehicle type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of passengers the vehicle can accommodate.
        /// </summary>
        public int MaxPassengers { get; set; }

    }
}

