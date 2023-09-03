using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarRentalACC.Data
{
	public class ImagesService : IImagesService
	{

		private ApplicationDbContext _dataContext;

		public ImagesService(ApplicationDbContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<Image> AddImage(string fileName)
		{
            string extension = System.IO.Path.GetExtension(fileName);
            var image = new Image {FileName = fileName, Extension = extension};
			_dataContext.Images.Add(image);
			await _dataContext.SaveChangesAsync();

			return image;
		}

        public async Task RemoveImage(Guid id)
		{
			Image currentImage = _dataContext.Images.FirstOrDefault(o => o.Id == id);
            _dataContext.Images.Remove(currentImage);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<string> GetImageById(Guid id)
        {
			var currentImage = _dataContext.Images.FirstOrDefault(o => o.Id == id);
			var fileName = currentImage.Id + currentImage.Extension;

            return fileName;
        }
    }
}

