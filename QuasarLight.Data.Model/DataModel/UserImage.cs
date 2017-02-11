using System;

namespace QuasarLight.Data.Model.DataModel
{
    public class UserImage
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ImageId { get; set; }
        public bool IsProfile { get; set; }
        public DateTime UploadDate { get; set; }
        public virtual User User { get; set; }

    }
}
