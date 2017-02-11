using System;

namespace QuasarLight.Data.Model.DataModel
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TotalPrice { get; set; }
        public int DeliveryType { get; set; }
        public int PaymentType { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
