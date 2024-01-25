using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    class AddProductToMealPlanCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MealPlanRepository mealPlanRepository = new MealPlanRepository();
            DisplayRecipeViewModel displayRecipeViewModel = (DisplayRecipeViewModel)parameter;

            if (displayRecipeViewModel.TotalPeople > 0)
            {
                MealPlanRecipe mealPlanRecipe = new MealPlanRecipe();
                mealPlanRecipe.SelectedRecipe = displayRecipeViewModel.SelectedRecipe;
                mealPlanRecipe.MealPlan = displayRecipeViewModel.SelectedMealPlan;
                mealPlanRecipe.TotalPeople = displayRecipeViewModel.TotalPeople;
                switch (displayRecipeViewModel.WeekDay)
                {
                    case "Mandag":
                        mealPlanRecipe.Day = Weekday.Mandag;
                        break;
                    case "Tirsdag":
                        mealPlanRecipe.Day = Weekday.Tirsdag;
                        break;
                    case "Onsdag":
                        mealPlanRecipe.Day = Weekday.Onsdag;
                        break;
                    case "Torsdag":
                        mealPlanRecipe.Day = Weekday.Torsdag;
                        break;
                    case "Fridag":
                        mealPlanRecipe.Day = Weekday.Fredag;
                        break;
                    case "Lørdag":
                        mealPlanRecipe.Day = Weekday.Lørdag;
                        break;
                    case "Søndag":
                        mealPlanRecipe.Day = Weekday.Søndag;
                        break;
                    default:
                        mealPlanRecipe.Day = Weekday.Mandag;
                        break;
                }


                mealPlanRecipe.Period = displayRecipeViewModel.SelectedTimePeriod;
                if (!mealPlanRepository.CheckExitenceOfMealPlanRecipe(mealPlanRecipe))
                {
                    mealPlanRepository.AddRecipeToMealPlan(mealPlanRecipe);
                }
            }
            else
            {
                MessageBox.Show("Du kan ikke tilføje en opskrift til en madplan uden at angive antal personer den skal bruges til");
            }
        }
    }
}
