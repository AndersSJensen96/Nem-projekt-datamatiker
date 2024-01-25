using Nem.Controllers;
using Nem.Controllers.Commands;
using Nem.Controllers.Interfaces;
using Nem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nem.ViewModels
{
    public class DisplayShoppingListViewModel : INotifyPropertyChanged, ICanAddProductInterface<Product>
	{
		#region field
		private float totalPrice;
		#endregion
		#region Properties
		public ShoppingList ShoppingList { get; set; }
		public string SearchBoxInput { get; set; } = "";
		public Product SelectedProduct { get; set; }
		private ObservableCollection<Product> foundProducts;
		public ObservableCollection<Product> FoundProducts
		{ 
			get
			{
				return foundProducts;
			}
			set
			{
				foundProducts = value;
				OnPropertyChanged("FoundProducts");
			} 
		}

		public float TotalPrice { 
			get
			{
				return totalPrice;
			}  
			set
			{
				totalPrice = value;
				OnPropertyChanged("TotalPrice");
			} 
		}

		public ShoppingListProductQuantity SelectedShoppingListProductQuantity { get;  set; }
		#endregion

		#region Commands

		public ICommand IncreaseProductQuantityInShoppingList { get; } = new IncreaseProductQuantityInShoppingListCommand();
		public ICommand DecreaseProductQuantityInShoppingList { get; } = new DecreaseProductQuantityInShoppingListCommand();
		public ICommand RemoveProductQuantityInShoppingList { get; } = new RemoveProductQuantityInShoppingListCommand();
		public ICommand FindProductCommand { get; } = new FindProductCommand();
		public ICommand AddProductToShoppingList { get; } = new AddProductToShoppingListCommand();
		public ICommand SaveShoppingList { get; } = new AddShoppingListCommand();
		#endregion

		#region Constructor
		public DisplayShoppingListViewModel()
        {
            ShoppingList = ActiveShoppingList.ShoppingList;
            CalculateTotalPrice();
		}
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		#region Methods
		public void CalculateTotalPrice()
		{
			double totalPrice = 0.0;
            foreach (ShoppingListProductQuantity shoppingListProductQuantity in ShoppingList.ShoppingListProductQuantities)
            {
				totalPrice += shoppingListProductQuantity.Quantity * shoppingListProductQuantity.SelectedProduct.Price;
			}
			TotalPrice = (float)totalPrice;
		}

		private void OnPropertyChanged(string info)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion
	}
}
