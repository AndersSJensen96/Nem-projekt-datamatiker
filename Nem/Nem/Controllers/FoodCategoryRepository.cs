using Nem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Nem.Controllers
{
	public class FoodCategoryRepository
	{
		#region Fields

		private string connectionString = DBConnection.ConnectionString;

		#endregion Fields

		#region Methods

		/**
         * Used to select all foodCategories from the database
         */

		public List<FoodCategory> GetAll()
		{
			List<FoodCategory> foodCategories = new List<FoodCategory>();

			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string selectAllFoodCategorySQL = "SELECT * FROM FoodCategory";

				SqlCommand foodCategoryCommand = new SqlCommand(selectAllFoodCategorySQL, connection);

				SqlDataReader foodCategoryReader = foodCategoryCommand.ExecuteReader();

				while (foodCategoryReader.Read())
				{
					FoodCategory foodCategory = new FoodCategory
					{
						FoodCategoryID = (int)foodCategoryReader["FoodCategoryID"],
						Name = (string)foodCategoryReader["Name"],
						Organic = (bool)foodCategoryReader["Organic"],
						Meat = (bool)foodCategoryReader["Meat"],
						Animal = (bool)foodCategoryReader["Animal"]
					};

					foodCategories.Add(foodCategory);
				}
			}
			catch
			{
			}
			finally
			{
				connection.Dispose();
			}

			return foodCategories;
		}

		public FoodCategory GetById(int id)
		{
			FoodCategory foodCategory = new FoodCategory();
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string selectFoodCategorySQL = "SELECT * FROM FoodCategory FoodCategoryID = @FoodCategroyID";

				SqlCommand command = new SqlCommand(selectFoodCategorySQL, connection);
				command.Parameters.Add(new SqlParameter("@FoodCategroyID", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					foodCategory.FoodCategoryID = (int)reader["FoodCategoryID"];
					foodCategory.Name = (string)reader["Name"];
					foodCategory.Organic = (bool)reader["Organic"];
					foodCategory.Animal = (bool)reader["Animal"];
					foodCategory.Meat = (bool)reader["Meat"];
				
				}
			}
			catch
			{
			}
			finally
			{
				connection.Dispose();
			}

			return foodCategory;
		}

		public void InsertFoodCategoryToDatabase(FoodCategory foodCategory)
		{

		}

		public void RemoveFoodCategoryFromDatabase(FoodCategory foodCategory)
		{

		}

		public void UpdateFoodCategoryInDatabase(FoodCategory foodCategory)
		{

		}

		#endregion Methods
	}
}
