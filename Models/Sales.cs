using System.Linq;


namespace aspnetapp.Models
{
    public class Sales
    {
        public long? Id { get; set; }
        public string CustomerName { get; set; }
        public string Salesperson { get; set; }
        public float Price { get; set; }
        public bool hasPayment { get; set; }
    }
}