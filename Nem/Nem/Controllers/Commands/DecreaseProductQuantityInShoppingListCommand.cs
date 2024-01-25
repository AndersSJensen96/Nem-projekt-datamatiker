using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    class DecreaseProductQuantityInShoppingListCommand : ICommand
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
            if(displayShoppingListViewModel.SelectedShoppingListProductQuantity != null)
            {
                if (displayShoppingListViewModel.SelectedShoppingListProductQuantity.Quantity > 1)
                {
                    ActiveShoppingList.AddProductToShoppingList(displayShoppingListViewModel.SelectedShoppingListProductQuantity.SelectedProduct, -1);
                }
                else
                {
                    MessageBox.Show("Du kan ikke købe mindre end 1 vare", "Confirmation", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Vælg venligst et produkt", "Confirmation", MessageBoxButton.OK);
            }
                
            displayShoppingListViewModel.CalculateTotalPrice();
        }
    }
}
