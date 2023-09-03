using System;
using Microsoft.AspNetCore.Identity;

namespace CarRentalACC.Data
{
	public interface IReservationsService
	{

        public Task AddReservation(DateTime startDate, DateTime endDate, double startLatitude, double startLongitude, double endLatitude, double endLongitude, ApplicationUser author, float cost, Car car);
        public Task RemoveReservation(Guid id);
        public Task<List<Reservation>> GetReservations();
        public Task<List<DateTime>> GetReservationDatesForCar(Car car);
        public Task<List<Reservation>> GetReservationsForUser(ApplicationUser user);

    }

}

