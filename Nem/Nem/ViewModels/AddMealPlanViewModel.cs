using Nem.Controllers;
using Nem.Controllers.Commands;
using Nem.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System;

namespace Nem.ViewModels
{
	public class AddMealPlanViewModel
	{
		#region Fields

		private RecipeRepository recipeRepository = new RecipeRepository();
		private MealPlanRepository mealPlanRepository = new MealPlanRepository();
		private TimePeriodRepository timePeriodRepository = new TimePeriodRepository();

		#endregion Fields

		#region Properties

		public string Title { get; set; }
		public int SelectedWeek { get; set; }
		public int SelectedYear { get; set; }
		//public string SelectedWeekday { get; set; }
		//public int TotalPeople { get; set; }
		//public TimePeriod SelectedTimePeriod { get; set; }
		public ObservableCollection<int> AllowedYears { get; set; }
		public ObservableCollection<int> AllowedWeeks { get; set; }
		//public Recipe SelectedRecipe { get; set; }
		//public MealPlanRecipe SelectedMealPlanRecipe { get; set; }
		#endregion Properties

		#region ObservableCollections
		//public ObservableCollection<Recipe> Recipes { get; set; }
		//public ObservableCollection<MealPlanRecipe> MealPlanRecipes { get; set; } = new ObservableCollection<MealPlanRecipe>();
		//public ObservableCollection<TimePeriod> TimePeriods { get; set; }
		#endregion

		#region Commands

		public ICommand AddMealPlan { get; } = new AddMealPlanCommand();
		//public ICommand AddRecipeToMealPlan { get; } = new AddRecipeToMealPlanCommand();
		//public ICommand RemoveRecipeFromNewMealPlan { get; } = new RemoveRecipeFromNewMealPlan();

		#endregion Commands

		#region Constructors

		public AddMealPlanViewModel()
		{
			DateTime currentDateTime = DateTime.Now;
			

			AllowedYears = new ObservableCollection<int>();
			AllowedYears.Add(currentDateTime.Year);
			AllowedYears.Add((currentDateTime.Year + 1));
			SetWeeks();
			// Recipes = new ObservableCollection<Recipe>(recipeRepository.GetAll());
			// TimePeriods = new ObservableCollection<TimePeriod>(timePeriodRepository.GetAll());
		}

		#endregion Constructors

		#region Methods
		private void SetWeeks()
		{
			AllowedWeeks = new ObservableCollection<int>();
			for (int i = 1; i <= 53; i++)
			{
				AllowedWeeks.Add(i);
			}
		}

	/*	public bool AddMealPlanRecipe()
		{
			bool ifExsist = false;
			Weekday selectedWeekday;
			switch (SelectedWeekday)
			{
				case "Mandag":
					selectedWeekday = Weekday.Mandag;
					break;

				case "Tirsdag":
					selectedWeekday = Weekday.Tirsdag;
					break;

				case "Onsdag":
					selectedWeekday = Weekday.Onsdag;
					break;

				case "Torsdag":
					selectedWeekday = Weekday.Torsdag;
					break;

				case "Fredag":
					selectedWeekday = Weekday.Fredag;
					break;

				case "Lørdag":
					selectedWeekday = Weekday.Lørdag;
					break;

				case "Søndag":
					selectedWeekday = Weekday.Søndag;
					break;

				default:
					selectedWeekday = Weekday.Mandag;
					break;
			}
			foreach (MealPlanRecipe mealPlanRecipe in MealPlanRecipes)
			{

				if (mealPlanRecipe.SelectedRecipe == SelectedRecipe &&
					mealPlanRecipe.Period == SelectedTimePeriod &&
					mealPlanRecipe.Day == selectedWeekday)
				{
					ifExsist = true;
					break;
				}
			}

			if (!ifExsist)
			{

				MealPlanRecipe mealPlanRecipe = new MealPlanRecipe
				{
					Day = selectedWeekday,
					Period = SelectedTimePeriod,
					SelectedRecipe = SelectedRecipe,
					TotalPeople = TotalPeople
				};
				MealPlanRecipes.Add(mealPlanRecipe);
				return true;
			}

			return false;
		}
		*/

		public void CreateMealPlan()
		{
			MealPlan mealPlan = new MealPlan();
			mealPlan.Title = this.Title;
			mealPlan.Owner = ActiveSession.CurrentUser;
			mealPlan.Week = this.SelectedWeek;
			mealPlan.Year = this.SelectedYear;
			mealPlanRepository.InsertMealPlanToDatabase(mealPlan);
			/*
			foreach (MealPlanRecipe mealPlanRecipe in MealPlanRecipes)
			{
				mealPlanRecipe.MealPlan = mealPlan;
				mealPlanRepository.AddRecipeToMealPlan(mealPlanRecipe);
			}
			*/
		}
		#endregion
	}
}
 