using Nem.Controllers;
using Nem.Controllers.Commands;
using Nem.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace Nem.ViewModels
{
	public class DisplayMealPlanViewModel : INotifyPropertyChanged
	{

		#region Fields
		private MealPlanRepository mealPlanRepository = new MealPlanRepository();
		private TimePeriodRepository timePeriodRepository = new TimePeriodRepository();
		private MealPlan selectedMealPlan;
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Fields

		#region Properties

		public MealPlan SelectedMealPlan { 
			get
			{
				return selectedMealPlan;
			}
			set
			{
				selectedMealPlan = value;
				OnPropertyChanged("SelectedMealPlan");
			}
		}
		public MealPlanRecipe SelectedMealPlanRecipe { get; set; }

		#endregion Properties

		#region Commands
		public ICommand RemoveRecipeFromMealPlan { get; } = new RemoveRecipeFromMealPlanCommand();
		public ICommand AddMealPlanToShoppingList { get; } = new AddMealPlanToShoppingListCommand();
		#endregion

		#region Constructors


		public DisplayMealPlanViewModel(MealPlan mealPlan)
		{
			SelectedMealPlan = mealPlanRepository.GetById(mealPlan);
		}

		#endregion Constructors

		#region Methods
		public void RemoveFromMealPlanRecipe(MealPlanRecipe mealPlanRecipe)
		{
			if (SelectedMealPlan.Recipes.Contains(mealPlanRecipe) && mealPlanRecipe != null)
			{
				mealPlanRepository.RemoveRecipeFromMealPlan(mealPlanRecipe);
				SelectedMealPlan.Recipes.Remove(mealPlanRecipe);
			}
		}

		private void OnPropertyChanged(string info)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion
	}
}