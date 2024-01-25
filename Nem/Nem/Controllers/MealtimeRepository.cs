using Nem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Nem.Controllers
{
	public class MealtimeRepository
	{
		#region Fields

		private string connectionString = DBConnection.ConnectionString;

		#endregion Fields

		#region Methods

		/**
         * Used to select all mealtimes from the database
         */

		public List<Mealtime> GetAll()
		{
			List<Mealtime> mealtimes = new List<Mealtime>();

			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string selectAllMealtimeSQL = "SELECT * FROM Mealtime";

				SqlCommand mealtimeCommand = new SqlCommand(selectAllMealtimeSQL, connection);

				SqlDataReader mealtimeReader = mealtimeCommand.ExecuteReader();

				while (mealtimeReader.Read())
				{
					Mealtime mealtime = new Mealtime
					{
						MealtimeID = (int)mealtimeReader["MealtimeID"],
						Name = (string)mealtimeReader["Name"]
					};

					mealtimes.Add(mealtime);
				}
			}
			catch
			{
			}
			finally
			{
				connection.Dispose();
			}

			return mealtimes;
		}

		public Mealtime GetById(int id)
		{
			Mealtime mealtime = new Mealtime();
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				string selectMealtimeSQL = "SELECT * FROM Mealtime MealtimeID = @MealtimeID";

				SqlCommand command = new SqlCommand(selectMealtimeSQL, connection);
				command.Parameters.Add(new SqlParameter("@MealtimeID", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					mealtime.Name = (string)reader["Name"];
					mealtime.MealtimeID = (int)reader["MealtimeID"];
				}
			}
			catch
			{
			}
			finally
			{
				connection.Dispose();
			}

			return mealtime;
		}
		#endregion
	}
}
