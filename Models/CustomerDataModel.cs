namespace dotNet.Models
{
	public class CustomerDataModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string phoneNo { get; set; }

		// Foreign Key
		public Guid ProductId { get; set; }

		// Navigation Property
		public ProductDataModel Product { get; set; }
	}
}
