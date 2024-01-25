using Nem.Controllers.Commands;
using Nem.Controllers.Interfaces;
using Nem.Models;
using System.Windows.Input;

namespace Nem.ViewModels
{
	public class DisplayProductViewModel : ICanAddProductInterface<Product>
	{
		#region Properties

		public Product SelectedProduct { get; set; }
		#endregion

		#region ICommands
		public ICommand AddProductToShoppingList { get; } = new AddProductToShoppingListCommand();

		#endregion ICommands
	}
}