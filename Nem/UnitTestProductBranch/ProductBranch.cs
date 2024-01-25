using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nem.Controllers;
using Nem.Models;
using System.Collections.Generic;

namespace UnitTestProductBranch
{
	[TestClass]
	public class ProductBranch
	{
		ProductRepository productRepository;
		ShelfRepository shelfRepository;

		[TestInitialize]
		public void init()
		{
			productRepository = new ProductRepository();
			shelfRepository = new ShelfRepository();
		}

		[TestMethod]
		public void ProductRepositoryGetAll()
		{
			List<Product> productList = productRepository.GetAll();
			Assert.IsNotNull(productList);
			Assert.IsTrue(productList.Count > 0, "The product list get all existing items in the database");
		}
		[TestMethod]
		public void ShelfRepositoryGetAll()
		{
			List<Shelf> shelfList = shelfRepository.GetAllShelfs();
			Assert.IsNotNull(shelfList);
			Assert.IsTrue(shelfList.Count > 0, "All shelfs from database");
		}
	}
}
