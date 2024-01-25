using Nem.Controllers.Interfaces;
using Nem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Nem.Controllers
{
	public class ShoppingListRepository : IRepositoryInterface<ShoppingList>
	{
        #region Fields

        private string connectionString = DBConnection.ConnectionString;

        #endregion Fields
        #region Methods
        public List<ShoppingList> GetAll()
		{
			throw new NotImplementedException();
		}

		public ShoppingList GetById(int id)
		{
			throw new NotImplementedException();
		}

		public ShoppingList GetLatestByUser()
		{
            ShoppingList shoppingList = new ShoppingList();

            SqlConnection latestConnection = new SqlConnection(connectionString);
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                latestConnection.Open();

                string SelectLatestShoppingListSQL = "SELECT TOP 1 * FROM ShoppingList " +
                    "WHERE [User] = @Username " +
                    "ORDER BY SaveDate DESC";

                SqlCommand latestCommand = new SqlCommand(SelectLatestShoppingListSQL, latestConnection);
                latestCommand.Parameters.Add(new SqlParameter("@Username", ActiveSession.CurrentUser.Username));
                SqlDataReader latestReader = latestCommand.ExecuteReader();

                while (latestReader.Read())
                {
                    string selectShoppingListSQL = "GetShoppingListProductsByShoppingListID @ShoppingListID = @InputShoppingListID";

                    SqlCommand command = new SqlCommand(selectShoppingListSQL, connection);
                    command.Parameters.Add(new SqlParameter("@InputShoppingListID", (int)latestReader["ShoppingListID"]));
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        shoppingList.Name = (string)latestReader["Name"];
                        shoppingList.CreationTime = (DateTime)latestReader["SaveDate"];
                        shoppingList.ShoppingListID = (int)latestReader["ShoppingListID"];
                        shoppingList.User = ActiveSession.CurrentUser;

                        ShoppingListProductQuantity shoppingListProductQuantity = new ShoppingListProductQuantity
                        {
                            Quantity = (int)reader["Quantity"],
                            TotalProductUnitValue = (double)reader["TotalProductUnitValue"],
                            ShoppingListProductQuantityID = (int)reader["ShoppingListProductQuantityID"]
                        };

                        Product product = new Product();

                        product.ProductID = (int)reader["ProductID"];
                        product.Name = (string)reader["Name"];
                        product.Description = (string)reader["Description"];
                        product.UnitValue = (double)reader["UnitValue"];
                        product.Price = (double)reader["Price"];
                        switch ((string)reader["unitName"])
                        {
                            case "gram":
                                product.Unit = UnitDefinition.gram;
                                break;
                            case "ml":
                                product.Unit = UnitDefinition.ml;
                                break;
                            default:
                                product.Unit = UnitDefinition.gram;
                                break;

                        }

                        product.Organic = (bool)reader["Organic"];
                        product.Animal = (bool)reader["Animal"];
                        product.Meat = (bool)reader["Meat"];
                        product.Spice = (bool)reader["Spice"];

                        Section section = new Section
                        {
                            SectionID = (int)reader["SectionID"],
                            Name = (string)reader["SectionName"]
                        };

                        Shelf shelf = new Shelf
                        {
                            Name = (string)reader["ShelfName"],
                            ShelfID = (int)reader["ShelfID"],
                            ShelfSection = section
                        };

                        product.ProductShelf = shelf;

                        shoppingListProductQuantity.SelectedProduct = product;

                        shoppingList.ShoppingListProductQuantities.Add(shoppingListProductQuantity);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                connection.Dispose();
            }

            return shoppingList;
        }


        public void InsertShoppingListToDatabase(ShoppingList shoppingList)
        {
			SqlConnection connectionInsert = new SqlConnection(connectionString);
			try
			{
				connectionInsert.Open();
				string insertShoppingListCommand = "INSERT INTO ShoppingList " +
					"([Name], [SaveDate], [User]) OUTPUT INSERTED.ShoppingListID " +
					"VALUES " +
					"(@Name, @SaveDate, @User) ";

				SqlCommand commandRecipe = new SqlCommand(insertShoppingListCommand, connectionInsert);
				commandRecipe.Parameters.Add(new SqlParameter("@Name", shoppingList.Name));
				commandRecipe.Parameters.Add(new SqlParameter("@SaveDate", DateTime.Now));
                commandRecipe.Parameters.Add(new SqlParameter("@User", ActiveSession.CurrentUser.Username));
                
                shoppingList.CreationTime = DateTime.Now;
                shoppingList.User = ActiveSession.CurrentUser;
                //commandRecipe.ExecuteNonQuery();
                int shoppingListID = (int)commandRecipe.ExecuteScalar();
				shoppingList.ShoppingListID = shoppingListID;

				foreach (ShoppingListProductQuantity shoppingListProductQuantity in shoppingList.ShoppingListProductQuantities)
				{
					string insertProductRelationSQL = "INSERT INTO ShoppingListProductQuantity" +
				"(Quantity, TotalProductUnitValue, Product, ShoppingList) " +
				"VALUES" +
				" (@Quantity, @TotalProductUnitValue, @Product, @ShoppingList)";

					SqlCommand commandProductRelation = new SqlCommand(insertProductRelationSQL, connectionInsert);
					commandProductRelation.Parameters.Add(new SqlParameter("@Quantity", shoppingListProductQuantity.Quantity));
					commandProductRelation.Parameters.Add(new SqlParameter("@TotalProductUnitValue", shoppingListProductQuantity.TotalProductUnitValue));
					commandProductRelation.Parameters.Add(new SqlParameter("@Product", shoppingListProductQuantity.SelectedProduct.ProductID));
					commandProductRelation.Parameters.Add(new SqlParameter("@ShoppingList", shoppingList.ShoppingListID));

					commandProductRelation.ExecuteNonQuery();
				}
            }
			catch
			{
			}
			finally
			{
				connectionInsert.Dispose();
			}
		}

        public void UpdateShoppingListInDatabase(ShoppingList shoppingList)
        {
            SqlConnection connectionInsert = new SqlConnection(connectionString);
            SqlConnection connectionSelect = new SqlConnection(connectionString);
            try
            {
                connectionInsert.Open();
                connectionSelect.Open();
                string insertShoppingListCommand = "UPDATE ShoppingList SET" +
                    "[Name] = @Name,[SaveDate] = @SaveDate WHERE ShoppingListID = @ShoppingListID";

                SqlCommand commandShoppingList = new SqlCommand(insertShoppingListCommand, connectionInsert);
                commandShoppingList.Parameters.Add(new SqlParameter("@Name", shoppingList.Name));
                commandShoppingList.Parameters.Add(new SqlParameter("@SaveDate", DateTime.Now));
                commandShoppingList.Parameters.Add(new SqlParameter("@ShoppingListID", shoppingList.ShoppingListID));
                //commandRecipe.ExecuteNonQuery();
                commandShoppingList.ExecuteNonQuery();
                
                foreach (ShoppingListProductQuantity shoppingListProductQuantity in shoppingList.ShoppingListProductQuantities)
                {
                    string checkIfExistsSQL = "SELECT * FROM ShoppingListProductQuantity WHERE Product = @ProductID AND ShoppingList = @ShoppingListID";

                    SqlCommand commandCheckIfExistsSQL = new SqlCommand(checkIfExistsSQL, connectionSelect);
                    commandCheckIfExistsSQL.Parameters.Add(new SqlParameter("@ProductID", shoppingListProductQuantity.SelectedProduct.ProductID));
                    commandCheckIfExistsSQL.Parameters.Add(new SqlParameter("@ShoppingListID", shoppingList.ShoppingListID));

                    SqlDataReader readerCheckIfExists = commandCheckIfExistsSQL.ExecuteReader();
                    if (readerCheckIfExists.Read())
                    {
                        string updateProductRelationSQL = "UPDATE ShoppingListProductQuantity SET " +
                       "Quantity = @Quantity, TotalProductUnitValue = @TotalProductUnitValue " +
                       "WHERE ShoppingList = @ShoppingListID AND Product = @ProductID";

                        SqlCommand commandUpdateProductRelation = new SqlCommand(updateProductRelationSQL, connectionInsert);
                        commandUpdateProductRelation.Parameters.Add(new SqlParameter("@Quantity", shoppingListProductQuantity.Quantity));
                        commandUpdateProductRelation.Parameters.Add(new SqlParameter("@TotalProductUnitValue", shoppingListProductQuantity.TotalProductUnitValue));
                        commandUpdateProductRelation.Parameters.Add(new SqlParameter("@ShoppingListID", shoppingList.ShoppingListID));
                        commandUpdateProductRelation.Parameters.Add(new SqlParameter("@ProductID", shoppingListProductQuantity.SelectedProduct.ProductID));
                        //commandRecipe.ExecuteNonQuery();
                        commandUpdateProductRelation.ExecuteNonQuery();

                        readerCheckIfExists.Close();
                    }
                    else
                    {
                        string insertProductRelationSQL = "INSERT INTO ShoppingListProductQuantity " +
                "(Quantity, TotalProductUnitValue, Product, ShoppingList) " +
                "VALUES" +
                " (@Quantity, @TotalProductUnitValue, @Product, @ShoppingList)";

                        SqlCommand commandProductRelation = new SqlCommand(insertProductRelationSQL, connectionInsert);
                        commandProductRelation.Parameters.Add(new SqlParameter("@Quantity", shoppingListProductQuantity.Quantity));
                        commandProductRelation.Parameters.Add(new SqlParameter("@TotalProductUnitValue", shoppingListProductQuantity.TotalProductUnitValue));
                        commandProductRelation.Parameters.Add(new SqlParameter("@Product", shoppingListProductQuantity.SelectedProduct.ProductID));
                        commandProductRelation.Parameters.Add(new SqlParameter("@ShoppingList", shoppingList.ShoppingListID));

                        commandProductRelation.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                connectionInsert.Dispose();
                connectionSelect.Dispose();
            }
        }
        #endregion
    }
}
