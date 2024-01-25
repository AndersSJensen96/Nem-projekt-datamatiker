using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class FindProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SearchBy(parameter);
        }

        public void SearchBy(object parameter)
        {
            DisplayShoppingListViewModel displayShoppingListViewModel = (DisplayShoppingListViewModel)parameter;
            ProductRepository productRepository = new ProductRepository();
            
            if (displayShoppingListViewModel.SearchBoxInput.Length >= 1)
            {
                displayShoppingListViewModel.FoundProducts = new ObservableCollection<Product>(productRepository.GetBySearch(displayShoppingListViewModel.SearchBoxInput));
            } 
            else 
            {
                displayShoppingListViewModel.FoundProducts = new ObservableCollection<Product>();
            }
        }
	}
}
