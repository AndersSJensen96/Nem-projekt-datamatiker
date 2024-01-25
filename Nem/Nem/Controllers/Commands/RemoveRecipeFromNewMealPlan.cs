using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class RemoveRecipeFromNewMealPlan : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{/*
			AddMealPlanViewModel displayMealPlanViewModel = (AddMealPlanViewModel)parameter;
			//Checks if the object exists in the collection and removes if it doesn't have it
			if (displayMealPlanViewModel.MealPlanRecipes.Contains(displayMealPlanViewModel.SelectedMealPlanRecipe) && displayMealPlanViewModel.SelectedMealPlanRecipe != null)
			{
				displayMealPlanViewModel.MealPlanRecipes.Remove(displayMealPlanViewModel.SelectedMealPlanRecipe);
			}
			*/
		}
	}
}
