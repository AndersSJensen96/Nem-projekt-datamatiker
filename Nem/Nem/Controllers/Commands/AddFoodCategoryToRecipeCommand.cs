using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Nem.Models;
using Nem.ViewModels;
namespace Nem.Controllers.Commands
{
	public class AddFoodCategoryToRecipeCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			AddRecipeViewModel addRecipeViewModel = (AddRecipeViewModel)parameter;
			
			//Checks if the object exists in the collection and adds if it doesn't have it
			if (!addRecipeViewModel.ChosenFoodCategories.Contains(addRecipeViewModel.SelectedFoodCategory) && addRecipeViewModel.SelectedFoodCategory != null)
			{
				addRecipeViewModel.ChosenFoodCategories.Add(addRecipeViewModel.SelectedFoodCategory);
			}
			
		}
	}
}
