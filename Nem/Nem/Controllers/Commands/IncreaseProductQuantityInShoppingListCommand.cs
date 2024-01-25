using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    class IncreaseProductQuantityInShoppingListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
			return true;
		}

        public void Execute(object parameter)
        {
			DisplayShoppingListViewModel displayShoppingListViewModel = (DisplayShoppingListViewModel)parameter;
			ShoppingListRepository shoppingListRepository = new ShoppingListRepository();
            if (displayShoppingListViewModel.SelectedShoppingListProductQuantity != null)
            {
               ActiveShoppingList.AddProductToShoppingList(displayShoppingListViewModel.SelectedShoppingListProductQuantity.SelectedProduct, 1);
            }
            else
            {
                MessageBox.Show("Vælg venligst et produkt", "Confirmation", MessageBoxButton.OK);
            }
           
            displayShoppingListViewModel.CalculateTotalPrice();
        }
    }
}
