using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nem.Models
{
	public class ShoppingListProductQuantity : INotifyPropertyChanged
	{
		#region Properties
		public int ShoppingListProductQuantityID { get; set; }
		private int quantity;
		public int Quantity 
		{
			get 
			{
				return quantity;
			} 
			set 
			{
				quantity = value;
				OnPropertyChanged("Quantity");
			}
		}
		public double TotalProductUnitValue { get; set; }
		public Product SelectedProduct { get; set; }
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		#region Methods
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
