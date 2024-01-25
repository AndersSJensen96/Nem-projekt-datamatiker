using Nem.Controllers.Interfaces;
using Nem.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Nem.Controllers
{
	public class ShelfRepository
	{
		#region Fields

		private string connectionString = DBConnection.ConnectionString;
		private SectionRepository sectionRepository = new SectionRepository();
		#endregion Fields

		#region Methods

		/**
         * Used to select all shelfs from the database
         */

		public List<Shelf> GetAllShelfs()
		{
			List<Shelf> shelfs = new List<Shelf>();

			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string selectAllShelfsSQL = "SELECT " +
					"Shelf.*, Section.Name as SectionName" +
					" FROM Shelf " +
					" INNER JOIN Section ON Section.SectionID = Shelf.SectionID";

				SqlCommand command = new SqlCommand(selectAllShelfsSQL, connection);

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Section section = sectionRepository.GetById((int)reader["SectionID"]);

					Shelf shelf = new Shelf
					{
						Name = (string)reader["Name"],
						ShelfID = (int)reader["ShelfID"],
						ShelfSection = section
					};

					shelfs.Add(shelf);
				}
			}
			catch
			{
			}
			finally
			{
				connection.Dispose();
			}

			return shelfs;
		}

		public Shelf GetById(int id)
		{
			Shelf shelf = new Shelf();
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string selectAllShelfsSQL = "SELECT " +
					"Shelf.*" +
					" FROM Shelf " +
					" WHERE ShelfID = @ShelfID";

				SqlCommand command = new SqlCommand(selectAllShelfsSQL, connection);
				command.Parameters.Add(new SqlParameter("@ShelfID", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Section section = sectionRepository.GetById((int)reader["SectionID"]);
					
					shelf.Name = (string)reader["Name"];
					shelf.ShelfID = (int)reader["ShelfID"];
					shelf.ShelfSection = section;
				}
			}
			catch
			{
			}
			finally
			{
				connection.Dispose();
			}

			return shelf;
		}

		/*
		 * This method is used to insert a Shelf into the database, using the shelf object recieved from the viewmodel
		 */

		public void InsertShelfToDatabase(Shelf shelf)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string insertShelfSQL = "INSERT INTO Shelf" +
				"(Name, SectionID)" +
				" VALUES " +
				"(@Name, @SectionID)";

				SqlCommand command = new SqlCommand(insertShelfSQL, connection);
				command.Parameters.Add(new SqlParameter("@Name", shelf.Name));
				command.Parameters.Add(new SqlParameter("@SectionID", shelf.ShelfSection.SectionID));
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
		 * This method is used to update a Shelf in the database, using the shelf object recieved from the viewmodel
		 */

		public void UpdateShelfInDatabase(Shelf shelf)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string insertShelfSQL = "UPDATE Shelf SET" +
				"Name = @Name, SectionID = @SectionID" +
				" WHERE " +
				"ShelfID = @ShelfID";

				SqlCommand command = new SqlCommand(insertShelfSQL, connection);
				command.Parameters.Add(new SqlParameter("@Name", shelf.Name));
				command.Parameters.Add(new SqlParameter("@SectionID", shelf.ShelfSection.SectionID));
				command.Parameters.Add(new SqlParameter("@ShelfID", shelf.ShelfID));
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

		#endregion Methods
	}
}