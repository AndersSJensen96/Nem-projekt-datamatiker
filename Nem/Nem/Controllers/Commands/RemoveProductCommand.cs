using Nem.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class RemoveProductCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (MessageBox.Show("Er du sikker på du vil fjerne produktet?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				DisplayProductListViewModel displayProductListViewModel = (DisplayProductListViewModel)parameter;
				ProductRepository productRepository = new ProductRepository();
				productRepository.RemoveProductFromDatabase(displayProductListViewModel.SelectedProduct);
				displayProductListViewModel.Products.Remove(displayProductListViewModel.SelectedProduct);
			}
		}
	}
}