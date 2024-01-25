using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class AddMealPlanToShoppingListCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			DisplayMealPlanViewModel displayMealPlanViewModel = (DisplayMealPlanViewModel)parameter;
			bool allMealPlansAdded = true;
			if (displayMealPlanViewModel.SelectedMealPlan != null)
			{
				foreach (MealPlanRecipe mealPlanRecipe in displayMealPlanViewModel.SelectedMealPlan.Recipes)
				{
					//ShoppingListRepository shoppingListRepository = new ShoppingListRepository();
					if (mealPlanRecipe.TotalPeople > 0)
					{
						ActiveShoppingList.AddRecipeToShoppingList(mealPlanRecipe.SelectedRecipe, mealPlanRecipe.TotalPeople);
					}
					else
					{
						allMealPlansAdded = false;
						MessageBox.Show("Vi kunne ikke tilføje "+ mealPlanRecipe.SelectedRecipe.Name + " til indkøbslisten grundet der ikke er angivet person antal til denne");
					}
				}
				if (allMealPlansAdded)
				{
					MessageBox.Show("Madplanens opskrifter er nu tilføjet til din indkøbsliste");
				}
				else
				{
					MessageBox.Show("Madplanens opskrifter blev ikke tilføjet da de manglede antal af personer");
				}
				
			}
		}
	}
}
