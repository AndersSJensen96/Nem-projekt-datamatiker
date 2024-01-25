using Nem.Models;
using Nem.ViewModels;
using System;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class AddProductCommand : ICommand
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

			AddProductViewModel addProductViewModel = (AddProductViewModel)parameter;
			ProductRepository productRepository = new ProductRepository();
			if (CheckBool(addProductViewModel))
			{
				UnitDefinition unitDefinition;
				switch(addProductViewModel.Unit)
				{
					case "gram":
						unitDefinition = UnitDefinition.gram;
						break;

					case "ml":
						unitDefinition = UnitDefinition.ml;
						break;

					default:
						unitDefinition = UnitDefinition.stk;
						break;
				}

				Product product = new Product()
				{
					Name = addProductViewModel.Name,
					Description = addProductViewModel.Description,
					Unit = unitDefinition,
					UnitValue = addProductViewModel.UnitValue,
					Organic = addProductViewModel.Organic,
					Animal = addProductViewModel.Animal,
					Meat = addProductViewModel.Meat,
					Spice = addProductViewModel.Spice,
					ProductShelf = addProductViewModel.ProductShelf,
					Price = addProductViewModel.Price
				};
				productRepository.InsertProductToDatabase(product);
			}
		}

		#region supplerende metoder

		private bool CheckBool(AddProductViewModel ViewModel)
		{
			bool check = false;
			if (ViewModel.Name != null && ViewModel.Description != null && ViewModel.UnitValue > 0.0 && ViewModel.ProductShelf != null)
			{
				check = true;
			}
			return check;
		}

		#endregion supplerende metoder
	}
}