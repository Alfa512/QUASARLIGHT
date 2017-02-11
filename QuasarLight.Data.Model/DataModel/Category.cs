namespace QuasarLight.Data.Model.DataModel
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
        public int Stock { get; set; }
        public int Count { get; set; }
        public string Seo { get; set; }
        public int Hierarchy { get; set; }
    }
}