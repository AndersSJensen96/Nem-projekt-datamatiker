using Nem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Nem.Controllers
{
	public static class ActiveSession
	{
		#region Fields
		private static string connectionString = DBConnection.ConnectionString;
		#endregion Fields

		#region Properties
		public static User CurrentUser { get; set; }
		#endregion

		#region Methods
		public static bool AuthenticateUser(string username, string password)
		{
			bool result = false;
			SqlConnection connection = new SqlConnection(connectionString);

			try
			{
				connection.Open();
				string query = "SELECT [User].*, Role.Name AS RoleName" +
								" FROM [User]" +
								" INNER JOIN Role ON Role.RoleID = [User].RoleID" +
								" WHERE Username = @username AND Password = @password";

				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.Add(new SqlParameter("@username", username));
				command.Parameters.Add(new SqlParameter("@password", password));

				SqlDataReader reader = command.ExecuteReader();
				if (reader.Read())
				{
					User user;
					if ((string)reader["RoleName"] == "Customer")
					{
						user = new Customer();
					}
					else if ((string)reader["RoleName"] == "Admin")
					{
						user = new Admin();
					}
					else
					{
						user = new Guest();
					}

					//User property population
					user.Name = (string)reader["Name"];
					user.Email = (string)reader["Email"];
					user.Username = (string)reader["Username"];
					user.Password = (string)reader["Password"];
					user.Zipcode = (int)reader["Zipcode"];

					//Set CurrentUser
					CurrentUser = user;
					ActiveShoppingList.SetNewestActiveShoppingList();
					result = true;
					reader.Close();
				}
			}
			catch
			{
				//Insert exception if needed
			}
			finally
			{
				connection.Dispose();
			}
			return result;
		}
		#endregion
	}
}
