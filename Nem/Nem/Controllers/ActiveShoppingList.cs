using Nem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using NUnit.Framework;

namespace Nem.Controllers
{
    public static class ActiveShoppingList
    {
        #region Properties
        public static ShoppingList ShoppingList { get; set; } = new ShoppingList();
        #endregion

        public static void AddRecipeToShoppingList(Recipe recipe, int totalPeople)
        {
            foreach (ProductQuantity item in recipe.ProductQuantities)
            {
				double totalQuantity = (totalPeople / recipe.NumberOfPersons);
				ProductQuantityToShoppingListProductQuantity(item, totalQuantity);
            }
        }
        public static void AddProductToShoppingList(Product product, int amount)
        {
			ShoppingListProductQuantity shoppingListProductQuantity = new ShoppingListProductQuantity();
			shoppingListProductQuantity.SelectedProduct = product;
			shoppingListProductQuantity.TotalProductUnitValue = product.UnitValue*amount;
			shoppingListProductQuantity.Quantity = amount;
			FindExsistingShoppingListProductQuantity(shoppingListProductQuantity);

		}
		private static void FindExsistingShoppingListProductQuantity(ShoppingListProductQuantity shoppingListProductQuantity)
		{
			// This is used to find a product in the shoppingList if the product allready exsits, it uses LINQ Where and 
			// FirstOrDefault, at the same time lambda is used to select the product and match it against the ID
			ShoppingListProductQuantity exsistingShoppingListProductQuantity = ShoppingList.ShoppingListProductQuantities
				.Where(shoppingListProductQ =>
				shoppingListProductQ.SelectedProduct.ProductID.Equals(shoppingListProductQuantity.SelectedProduct.ProductID))
				.FirstOrDefault();
			 
			if (exsistingShoppingListProductQuantity != null)
			{
				double perItemValue = shoppingListProductQuantity.SelectedProduct.UnitValue; // how mush there is in one product
				double neededValue = shoppingListProductQuantity.TotalProductUnitValue; // how much is need for the recipie

				neededValue += exsistingShoppingListProductQuantity.TotalProductUnitValue;
				exsistingShoppingListProductQuantity.TotalProductUnitValue = neededValue;
				exsistingShoppingListProductQuantity.Quantity = (int)Math.Ceiling(neededValue / perItemValue);
			}
			else 
			{
				ShoppingList.ShoppingListProductQuantities.Add(shoppingListProductQuantity);
			}
		}
		public static void ProductQuantityToShoppingListProductQuantity(ProductQuantity productQuantity, double percentNeeded)
        {
			ShoppingListProductQuantity shoppingListProductQuantity = new ShoppingListProductQuantity();
			double perItemValue = productQuantity.SelectedProduct.UnitValue; // how mush there is in one product
			double neededValue = productQuantity.ProductUnitValue; // how much is need for the recipie
			shoppingListProductQuantity.SelectedProduct = productQuantity.SelectedProduct;
			shoppingListProductQuantity.Quantity = (int)Math.Ceiling(percentNeeded * (neededValue / perItemValue)); // how many units of the product there is needed rounded up
			shoppingListProductQuantity.TotalProductUnitValue = neededValue * percentNeeded;

			FindExsistingShoppingListProductQuantity(shoppingListProductQuantity);
		}

		public static void SetNewestActiveShoppingList()
		{
			ShoppingListRepository shoppingListRepository = new ShoppingListRepository();
			ShoppingList = shoppingListRepository.GetLatestByUser();
		}
    }
}
