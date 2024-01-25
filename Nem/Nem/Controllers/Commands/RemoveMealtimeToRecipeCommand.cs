using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class RemoveMealtimeToRecipeCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			AddRecipeViewModel addRecipeViewModel = (AddRecipeViewModel)parameter;
			//Checks if the object exists in the collection and removes if it doesn't have it
			if (addRecipeViewModel.ChosenMealtimes.Contains(addRecipeViewModel.SelectedMealtime) && addRecipeViewModel.SelectedMealtime != null)
			{
				addRecipeViewModel.ChosenMealtimes.Remove(addRecipeViewModel.SelectedMealtime);
			}
		}
	}
}
