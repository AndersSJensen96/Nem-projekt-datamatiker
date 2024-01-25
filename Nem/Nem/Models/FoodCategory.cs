using System;
using System.Collections.Generic;
using System.Text;

namespace Nem.Models
{
	public class FoodCategory
	{
		#region Properties
		public int FoodCategoryID { get; set; }
		public string Name { get; set; }
		public bool Organic { get; set; }
		public bool Meat { get; set; }
		public bool Animal { get; set; }
		public List<Recipe> Recipes { get; set; }
		#endregion
	}
}
