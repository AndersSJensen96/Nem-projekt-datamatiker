using Nem.Controllers.Interfaces;
using Nem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Nem.Controllers
{
    public class MealPlanRepository
    {
        #region Fields

        private string connectionString = DBConnection.ConnectionString;

        #endregion Fields

        #region Methods

        public List<MealPlan> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<MealPlan> mealPlans = new List<MealPlan>();
            try
            {
                string selectAllMealtimeSQL = "SELECT * FROM MealPlan WHERE RegisteredUser = @User";

                connection.Open();

                SqlCommand mealPlanCommand = new SqlCommand(selectAllMealtimeSQL, connection);
                mealPlanCommand.Parameters.Add(new SqlParameter("@User", ActiveSession.CurrentUser.Username));
                SqlDataReader mealPlanReader = mealPlanCommand.ExecuteReader();

                while (mealPlanReader.Read())
                {
                    MealPlan mealPlan = new MealPlan
                    {
                        MealPlanID = (int)mealPlanReader["MealPlanID"],
                        Title = (string)mealPlanReader["Title"],

                        Week = (int)mealPlanReader["Week"],
                        Year = (int)mealPlanReader["Year"],

                        Owner = ActiveSession.CurrentUser,
                    };

                    mealPlans.Add(mealPlan);
                }
            }
            catch
            {
            }
            finally
            {
                connection.Dispose();
            }
            return mealPlans;
        }

        public MealPlan GetById(MealPlan mealPlan)
        {
            RecipeRepository recipeRepository = new RecipeRepository();
            SqlConnection connection = new SqlConnection(connectionString);
            TimePeriodRepository timePeriodRepository = new TimePeriodRepository();
            
            try
            {
                string selectAllMealtimeSQL = "SELECT * FROM MealPlanRecipe WHERE MealPlan = @MealPlanID ORDER BY Weekday, TimePeriod";

                connection.Open();

                SqlCommand mealPlanCommand = new SqlCommand(selectAllMealtimeSQL, connection);
                mealPlanCommand.Parameters.Add(new SqlParameter("@MealPlanID", mealPlan.MealPlanID));
                SqlDataReader mealPlanReader = mealPlanCommand.ExecuteReader();
                while (mealPlanReader.Read())
                {
                    Recipe recipe = new Recipe();
                    MealPlanRecipe mealPlanRecipe = new MealPlanRecipe();
                    recipe.RecipeID = (int)mealPlanReader["Recipe"];
                    
                    mealPlanRecipe.MealPlan = mealPlan;
                    mealPlanRecipe.SelectedRecipe = recipeRepository.GetById(recipe); 
                    mealPlanRecipe.TotalPeople = (int)mealPlanReader["TotalPeople"];
                    int weekday = (int)mealPlanReader["Weekday"];
                    switch (weekday)
                    {
                        case 1:
                            mealPlanRecipe.Day = Weekday.Mandag;
                            break;

                        case 2:
                            mealPlanRecipe.Day = Weekday.Tirsdag;
                            break;

                        case 3:
                            mealPlanRecipe.Day = Weekday.Onsdag;
                            break;

                        case 4:
                            mealPlanRecipe.Day = Weekday.Torsdag;
                            break;

                        case 5:
                            mealPlanRecipe.Day = Weekday.Fredag;
                            break;

                        case 6:
                            mealPlanRecipe.Day = Weekday.Lørdag;
                            break;

                        case 7:
                            mealPlanRecipe.Day = Weekday.Søndag;
                            break;

                        default:
                            mealPlanRecipe.Day = Weekday.Mandag;
                            break;
                    }
                    mealPlanRecipe.Period = timePeriodRepository.GetById((int)mealPlanReader["TimePeriod"]);
                    if (mealPlan.Recipes == null)
                    {
                        mealPlan.Recipes = new ObservableCollection<MealPlanRecipe>();
                    }
                    mealPlan.Recipes.Add(mealPlanRecipe);
                }
            }
            catch
            {
            }
            finally
            {
                connection.Dispose();
            }
            return mealPlan;
        }

        public void InsertMealPlanToDatabase(MealPlan mealPlan)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string InsertSQL = "INSERT INTO MealPlan(Title, Week, Year, RegisteredUser)  OUTPUT INSERTED.MealPlanID " +
                                "VALUES (@Title, @Week, @Year, @User)";

                SqlCommand command = new SqlCommand(InsertSQL, connection);
                command.Parameters.Add(new SqlParameter("@Title", mealPlan.Title));
                command.Parameters.Add(new SqlParameter("@Week", mealPlan.Week));
                command.Parameters.Add(new SqlParameter("@Year", mealPlan.Year));
                command.Parameters.Add(new SqlParameter("@User", mealPlan.Owner.Username));

                int mealPlanId = (int)command.ExecuteScalar();
                mealPlan.MealPlanID = mealPlanId;
            }
            catch
            {

            }
            finally
            {
                connection.Dispose();
            }
            

            
        }

        public void RemoveMealPlanFromDatabase(MealPlan mealPlan)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string DeleteSQL = "DELETE FROM MealPlan WHERE MealPlanID = @MealPlanID";

                SqlCommand command = new SqlCommand(DeleteSQL, connection);
                command.Parameters.Add(new SqlParameter("@MealPlanID", mealPlan.MealPlanID));

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

        public void AddRecipeToMealPlan(MealPlanRecipe mealPlanRecipe)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                
                string AddMealPlanRecipe = "INSERT INTO MealPlanRecipe(MealPlan, Recipe, TimePeriod, Weekday, TotalPeople) " +
                                           "VALUES(@MealPlanID, @RecipeID, @TimePeriod, @Weekday,@TotalPeople)";
             
                SqlCommand command = new SqlCommand(AddMealPlanRecipe, connection);
                command.Parameters.Add(new SqlParameter("@MealPlanID", mealPlanRecipe.MealPlan.MealPlanID));
                command.Parameters.Add(new SqlParameter("@RecipeID", mealPlanRecipe.SelectedRecipe.RecipeID));
                command.Parameters.Add(new SqlParameter("@TimePeriod", mealPlanRecipe.Period.PeriodID));
                command.Parameters.Add(new SqlParameter("@Weekday", (int)mealPlanRecipe.Day));
                command.Parameters.Add(new SqlParameter("@TotalPeople", mealPlanRecipe.TotalPeople));

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

        public void RemoveRecipeFromMealPlan(MealPlanRecipe mealPlanRecipe)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                string DeleteSQL = "DELETE FROM MealPlanRecipe " +
                                   "WHERE MealPlan = @MealPlanID " +
                                   "AND Recipe = @RecipeID " +
                                   "AND TimePeriod = @TimePeriod " +
                                   "AND Weekday = @Weekday";

                SqlCommand command = new SqlCommand(DeleteSQL, connection);
                command.Parameters.Add(new SqlParameter("@MealPlanID", mealPlanRecipe.MealPlan.MealPlanID));
                command.Parameters.Add(new SqlParameter("@RecipeID", mealPlanRecipe.SelectedRecipe.RecipeID));
                command.Parameters.Add(new SqlParameter("@TimePeriod", mealPlanRecipe.Period.PeriodID));
                command.Parameters.Add(new SqlParameter("@Weekday", (int)mealPlanRecipe.Day));

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

		public bool CheckExitenceOfMealPlanRecipe(MealPlanRecipe mealPlanRecipe)
		{
			bool response = false;

            SqlConnection connection = new SqlConnection(connectionString);

            try
			{



                connection.Open();


                string SqlSelect = "SELECT * FROM MealPlanRecipe " + "WHERE MealPlan = @MealPlanID " + "AND Recipe = @RecipeID " + "AND TimePeriod = @TimePeriodID " + "AND Weekday = @Weekday";

				SqlCommand command = new SqlCommand(SqlSelect, connection);
                command.Parameters.Add(new SqlParameter("@MealPlanID", mealPlanRecipe.MealPlan.MealPlanID));
				command.Parameters.Add(new SqlParameter("@RecipeID", mealPlanRecipe.SelectedRecipe.RecipeID));
				command.Parameters.Add(new SqlParameter("@TimePeriodID", mealPlanRecipe.Period.PeriodID));
				command.Parameters.Add(new SqlParameter("@Weekday", (int)mealPlanRecipe.Day));
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    response = true;
                    break;
                }

			}
			catch
			{

			}
			finally
			{
				connection.Dispose();
			}


            return response;
		}
		#endregion Methods
	}
}