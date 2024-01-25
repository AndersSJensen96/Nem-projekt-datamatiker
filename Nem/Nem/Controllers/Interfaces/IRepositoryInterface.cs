using System;
using System.Collections.Generic;
using System.Text;

namespace Nem.Controllers.Interfaces
{
	public interface IRepositoryInterface<T>
	{
		public List<T> GetAll();
		public T GetById(int id);
	}
}
