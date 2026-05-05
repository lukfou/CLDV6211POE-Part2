using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CLDV6211POE.Models
{
    public class Venue
    {
        public int VenueId { get; set; }

        [Required]
        public required string Name { get; set; }

        public required string Location { get; set; }

        public int Capacity { get; set; }

        // ✅ ADD THIS
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
