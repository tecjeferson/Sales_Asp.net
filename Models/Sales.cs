using System.Linq;


namespace aspnetapp.Models
{
    public class Sales : SalesPerson
    {
       
        public string CustomerName { get; set; }
        public float Price { get; set; }
        public bool hasPayment { get; set; }
    }
}