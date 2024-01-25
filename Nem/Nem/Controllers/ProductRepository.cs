using Nem.Controllers.Interfaces;
using Nem.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Nem.Controllers
{
    public class ProductRepository : IRepositoryInterface<Product>
    {
        #region Fields

        private string connectionString = DBConnection.ConnectionString;
        private ShelfRepository shelfRepository = new ShelfRepository();
        #endregion Fields

        #region Methods
        #region SelectSQLs
        /**
         * This is used to get specific products by searchfrom the database and 
         * craete product instances, shelf instances, and section instances
         */
        //public List<Product> GetBySearch(string input, bool organic, bool animal, bool meat, bool spice)
        public List<Product> GetBySearch(string input)
        {
            List<Product> products = new List<Product>();

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
				string selectAllProductsSQL = "ProductsByStringSearch @Name = @input";

				SqlCommand command = new SqlCommand(selectAllProductsSQL, connection);
                command.Parameters.Add(new SqlParameter("@input", input));

                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
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
                            product.Unit = UnitDefinition.stk;
                            break;

                    }

                    product.Organic = (bool)reader["Organic"];
                    product.Animal = (bool)reader["Animal"];
                    product.Meat = (bool)reader["Meat"];
                    product.Spice = (bool)reader["Spice"];

                    Shelf shelf = shelfRepository.GetById((int)reader["ShelfID"]);

                    product.ProductShelf = shelf;

                    products.Add(product);
                }
            }
            catch
            {
            }
            finally
            {
                connection.Dispose();
            }

            return products;
        }

        /**
         * This is used to get all products from the database and create product instances, shelf instances, and section instances
         */
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string selectAllProductsSQL = "EXEC GetAllProducts";

                SqlCommand command = new SqlCommand(selectAllProductsSQL, connection);

                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
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
                            product.Unit = UnitDefinition.stk;
                            break;
                    }

                    product.Organic = (bool)reader["Organic"];
                    product.Animal = (bool)reader["Animal"];
                    product.Meat = (bool)reader["Meat"];
                    product.Spice = (bool)reader["Spice"];

                    Shelf shelf = shelfRepository.GetById((int)reader["ShelfID"]);

                    product.ProductShelf = shelf;

                    products.Add(product);
                }
            }
            catch
            {
            }
            finally
            {
                connection.Dispose();
            }

            return products;
        }

        /**
         * This is used to get a product from the database and craete product instances, shelf instances, and section instances
         */
        public Product GetById(int id)
        {
            Product product = new Product();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string selectProductSQL = "SELECT " +
                    "Product.*, UnitType.Name as unitName" +
                    " FROM Product" +
                    " INNER JOIN UnitType ON UnitType.UnitTypeID = Product.UnitTypeID" +
                    " WHERE Product.ProductID = @ProductID";

                SqlCommand command = new SqlCommand(selectProductSQL, connection);
                command.Parameters.Add(new SqlParameter("@ProductID", id));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
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
                            product.Unit = UnitDefinition.stk;
                            break;
                    }

                    product.Organic = (bool)reader["Organic"];
                    product.Animal = (bool)reader["Animal"];
                    product.Meat = (bool)reader["Meat"];
                    product.Spice = (bool)reader["Spice"];

                    Shelf shelf = shelfRepository.GetById((int)reader["ShelfID"]);

                    product.ProductShelf = shelf;
                }
            }
            catch
            {
            }
            finally
            {
                connection.Dispose();
            }
            return product;
        }
		#endregion
		/* 
		 * This method is used to insert the Product into the database, using the product object recieved from the viewmodel
		 */
		#region Product C/U/D sql methods
		public void InsertProductToDatabase(Product product)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string insertProductSQL = "INSERT INTO Product" +
                "(Name, Description, UnitValue, UnitTypeID, Organic, Animal, Meat, Spice, ShelfID, Price) " +
                "VALUES" +
                " (@Name, @Description,@UnitValue,@UnitTypeID,@Organic,@Animal,@Meat,@Spice,@ShelfID, @Price)";

                SqlCommand command = new SqlCommand(insertProductSQL, connection);
                command.Parameters.Add(new SqlParameter("@Name", product.Name));
                command.Parameters.Add(new SqlParameter("@Description", product.Description));
                command.Parameters.Add(new SqlParameter("@UnitValue", product.UnitValue));
                command.Parameters.Add(new SqlParameter("@Organic", product.Organic));
                command.Parameters.Add(new SqlParameter("@Animal", product.Animal));
                command.Parameters.Add(new SqlParameter("@Meat", product.Meat));
                command.Parameters.Add(new SqlParameter("@Spice", product.Spice));
                command.Parameters.Add(new SqlParameter("@UnitTypeID", (int)product.Unit));
                command.Parameters.Add(new SqlParameter("@ShelfID", product.ProductShelf.ShelfID));
                command.Parameters.Add(new SqlParameter("@Price", product.Price));

                command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                connection.Dispose();
            }
        }
		/**
         * Sql method to remove a product from the database
         */
		public void RemoveProductFromDatabase(Product product)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string insertProductSQL = "DELETE FROM Product WHERE " +
				"ProductID = @ProductID";

				SqlCommand command = new SqlCommand(insertProductSQL, connection);
				command.Parameters.Add(new SqlParameter("@ProductID", product.ProductID));

				command.ExecuteNonQuery();
			}
			catch
			{
			}
			finally
			{
				connection.Dispose();
			}
		}

        /* 
		 * This method is used to update the Product in the database, using the product object recieved from the viewmodel
		 */
        public void UpdateProductInDatabase(Product product)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string updateProductSQL = "UPDATE Product SET" +
                "Name = @Name, " +
                "Description = @Description, " +
                "UnitValue = @UnitValue, " +
                "Unit = @Unit, " +
                "Organic = @Organic, " +
                "Animal = @Animal, " +
                "Meat = @Meat, " +
                "Spice = @Spice, " +
                "ShelfID = @ShelfID " +
                "Price = @Price" +
                "WHERE ProductID = @ProductID";

                SqlCommand command = new SqlCommand(updateProductSQL, connection);
                command.Parameters.Add(new SqlParameter("@Name", product.Name));
                command.Parameters.Add(new SqlParameter("@Description", product.Description));
                command.Parameters.Add(new SqlParameter("@UnitValue", product.UnitValue));
                command.Parameters.Add(new SqlParameter("@Unit", product.Unit));
                command.Parameters.Add(new SqlParameter("@Organic", product.Organic));
                command.Parameters.Add(new SqlParameter("@Animal", product.Animal));
                command.Parameters.Add(new SqlParameter("@Meat", product.Meat));
                command.Parameters.Add(new SqlParameter("@Spice", product.Spice));
                command.Parameters.Add(new SqlParameter("@ShelfID", product.ProductShelf.ShelfID));
                command.Parameters.Add(new SqlParameter("@Price", product.Price));
                command.Parameters.Add(new SqlParameter("@ProductID", product.ProductID));

                command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                connection.Dispose();
            }
        }
		#endregion

		#endregion Methods
	}
}