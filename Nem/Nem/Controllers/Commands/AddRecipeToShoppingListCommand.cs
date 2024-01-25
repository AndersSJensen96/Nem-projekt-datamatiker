using Nem.Models;
using Nem.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class AddRecipeToShoppingListCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			//if(parameter is ViewModelAddProduct)
			//{
			//	return CheckBool((ViewModelAddProduct)parameter);
			//}
			//else
			//{
			//	return false;
			//}
			return true;
		}

		public void Execute(object parameter)
		{

			DisplayRecipeViewModel displayRecipeViewModel = (DisplayRecipeViewModel)parameter;
			ShoppingListRepository shoppingListRepository = new ShoppingListRepository();
			if (displayRecipeViewModel.TotalPeople > 0)
			{
				ActiveShoppingList.AddRecipeToShoppingList(displayRecipeViewModel.SelectedRecipe, displayRecipeViewModel.TotalPeople);
			}
            else
            {
                MessageBox.Show("Du kan ikke tilføje en opskrift til en madplan uden at angive antal personer den skal bruges til");
            }
	
			//shoppingListRepository.InsertToDatabase(ActiveShoppingList.ShoppingList);
		}

		#region supplerende metoder

		

		#endregion supplerende metoder
	}
}