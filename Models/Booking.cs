using System;
using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [Display(Name = "Event")]
        public int EventId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        // Optional navigation properties if using EF
        public Venue? Venue { get; set; }
        public Event? Event { get; set; }
    }
}
