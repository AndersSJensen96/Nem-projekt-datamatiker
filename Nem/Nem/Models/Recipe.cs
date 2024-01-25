using System;
using System.Collections.Generic;
using System.Text;

namespace Nem.Models
{
	public class Recipe
	{
		#region Properties
		public int RecipeID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Difficulty { get; set; }
		public int CookingTime { get; set; }
		public bool Animal { get; set; }
		public bool Meat { get; set; }
		public bool Organic { get; set; }
		public int NumberOfPersons { get; set; }
		public DateTime CreationTime { get; set; }
		public User CurrentUser { get; set; }
		public List<FoodCategory> FoodCategories { get; set; } = new List<FoodCategory>();
		public List<Mealtime> Mealtimes { get; set; } = new List<Mealtime>();
		public List<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();
		#endregion
	}
}
