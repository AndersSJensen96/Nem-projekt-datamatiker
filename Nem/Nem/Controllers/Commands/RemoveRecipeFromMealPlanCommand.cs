using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class RemoveRecipeFromMealPlanCommand : ICommand
	{

		public event EventHandler CanExecuteChanged;

		
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			DisplayMealPlanViewModel displayMealPlanViewModel = (DisplayMealPlanViewModel)parameter;
			displayMealPlanViewModel.RemoveFromMealPlanRecipe(displayMealPlanViewModel.SelectedMealPlanRecipe);
		}
	}
}
