using Nem.Controllers.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Nem.Models;
using Nem.Controllers;
using System.ComponentModel;
using System.Collections.Generic;

namespace Nem.ViewModels
{
	public class DisplayRecipeListViewModel : INotifyPropertyChanged
	{
		#region Fields
		private ObservableCollection<Recipe> filteredRecipies;
		#region Objects

		private RecipeRepository recipeRepository = new RecipeRepository();
		private FoodCategoryRepository foodCategoryRepository = new FoodCategoryRepository();
		private MealtimeRepository mealtimeRepository = new MealtimeRepository();
		#endregion Objects

		#endregion Fields

		#region Properties
		public ObservableCollection<Mealtime> Mealtimes { get; set; }
		public ObservableCollection<FoodCategory> FoodCategories { get; set; }
		public ObservableCollection<Recipe> Recipes { get; set; }
		public ObservableCollection<Recipe> FilteredRecipies
		{
			get { return filteredRecipies; }
			set { filteredRecipies = value; OnPropertyChanged("FilteredRecipies"); }
		}
		public FoodCategory SelectedFoodCategory { get; set; }
		public Mealtime SelectedMealtime { get; set; }
		public Recipe SelectedRecipe { get; set; }
		public string SelectedSearchValue { get; set; }
		public string SearchBoxInput { get; set; } = "";
		public bool Organic { get; set; } = true;

		public bool Animal { get; set; } = true;

		public bool Meat { get; set; } = true;
		public bool Other { get; set; } = true;
		#endregion Properties

		#region ICommands

		public ICommand SortCommand { get; } = new SortRecipeCommand();
		

		#endregion ICommands

		#region Constructor

		public DisplayRecipeListViewModel()
		{
			List<Mealtime> mealtimes = mealtimeRepository.GetAll();
			mealtimes.Insert(0, new Mealtime { Name = "Alle måltidsgrupper"});

			List<FoodCategory> foodcategories = foodCategoryRepository.GetAll();

			foodcategories.Insert(0, new FoodCategory { Name = "Alle madkategorier"});

			Mealtimes = new ObservableCollection<Mealtime>(mealtimes);
			FoodCategories = new ObservableCollection<FoodCategory>(foodcategories);
			Recipes = new ObservableCollection<Recipe>(recipeRepository.GetAll());
			FilteredRecipies = new ObservableCollection<Recipe>(recipeRepository.GetAll());
		}

		#endregion Constructor

		public event PropertyChangedEventHandler PropertyChanged;


		#region Methods
		private void OnPropertyChanged(string info)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion
	}
}