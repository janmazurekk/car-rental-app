using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CarRentalACC.Data
{
    public class CarsService : ICarsService
    {

        private ApplicationDbContext _dataContext;
        private IImagesService imagesService;
        private Image currentImage;

        public CarsService(ApplicationDbContext dataContext, IImagesService imagesService)
        {
            _dataContext = dataContext;
            this.imagesService = imagesService;
        }

        public async Task<Car> AddCar(string model, string category, string description, float price, Image image)
        {
            var car = new Car { Model = model, Category = category, Description = description, Price = price, Image = image };
            _dataContext.Cars.Add(car);

            await _dataContext.SaveChangesAsync();

            return car;
        }

        public async Task EditCar(Guid id, string model, string category, string description, float price, Image? image = null)
        {
            Car currentCar = _dataContext.Cars.FirstOrDefault(o => o.Id == id);
            currentCar.Model = model;
            currentCar.Category = category;
            currentCar.Description = description;
            currentCar.Price = price;
            if (image != null)
            {
                var oldImage = currentCar.Image.Id;
                currentCar.Image = image;
                await imagesService.RemoveImage(oldImage);
            }
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveCar(Guid id)
        {
            Car currentCar = _dataContext.Cars.FirstOrDefault(o => o.Id == id);
            _dataContext.Cars.Remove(currentCar);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<Car?> GetCarById(Guid id)
        {
            var currentCar = _dataContext.Cars.FirstOrDefault(o => o.Id == id);
            return currentCar;
        }

        public async Task<List<Car>> GetCars(string category = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            IQueryable<Car> carsQuery = _dataContext.Cars.Include(e => e.Image);

            if (!string.IsNullOrEmpty(category))
            {
                string lowercaseCategory = category.ToLower();
                carsQuery = carsQuery.Where(car => car.Category.ToLower() == lowercaseCategory);
            }

            if (startDate != null && endDate != null)
            {
                carsQuery = carsQuery.Where(car =>
                    !car.Reservations.Any(reservation =>
                        (reservation.StartDate <= endDate && reservation.EndDate >= startDate)
                    )
                );
            }

            var cars = await carsQuery.ToListAsync();
            return cars;
        }

    }
}
