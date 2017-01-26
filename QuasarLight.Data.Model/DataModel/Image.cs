using System;
using System.Data.Entity.Spatial;

namespace QuasarLight.Data.Model.DataModel
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DbGeography Location { get; set; }
        public byte Privacy { get; set; }
        public bool IsDeleted { get; set; }
    }
}