using System.Collections.Generic;

namespace Nem.Models
{
	public class Shelf
	{
		#region Properties
		public int ShelfID { get; set; }
		public string Name { get; set; }
		public Section ShelfSection { get; set; }
		public List<Product> Products { get; set; }
		#endregion Properties
	}
}