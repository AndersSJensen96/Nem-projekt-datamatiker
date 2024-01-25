using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Nem.Models
{
	public class ShoppingList : INotifyPropertyChanged
	{
		#region Properties
		public int ShoppingListID { get; set; }
		public string Name { get; set; }
		private DateTime creationTime;

		public DateTime CreationTime
		{
			get
			{
				return creationTime;
			}
			set
			{
				creationTime = value;
				OnPropertyChanged("CreationTime");
			}
		}
		public ObservableCollection<ShoppingListProductQuantity> ShoppingListProductQuantities { get; set; } = new ObservableCollection<ShoppingListProductQuantity>();
		public User User { get; set; }
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
