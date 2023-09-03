using System;
namespace CarRentalACC.Data
{
    public class Car
    {

        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Image Image { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}

