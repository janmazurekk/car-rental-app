using System;
namespace CarRentalACC.Data
{
	public interface ICarsService
	{

		public Task<Car> AddCar (string model, string category, string description, float price, Image image);
        public Task EditCar(Guid id, string model, string category, string description, float price, Image? image = null);
        public Task RemoveCar(Guid id);
		public Task<Car?> GetCarById(Guid id);
        public Task<List<Car>> GetCars(string category = null, DateTime? startDate = null, DateTime? endDate = null);

    }
}

