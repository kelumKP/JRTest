/*
 * File Author: Kelum
 * License: MIT
 */

using System;

namespace JRAppAPI.Models
{
    /// <summary>
    /// Represents a listing for a vehicle.
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// Gets or sets the name of the listing.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price per passenger for the listing.
        /// </summary>
        public decimal PricePerPassenger { get; set; }

        /// <summary>
        /// Gets or sets the vehicle type associated with the listing.
        /// </summary>
        public VehicleType VehicleType { get; set; }

        /// <summary>
        /// Gets or sets the total price for the listing.
        /// </summary>
        public decimal Price { get; set; }
    }
}

