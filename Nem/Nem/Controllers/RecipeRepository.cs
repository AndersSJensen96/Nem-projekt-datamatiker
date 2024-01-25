using Nem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Nem.Controllers
{
    public class RecipeRepository 
    {
        #region Fields

        private string connectionString = DBConnection.ConnectionString;
		private ProductRepository productRepository = new ProductRepository();
		private MealtimeRepository mealtiemRepository = new MealtimeRepository();
		private FoodCategoryRepository foodCategoryRepository = new FoodCategoryRepository();
		#endregion Fields

		#region Methods

		public List<Recipe> GetAll()
        {
            List<Recipe> recipes = new List<Recipe>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlConnection connectionTwo = new SqlConnection(connectionString);
			try
            {
                connection.Open();
                connectionTwo.Open();

				List<Mealtime> mealtimes = mealtiemRepository.GetAll();
				List<FoodCategory> foodCategories = foodCategoryRepository.GetAll();

				string selectAllRecipesSQL = "SELECT * FROM Recipe";

                SqlCommand allRecipesCommand = new SqlCommand(selectAllRecipesSQL, connection);
                SqlDataReader allRecipesReader = allRecipesCommand.ExecuteReader();

				while (allRecipesReader.Read())
				{
					Recipe recipe = new Recipe();

					recipe.RecipeID = (int)allRecipesReader["RecipeID"];
					recipe.Name = (string)allRecipesReader["Name"];
					recipe.Description = (string)allRecipesReader["Description"];
					recipe.Difficulty = (int)allRecipesReader["Difficulty"];
					recipe.CookingTime = (int)allRecipesReader["CookingTime"];
					recipe.CreationTime = (DateTime)allRecipesReader["CreationTime"];
					recipe.Organic = (bool)allRecipesReader["Organic"];
					recipe.Animal = (bool)allRecipesReader["Animal"];
					recipe.Meat = (bool)allRecipesReader["Meat"];
					recipe.NumberOfPersons = (int)allRecipesReader["NumberOfPersons"];

					//This line tried to make a new instance of a user, Changed it to customer instead since it's a customer that makes recipies.
					User recipeUser = new Customer
					{
						Username = (string)allRecipesReader["User"] 
					};

					recipe.CurrentUser = recipeUser;

					#region DeletedCode
					/*
					string selectProductQuantitysSQL = "SELECT * FROM ProductQuantity WHERE Recipe = @RecipeID ";

					SqlCommand allProductQuantityCommand = new SqlCommand(selectProductQuantitysSQL, connectionTwo);
					allProductQuantityCommand.Parameters.Add(new SqlParameter("@RecipeID", recipe.RecipeID));
					using (SqlDataReader allProductQuantityReader = allProductQuantityCommand.ExecuteReader())
					{
						while (allProductQuantityReader.Read())
						{
							Product product = productRepository.GetById((int)allProductQuantityReader["Product"]);

							ProductQuantity productQuantity = new ProductQuantity
							{
								ProductQuantityID = (int)allProductQuantityReader["ProductQuantityID"],
								UserUnit = (string)allProductQuantityReader["UserUnit"],
								UserUnitValue = (double)allProductQuantityReader["UserUnitValue"],
								ProductUnitValue = (double)allProductQuantityReader["ProductUnitValue"],
								ProductQuantityRecipeyProperty = recipe,
								SelectedProduct = product
							};

							recipe.ProductQuantities.Add(productQuantity);
						}
					}*/
					#endregion

					string selectAllMealtimeSQL = "SELECT * FROM RecipeMealtime WHERE Recipe = @RecipeID";

					SqlCommand allMealtimeCommand = new SqlCommand(selectAllMealtimeSQL, connectionTwo);
					allMealtimeCommand.Parameters.Add(new SqlParameter("@RecipeID", recipe.RecipeID));
					using (SqlDataReader allMealtimeReader = allMealtimeCommand.ExecuteReader()) 
					{
						while (allMealtimeReader.Read())
						{
							Mealtime mealtime = mealtimes.FirstOrDefault(x => (x.MealtimeID == (int)allMealtimeReader["Mealtime"]));
							recipe.Mealtimes.Add(mealtime);
						}
					}

					string selectAllCategoriesSQL = "SELECT * FROM RecipeFoodCategory WHERE RecipeFoodCategory.Recipe = @RecipeID";

					SqlCommand allFoodCategoriesCommand = new SqlCommand(selectAllCategoriesSQL, connectionTwo);
					allFoodCategoriesCommand.Parameters.Add(new SqlParameter("@RecipeID", recipe.RecipeID));
					using (SqlDataReader allFoodCategoriesReader = allFoodCategoriesCommand.ExecuteReader())
					{
						while (allFoodCategoriesReader.Read())
						{
							FoodCategory foodCategory = foodCategories.FirstOrDefault(x => (x.FoodCategoryID == (int)allFoodCategoriesReader["FoodCategory"]));
							recipe.FoodCategories.Add(foodCategory);
						}
					}
					recipes.Add(recipe);
				}
			} 
			catch
			{

			}
			finally
			{
				connection.Dispose();
				connectionTwo.Dispose();
			}

			return recipes;
		}
		/**
		 * Get single recipe sql
		 */
		public Recipe GetById(Recipe recipe)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			SqlConnection connectionTwo = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				connectionTwo.Open();
				string selectAllRecipesSQL = "SELECT * FROM Recipe " +
					"WHERE RecipeID = @RecipeID";

				SqlCommand singleRecipesCommand = new SqlCommand(selectAllRecipesSQL, connection);
				singleRecipesCommand.Parameters.Add(new SqlParameter("@RecipeID", recipe.RecipeID));
				SqlDataReader singleRecipesReader = singleRecipesCommand.ExecuteReader();

				while (singleRecipesReader.Read())
				{
					recipe.Name = (string)singleRecipesReader["Name"];
					recipe.Description = (string)singleRecipesReader["Description"];
					recipe.Difficulty = (int)singleRecipesReader["Difficulty"];
					recipe.CookingTime = (int)singleRecipesReader["CookingTime"];
					recipe.CreationTime = (DateTime)singleRecipesReader["CreationTime"];
					recipe.Organic = (bool)singleRecipesReader["Organic"];
					recipe.Animal = (bool)singleRecipesReader["Animal"];
					recipe.Meat = (bool)singleRecipesReader["Meat"];
					recipe.RecipeID = (int)singleRecipesReader["RecipeID"];
					recipe.NumberOfPersons = (int)singleRecipesReader["NumberOfPersons"];

					string selectProductQuantitysSQL = "SELECT * FROM ProductQuantity WHERE Recipe = @RecipeID ";

					SqlCommand allProductQuantityCommand = new SqlCommand(selectProductQuantitysSQL, connectionTwo);
					allProductQuantityCommand.Parameters.Add(new SqlParameter("@RecipeID", recipe.RecipeID));
					SqlDataReader allProductQuantityReader = allProductQuantityCommand.ExecuteReader();
					while (allProductQuantityReader.Read())
					{
						Product product = productRepository.GetById((int)allProductQuantityReader["Product"]);

						ProductQuantity productQuantity = new ProductQuantity();

						productQuantity.ProductQuantityID = (int)allProductQuantityReader["ProductQuantityID"];
						productQuantity.UserUnit = (string)allProductQuantityReader["UserUnit"];
						productQuantity.UserUnitValue = (double)allProductQuantityReader["UserUnitValue"];
						productQuantity.ProductUnitValue = (double)allProductQuantityReader["ProductUnitValue"];
						productQuantity.ProductQuantityRecipeyProperty = recipe;
						productQuantity.SelectedProduct = product;

						recipe.ProductQuantities.Add(productQuantity);
					}


					string selectAllMealtimeSQL = "SELECT * FROM RecipeMealtime WHERE Recipe = @RecipeID";

					SqlCommand allMealtimeCommand = new SqlCommand(selectAllMealtimeSQL, connectionTwo);
					allMealtimeCommand.Parameters.Add(new SqlParameter("@RecipeID", recipe.RecipeID));
					using (SqlDataReader allMealtimeReader = allMealtimeCommand.ExecuteReader())
					{
						while (allMealtimeReader.Read())
						{
							Mealtime mealtime = mealtiemRepository.GetById((int)allMealtimeReader["Mealtime"]);
							recipe.Mealtimes.Add(mealtime);
						}
					}

					string selectAllCategoriesSQL = "SELECT * FROM RecipeFoodCategory WHERE RecipeFoodCategory.Recipe = @RecipeID";

					SqlCommand allFoodCategoriesCommand = new SqlCommand(selectAllCategoriesSQL, connectionTwo);
					allFoodCategoriesCommand.Parameters.Add(new SqlParameter("@RecipeID", recipe.RecipeID));
					using (SqlDataReader allFoodCategoriesReader = allFoodCategoriesCommand.ExecuteReader())
					{
						while (allFoodCategoriesReader.Read())
						{
							FoodCategory foodCategory = foodCategoryRepository.GetById((int)allFoodCategoriesReader["FoodCategory"]);
							recipe.FoodCategories.Add(foodCategory);
						}
					}
				}
			}
			catch
			{

			}
			finally
			{
				connectionTwo.Dispose();
				connection.Dispose();
			}

			return recipe;
		}

		public void InsertRecipeToDatabase(Recipe recipe)
		{
			SqlConnection connectionInsert = new SqlConnection(connectionString);
            try
			{
				connectionInsert.Open();
				string insertRecipeCommand = "INSERT INTO Recipe " +
					"([Name], [Description], [Difficulty], [CookingTime], [User], [NumberOfPersons]) OUTPUT INSERTED.RecipeID " +
                    "VALUES " +
					"(@Name, @Description, @Difficulty, @CookingTime, @User, @NumberOfPersons) ";

				SqlCommand commandRecipe = new SqlCommand(insertRecipeCommand, connectionInsert);
				commandRecipe.Parameters.Add(new SqlParameter("@Name", recipe.Name));
				commandRecipe.Parameters.Add(new SqlParameter("@Description", recipe.Description));
				commandRecipe.Parameters.Add(new SqlParameter("@Difficulty", recipe.Difficulty));
				commandRecipe.Parameters.Add(new SqlParameter("@CookingTime", recipe.CookingTime));
				commandRecipe.Parameters.Add(new SqlParameter("@NumberOfPersons", recipe.NumberOfPersons));
				commandRecipe.Parameters.Add(new SqlParameter("@User", ActiveSession.CurrentUser.Username));

				//commandRecipe.ExecuteNonQuery();
				int recipeId = (int)commandRecipe.ExecuteScalar();
				recipe.RecipeID = recipeId;

				foreach (FoodCategory foodCategory in recipe.FoodCategories)
				{
					string insertFoodCategoryRelationSQL = "INSERT INTO RecipeFoodCategory" +
				"(Recipe, FoodCategory) " +
				"VALUES" +
				" (@Recipe, @FoodCategory)";

					SqlCommand commandFoodCategory = new SqlCommand(insertFoodCategoryRelationSQL, connectionInsert);
					commandFoodCategory.Parameters.Add(new SqlParameter("@Recipe", recipe.RecipeID));
					commandFoodCategory.Parameters.Add(new SqlParameter("@FoodCategory", foodCategory.FoodCategoryID));

					commandFoodCategory.ExecuteNonQuery();
				}

				foreach (Mealtime mealtime in recipe.Mealtimes)
				{
					string insertMealtimeRelationSQL = "INSERT INTO RecipeMealtime" +
				"(Recipe, Mealtime) " +
				"VALUES" +
				" (@Recipe, @Mealtime)";

					SqlCommand commandMealtime = new SqlCommand(insertMealtimeRelationSQL, connectionInsert);
					commandMealtime.Parameters.Add(new SqlParameter("@Recipe", recipe.RecipeID));
					commandMealtime.Parameters.Add(new SqlParameter("@Mealtime", mealtime.MealtimeID));

					commandMealtime.ExecuteNonQuery();
				}

				bool isMeatIncluded = false;
				bool isOrganicIncluded = false;
				bool isAnimalIncluded = false;

				foreach (ProductQuantity productQuantity in recipe.ProductQuantities)
				{
					string insertProductRelationSQL = "INSERT INTO ProductQuantity" +
				"(UserUnitValue, UserUnit, ProductUnitValue, Product, Recipe) " +
				"VALUES" +
				" (@UserUnitValue, @UserUnit, @ProductUnitValue, @Product, @Recipe)";

					SqlCommand commandProductRelation = new SqlCommand(insertProductRelationSQL, connectionInsert);
					commandProductRelation.Parameters.Add(new SqlParameter("@UserUnitValue", productQuantity.UserUnitValue));
					commandProductRelation.Parameters.Add(new SqlParameter("@UserUnit", productQuantity.UserUnit));
					commandProductRelation.Parameters.Add(new SqlParameter("@ProductUnitValue", productQuantity.ProductUnitValue));
					commandProductRelation.Parameters.Add(new SqlParameter("@Recipe", recipe.RecipeID));
					commandProductRelation.Parameters.Add(new SqlParameter("@Product", productQuantity.SelectedProduct.ProductID));
					
					if (productQuantity.SelectedProduct.Meat)
					{
						isMeatIncluded = true;
					}
					if (productQuantity.SelectedProduct.Organic)
					{
						isOrganicIncluded = true;
					}
					if (productQuantity.SelectedProduct.Animal)
					{
						isAnimalIncluded = true;
					}

					commandProductRelation.ExecuteNonQuery();
				}

				recipe.Organic = isOrganicIncluded;
				recipe.Meat = isMeatIncluded;
				recipe.Animal = isAnimalIncluded;
				string updateRecipeCommand = "UPDATE Recipe " +
					"SET [Organic] = @Organic, [Meat] = @Meat, [Animal] = @Animal " +
					"WHERE RecipeID = @RecipeID";

				SqlCommand commandUpdateRecipe = new SqlCommand(updateRecipeCommand, connectionInsert);
				commandUpdateRecipe.Parameters.Add(new SqlParameter("@Organic", isOrganicIncluded));
				commandUpdateRecipe.Parameters.Add(new SqlParameter("@Meat", isMeatIncluded));
				commandUpdateRecipe.Parameters.Add(new SqlParameter("@Animal", isAnimalIncluded));
				commandUpdateRecipe.Parameters.Add(new SqlParameter("@RecipeID", recipe.RecipeID));

				commandUpdateRecipe.ExecuteNonQuery();
			}
			catch
			{
			}
			finally
			{
				connectionInsert.Dispose();
			}
		}

		public void UpdateRecipeInDatabase(Recipe recipe)
		{

		}

		public void RemoveRecipeFromDatabase(Recipe recipe)
		{

		}
		#endregion Methods

	}
}
