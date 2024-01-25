using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    class RemoveProductQuantityInShoppingListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DisplayShoppingListViewModel displayShoppingListViewModel = (DisplayShoppingListViewModel)parameter;

            displayShoppingListViewModel.ShoppingList.ShoppingListProductQuantities
                .Remove(displayShoppingListViewModel.SelectedShoppingListProductQuantity);
        }
    }
}
