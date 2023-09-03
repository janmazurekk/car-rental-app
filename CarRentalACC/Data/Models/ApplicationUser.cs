using System;
using Microsoft.AspNetCore.Identity;
namespace CarRentalACC.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Reservation> UserReservations { get; set; } = new List<Reservation>();

    }
}
