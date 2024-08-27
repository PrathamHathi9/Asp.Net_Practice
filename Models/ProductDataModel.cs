namespace dotNet.Models
{
        public class ProductDataModel
        {
            public Guid id { get; set;}
            public string PName { get; set; }
            public double Rate { get; set; }

            // Navigation Property
            public ICollection<CustomerDataModel> Customer { get; set; }
        }
}
