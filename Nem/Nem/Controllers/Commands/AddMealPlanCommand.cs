using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
	public class AddMealPlanCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			MealPlanRepository mealPlanRepository = new MealPlanRepository();
			AddMealPlanViewModel addMealPlanViewModel = (AddMealPlanViewModel)parameter;
			if (CheckBool(addMealPlanViewModel))
			{
				addMealPlanViewModel.CreateMealPlan();
			}
		}

		#region Methods
		private bool CheckBool(AddMealPlanViewModel ViewModel)
		{
			bool check = false;
			if (ViewModel.Title != null && ViewModel.SelectedWeek > 0 && ViewModel.SelectedYear > 0)
			{
				check = true;
			}
			return check;
		}
		#endregion
	}
}
