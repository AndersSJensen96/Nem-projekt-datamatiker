namespace Nem.Models
{
	public class Product
	{
		#region Properties

		public string Name { get; set; }
		public string Description { get; set; }
		public double UnitValue { get; set; }
		public UnitDefinition Unit { get; set; }
		public bool Organic { get; set; }
		public bool Animal { get; set; }
		public bool Meat { get; set; }
		public bool Spice { get; set; }
		public Shelf ProductShelf { get; set; }
		public int ProductID { get; set; }
		public double Price { get; set; }
		#endregion Properties
	}
}