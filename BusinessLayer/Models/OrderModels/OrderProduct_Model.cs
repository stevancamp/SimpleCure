namespace BusinessLayer.Models.OrderModels
{
    public class OrderProduct_Model
    {
        public int ID { get; set; }

        public int OrderInfoID { get; set; }

        public int Type { get; set; }

        public int Quantity { get; set; }
    }
}