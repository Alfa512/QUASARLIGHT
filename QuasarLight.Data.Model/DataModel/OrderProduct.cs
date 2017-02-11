namespace QuasarLight.Data.Model.DataModel
{
    public class OrderProduct
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}