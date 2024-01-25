using Nem.Models;
using System.Data.SqlClient;

namespace Nem.Controllers
{
	public class SectionRepository
	{
		#region Fields

		private string connectionString = DBConnection.ConnectionString;

		#endregion Fields

		#region Methods

		/*
		 * This method is used to insert a Section into the database, using the shelf object recieved from the viewmodel
		 */

		public void InsertSectionToDatabase(Section section)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string insertShelfSQL = "INSERT INTO Section" +
				"(Name)" +
				" VALUES " +
				"(@Name)";

				SqlCommand command = new SqlCommand(insertShelfSQL, connection);
				command.Parameters.Add(new SqlParameter("@Name", section.Name));
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

		public Section GetById(int id)
		{
			Section section = new Section();
			SectionRepository sectionRepository = new SectionRepository();
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string selectSectionSQL = "SELECT " +
					"Section.*" +
					" FROM Section " +
					" WHERE SectionID = @SectionID";

				SqlCommand command = new SqlCommand(selectSectionSQL, connection);
				command.Parameters.Add(new SqlParameter("@SectionID", id));

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					section.SectionID = (int)reader["SectionID"];
					section.Name = (string)reader["Name"];
				}
			}
			catch
			{
			}
			finally
			{
				connection.Dispose();
			}

			return section;
		}

		/*
		 * This method is used to update a Section in the database, using the shelf object recieved from the viewmodel
		 */

		public void UpdateSectionToDatabase(Section section)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string insertShelfSQL = "UPDATE Section SET" +
				" Name = @Name" +
				" WHERE " +
				" SectionID = @SectionID";

				SqlCommand command = new SqlCommand(insertShelfSQL, connection);
				command.Parameters.Add(new SqlParameter("@Name", section.Name));
				command.Parameters.Add(new SqlParameter("@SectionID", section.SectionID));
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