using System;
namespace CarRentalACC.Data
{
	public interface IImagesService
	{

		public Task<Image> AddImage(string FileName);
        public Task RemoveImage(Guid id);
		public Task<string> GetImageById(Guid id);

    }
}

