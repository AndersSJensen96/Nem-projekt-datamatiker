namespace Nem.Models
{
	public class MealPlanRecipe
	{
		#region Properties
		public MealPlan MealPlan { get; set; }
		public Recipe SelectedRecipe { get; set; }
		public Weekday Day { get; set; }
		public TimePeriod Period { get; set; }
		public int TotalPeople { get; set; }

		#endregion Properties
	}
}