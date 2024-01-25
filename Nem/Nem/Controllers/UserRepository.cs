using System;
using System.Collections.Generic;
using System.Text;
using Nem.Models;
using Nem.Controllers.Interfaces;
using System.Data.SqlClient;

namespace Nem.Controllers
{
	public class UserRepository : IRepositoryInterface<User>
	{
		private string connectionString = DBConnection.ConnectionString;

		public List<User> GetAll()
		{
			throw new NotImplementedException();
		}

		public User GetById(int id)
		{
			throw new NotImplementedException();
		}

		//CreateUser method to Insert a User in the database
		public void CreateUser(User user)
		{
			SqlConnection connection = new SqlConnection(connectionString);

			try
			{
				connection.Open();

				//SQL Sentence
				string InsertUserSQL = "INSERT INTO [USER](Name, Username, Password, Email, ZipCode, RoleID) " +
										"VALUES(@Name, @Username, @Password, @Email, @ZipCode, @Role)";

				//Command
				SqlCommand command = new SqlCommand(InsertUserSQL, connection);

				//Parameters
				command.Parameters.Add(new SqlParameter("@Name", user.Name));
				command.Parameters.Add(new SqlParameter("@Username", user.Username));
				command.Parameters.Add(new SqlParameter("@Password", user.Password));
				command.Parameters.Add(new SqlParameter("@Email", user.Email));
				command.Parameters.Add(new SqlParameter("@ZipCode", user.Zipcode));
				//Check if the User is a Customer or Admin
				if (user is Customer)
				{
					command.Parameters.Add(new SqlParameter("@Role", 2));
				}
				else if (user is Admin)
				{
					command.Parameters.Add(new SqlParameter("@Role", 1));
				}

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
	}
}
