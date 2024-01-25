using Nem.Controllers;
using Nem.Models;
using System.Collections.ObjectModel;

namespace Nem.ViewModels
{
	public class DisplayMealPlanListViewModel
	{
		#region Fields

		private TimePeriodRepository timePeriodRepository = new TimePeriodRepository();
		private MealPlanRepository mealPlanRepository = new MealPlanRepository();

		#endregion Fields

		#region Properties

		public ObservableCollection<MealPlan> MealPlans { get; set; }
		public MealPlan SelectedMealPlan { get; set; }

		#endregion Properties

		#region Constructors

		public DisplayMealPlanListViewModel()
		{
			MealPlans = new ObservableCollection<MealPlan>(mealPlanRepository.GetAll());
		}

		#endregion Constructors
	}
}