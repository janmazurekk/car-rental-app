using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalACC.Data
{
    public class ReservationsService : IReservationsService
    {

        private ApplicationDbContext _dataContext;

        public ReservationsService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddReservation(DateTime startDate, DateTime endDate, double startLatitude, double startLongitude, double endLatitude, double endLongitude, ApplicationUser author, float cost, Car car)
        {
            var reservation = new Reservation { StartDate = startDate, EndDate = endDate, CreationDate = DateTime.Now, StartLatitude = startLatitude, StartLongitude = startLongitude, EndLatitude = endLatitude, EndLongitude = endLongitude, Author = author, Cost = cost, Car = car };
            _dataContext.Reservations.Add(reservation);
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveReservation(Guid id)
        {
            Reservation currentReservation = _dataContext.Reservations.FirstOrDefault(o => o.Id == id);
            _dataContext.Reservations.Remove(currentReservation);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetReservations()
        {
            return await _dataContext.Reservations.Include(e => e.Car).Include(e => e.Author).ToListAsync();
        }

        public async Task<List<DateTime>> GetReservationDatesForCar(Car car)
        {
            List<DateTime> reservationDates = new List<DateTime>();

            // Retrieve all reservations for the given car from the database
            List<Reservation> reservationsForCar = await _dataContext.Reservations
                .Where(r => r.Car == car)
                .ToListAsync();

            // Loop through the reservations and add all the dates (including time) to the list
            foreach (var reservation in reservationsForCar)
            {
                // Get all the dates between StartDate and EndDate (inclusive)
                for (var date = reservation.StartDate; date <= reservation.EndDate; date = date.AddDays(1))
                {
                    reservationDates.Add(date);
                }
            }

            return reservationDates;
        }

        public async Task<List<Reservation>> GetReservationsForUser(ApplicationUser user)
        {
            return await _dataContext.Reservations
                .Where(r => r.Author == user)
                .ToListAsync();
        }
    }
}

