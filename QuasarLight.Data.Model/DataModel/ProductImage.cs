using System;

namespace QuasarLight.Data.Model.DataModel
{
    public class ProductImage
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ImageId { get; set; }
        public bool IsProfile { get; set; }
        public DateTime UploadDate { get; set; }

    }
}
