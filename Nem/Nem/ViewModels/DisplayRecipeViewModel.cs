using Nem.Controllers;
using Nem.Controllers.Commands;
using Nem.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Nem.ViewModels
{
	public class DisplayRecipeViewModel
	{
		private MealPlanRepository mealPlanRepository = new MealPlanRepository();
		private TimePeriodRepository timePeriodRepository = new TimePeriodRepository();
		#region Properties
		public string WeekDay { get; set; }
		public Recipe SelectedRecipe { get; set; }
		public MealPlan SelectedMealPlan { get; set; }
		public TimePeriod SelectedTimePeriod { get; set; }
		public int TotalPeople { get; set; }

		public ObservableCollection<MealPlan> MealPlans { get; set; }
		public ObservableCollection<TimePeriod> TimePeriods { get; set; }
		#endregion Properties

		#region Constructor
		public DisplayRecipeViewModel(Recipe recipe)
		{
			RecipeRepository recipeRepository = new RecipeRepository();
			if (recipe.ProductQuantities.Count <= 0)
			{
				recipe = recipeRepository.GetById(recipe);
			}
			SelectedRecipe = recipe;
			if (ActiveSession.CurrentUser != null)
			{
				MealPlans = new ObservableCollection<MealPlan>(mealPlanRepository.GetAll());
			}
			

			TimePeriods = new ObservableCollection<TimePeriod>(timePeriodRepository.GetAll());
		}

		#endregion

		#region ICommands
		public ICommand AddRecipeToMealPlan { get; } = new AddProductToMealPlanCommand();
		public ICommand AddRecipeToShoppingList { get; } = new AddRecipeToShoppingListCommand();

		#endregion ICommands
	}
}
