using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class AddShoppingListCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			if (ActiveSession.CurrentUser != null)
			{
				return true;
			}
			return false;
		}

		public void Execute(object parameter)
		{
			DisplayShoppingListViewModel displayShoppingListViewModel = (DisplayShoppingListViewModel)parameter;
			ShoppingListRepository shoppingListRepository = new ShoppingListRepository();

			if (displayShoppingListViewModel.ShoppingList.ShoppingListID == 0)
			{
				shoppingListRepository.InsertShoppingListToDatabase(displayShoppingListViewModel.ShoppingList);
				MessageBox.Show("Din indkøbsliste er nu oprettede", "Confirmation", MessageBoxButton.OK);
			}
			else
			{
				if (displayShoppingListViewModel.ShoppingList.User.Username == ActiveSession.CurrentUser.Username)
				{
					shoppingListRepository.UpdateShoppingListInDatabase(displayShoppingListViewModel.ShoppingList);
					MessageBox.Show("Din indkøbsliste er nu gemt", "Confirmation", MessageBoxButton.OK);
				}
				else
				{
					MessageBox.Show("Der er sket en fejl, du har ikke ret til at gemme denne liste", "Confirmation", MessageBoxButton.OK);
				}
			}
		}
	}
}
