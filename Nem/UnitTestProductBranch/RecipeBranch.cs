using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nem.Controllers;
using Nem.Models;
using System.Collections.Generic;

namespace UnitTestProductBranch
{
	[TestClass]
	public class RecipeBranch
	{
		RecipeRepository recipeRepository;
		FoodCategoryRepository foodCategoryRepository;
		MealtimeRepository mealtimeRepository;

		[TestInitialize]
		public void init()
		{
			recipeRepository = new RecipeRepository();
			foodCategoryRepository = new FoodCategoryRepository();
			mealtimeRepository = new MealtimeRepository();
		}

		[TestMethod]
		public void RecipeRepositoryGetAll()
		{
			List<Recipe> recipeList = recipeRepository.GetAll();
			Assert.IsNotNull(recipeList);
			Assert.IsTrue(recipeList.Count > 0, "List has been populated");
		}
		[TestMethod]
		public void FoodCategoryRepositoryGetAll()
		{
			List<FoodCategory> foodCategoryList = foodCategoryRepository.GetAll();
			Assert.IsNotNull(foodCategoryList);
			Assert.IsTrue(foodCategoryList.Count > 0, "List has been populated");
		}
		[TestMethod]
		public void MealtimeRepositoryGetAll()
		{
			List<Mealtime> MealtimeList = mealtimeRepository.GetAll();
			Assert.IsNotNull(MealtimeList);
			Assert.IsTrue(MealtimeList.Count > 0, "List has been populated");
		}
	}
}
