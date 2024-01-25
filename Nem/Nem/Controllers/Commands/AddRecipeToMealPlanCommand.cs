using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class AddRecipeToMealPlanCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			/*
			AddMealPlanViewModel addMealPlanViewModel = (AddMealPlanViewModel)parameter;
			//Checks if the object exists in the collection and removes if it doesn't have it
			if (addMealPlanViewModel.SelectedRecipe != null && addMealPlanViewModel.AddMealPlanRecipe())
			{
				
			} else
			{
				MessageBox.Show("Opskriften blev ikke tilføjet, da den allerede var tilføjet til den pågældende ugedag, og tidsrum");
			}
			*/
		}
	}
}
