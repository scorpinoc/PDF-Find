using System;

namespace Model.Entities
{
    public class FileReadingData : IFileOpeningData
    {
        public Guid Id { get; set; }
        public DateTime Opening { get; set; }
        public string FileName { get; set; }
    }
}