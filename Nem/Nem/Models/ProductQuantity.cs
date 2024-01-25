using System;
using System.Collections.Generic;
using System.Text;

namespace Nem.Models
{
	public class ProductQuantity
	{
		public int ProductQuantityID { get; set; }
		public double UserUnitValue { get; set; } // user defined value " one spoon"
		public string UserUnit { get; set; } // defined type "spoon"
		public double ProductUnitValue { get; set; } // Product real unit value fx. grams in a bag of rice.
		public Product SelectedProduct { get; set; } = new Product();
		public Recipe ProductQuantityRecipeyProperty { get; set; } = new Recipe();
	}
}
