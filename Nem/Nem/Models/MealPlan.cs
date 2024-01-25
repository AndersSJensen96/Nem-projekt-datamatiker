using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nem.Models
{
	public class MealPlan
	{
		#region Properties
		public int MealPlanID { get; set; }
		public string Title { get; set; }
		public int Week { get; set; }
		public int Year { get; set; }
		public User Owner { get; set; }
		public ObservableCollection<MealPlanRecipe> Recipes { get; set; }
		#endregion
	}
}