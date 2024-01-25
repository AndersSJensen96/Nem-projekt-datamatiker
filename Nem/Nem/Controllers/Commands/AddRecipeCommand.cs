using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Nem.Models;
using Nem.ViewModels;
using System.Linq;

namespace Nem.Controllers.Commands
{
	public class AddRecipeCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			AddRecipeViewModel addRecipeViewModel = (AddRecipeViewModel)parameter;
			RecipeRepository recipeRepository = new RecipeRepository();
			
			if (InformationCheck(addRecipeViewModel))
			{
				System.Windows.MessageBox.Show("Tilføget");
				Recipe newRecipe = new Recipe();
				newRecipe.Name = addRecipeViewModel.Name;
				newRecipe.Description = addRecipeViewModel.Description;
				newRecipe.Difficulty = addRecipeViewModel.Difficulty;
				newRecipe.CookingTime = addRecipeViewModel.CookingTime;
				newRecipe.CurrentUser = addRecipeViewModel.CurrentUser;
				newRecipe.NumberOfPersons = addRecipeViewModel.NumberOfPersons;
				//ToList is a Linq, and in this case it converts an observablecollection to a list.
				newRecipe.FoodCategories = addRecipeViewModel.ChosenFoodCategories.ToList();
				newRecipe.Mealtimes = addRecipeViewModel.ChosenMealtimes.ToList();
				newRecipe.ProductQuantities = addRecipeViewModel.ChosenProductQuantitys.ToList();

				recipeRepository.InsertRecipeToDatabase(newRecipe);
			}

		}

		#region Supplimentary methods
		private bool InformationCheck(AddRecipeViewModel addRecipeViewModel)
		{
			bool check = false;
			if (addRecipeViewModel.Name != "" 
				&& addRecipeViewModel.Description != "" 
				&& addRecipeViewModel.Difficulty != 0 
				&& addRecipeViewModel.CookingTime != 0 
				&& addRecipeViewModel.CurrentUser != null
				&& addRecipeViewModel.ChosenMealtimes.Count != 0
				&& addRecipeViewModel.ChosenFoodCategories.Count != 0)
			{
				check = true;
			}
			return check;
		}
		#endregion
	}
}
