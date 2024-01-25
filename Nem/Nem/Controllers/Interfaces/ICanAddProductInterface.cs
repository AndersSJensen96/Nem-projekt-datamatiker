using System;
using System.Collections.Generic;
using System.Text;

namespace Nem.Controllers.Interfaces
{
	public interface ICanAddProductInterface<T>
	{
		public T SelectedProduct { get; set; }
	}
}
