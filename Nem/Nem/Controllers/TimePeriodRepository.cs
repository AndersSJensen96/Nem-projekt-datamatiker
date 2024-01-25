using Nem.Controllers.Interfaces;
using Nem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Nem.Controllers
{
	public class TimePeriodRepository : IRepositoryInterface<TimePeriod>
	{
		#region Fields

		private string connectionString = DBConnection.ConnectionString;

		#endregion Fields

		#region Methods

		public List<TimePeriod> GetAll()
		{
			SqlConnection connection = new SqlConnection(connectionString);
			List<TimePeriod> timePeriods = new List<TimePeriod>();

			try
			{
				connection.Open();

				string SelectSQL = "SELECT * FROM TimePeriod";

				SqlCommand command = new SqlCommand(SelectSQL, connection);

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					TimePeriod timePeriod = new TimePeriod();
					timePeriod.PeriodID = (int)reader["TimePeriodID"];
					timePeriod.Name = (string)reader["Name"];

					timePeriods.Add(timePeriod);
				}

			}
			catch
			{

			}
			finally
			{
				connection.Dispose();
			}

			return timePeriods;
		}

		public TimePeriod GetById(int id)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			TimePeriod timePeriod = new TimePeriod();

			try
			{
				connection.Open();

				string SelectSQL = "SELECT * FROM TimePeriod WHERE TimePeriodID = @ID";

				SqlCommand command = new SqlCommand(SelectSQL, connection);
				command.Parameters.Add(new SqlParameter("@ID", id));

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					timePeriod.PeriodID = id;
					timePeriod.Name = (string)reader["Name"];
				}

			}
			catch
			{

			}
			finally
			{
				connection.Dispose();
			}

			return timePeriod;
		}

		public void InsertMealPlanToDatabase(TimePeriod timePeriod)
		{
			throw new NotImplementedException();
		}

		public void RemoveMealPlanFromDatabase(TimePeriod timePeriod)
		{
			throw new NotImplementedException();
		}

		#endregion Methods
	}
}