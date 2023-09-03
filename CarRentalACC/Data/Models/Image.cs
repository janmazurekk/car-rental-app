using System;
namespace CarRentalACC.Data
{
    public class Image
    {

        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        private string _filePath;
        public string FilePath
        {
            get => _filePath ?? $"{Id}{Extension}";
        }        

    }
}

