using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    class SearchForProductCommand : ICommand
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
            DisplayProductListViewModel displayProductListViewModel = (DisplayProductListViewModel)parameter;
            if (displayProductListViewModel.SearchBoxInput.Length >= 1)
            {
                ObservableCollection<Product> temp = new ObservableCollection<Product>();
                foreach (Product product in displayProductListViewModel.Products)
                {
                    if (product.Name.ToUpper().Contains(displayProductListViewModel.SearchBoxInput.ToUpper()))
                    {
                        temp.Add(product);
                    }
                }
                displayProductListViewModel.FilteredProducts = temp;
            }
            else
            {
                displayProductListViewModel.FilteredProducts = displayProductListViewModel.Products;
            }

        }
    }
}
