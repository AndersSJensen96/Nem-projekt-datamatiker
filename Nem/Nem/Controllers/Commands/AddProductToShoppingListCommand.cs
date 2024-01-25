using Nem.Controllers.Interfaces;
using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    class AddProductToShoppingListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ICanAddProductInterface<Product> displayProductViewModel = (ICanAddProductInterface<Product>)parameter;
            ShoppingListRepository shoppingListRepository = new ShoppingListRepository();
            ActiveShoppingList.AddProductToShoppingList(displayProductViewModel.SelectedProduct, 1);

            //shoppingListRepository.InsertToDatabase(ActiveShoppingList.ShoppingList);
        }
    }
}
