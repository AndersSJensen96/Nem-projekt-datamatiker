using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Nem.Controllers;
using Nem.Models;
using Nem.Controllers.Commands;
using System.Windows.Controls;

namespace Nem.ViewModels
{
	public class AddRecipeViewModel
	{
		#region Fields

		#region Repositories

		private FoodCategoryRepository foodCategoryRepository = new FoodCategoryRepository();
		private MealtimeRepository mealtimeRepository = new MealtimeRepository();
		private ProductRepository productRepository = new ProductRepository();

		#endregion Repositories

		#endregion Fields

		#region Properties

		public string Name { get; set; }
		public string Description { get; set; }
		public int Difficulty { get; set; }
		public int CookingTime { get; set; }
		public int NumberOfPersons { get; set; }
		//Tried to make an instance of User. Changed to customer instead
		public User CurrentUser { get; set; } = new Customer { Username = "kunde"};
		public DateTime CreationTime { get; set; }
		public FoodCategory SelectedFoodCategory { get; set; }
		public Mealtime SelectedMealtime { get; set; }
		public Product SelectedProduct { get; set; }
		public ProductQuantity SelectedProductQuantity { get; set; } //
		public double ProductQuantity_UserUnitValue { get; set; }
		public string ProductQuantity_UserUnit { get; set; }
		public double ProductQuantity_ProductUnitValue { get; set; }

		#region ObservableCollections
		//Observable for the choosen objects
		public ObservableCollection<FoodCategory> ChosenFoodCategories { get; set; } = new ObservableCollection<FoodCategory>();
		public ObservableCollection<Mealtime> ChosenMealtimes { get; set; } = new ObservableCollection<Mealtime>();
		public ObservableCollection<ProductQuantity> ChosenProductQuantitys { get; set; } = new ObservableCollection<ProductQuantity>(); //
		//Observable for printing list of all exiting object
		public ObservableCollection<FoodCategory> FoodCategories { get; set; }
		public ObservableCollection<Mealtime> Mealtimes { get; set; }
		public ObservableCollection<Product> Products { get; set; }


		#endregion ObservableCollections

		#endregion Properties

		#region ICommands

		public ICommand AddRecipe { get; } = new AddRecipeCommand();
		public ICommand AddFoodCategory { get; } = new AddFoodCategoryToRecipeCommand();
		public ICommand RemoveFoodCategory { get; } = new RemoveFoodCategoryToRecipeCommand();
		public ICommand AddMealtime { get; } = new AddMealtimeToRecipeCommand();
		public ICommand RemoveMealtime { get; } = new RemoveMealtimeToRecipeCommand();
		public ICommand AddProduct { get; } = new AddProductToRecipeCommand();
		public ICommand RemoveProduct { get; } = new RemoveProductToRecipeCommand();

		#endregion ICommands

		#region Constructor

		public AddRecipeViewModel()
		{
			FoodCategories = new ObservableCollection<FoodCategory>(foodCategoryRepository.GetAll());
			Mealtimes = new ObservableCollection<Mealtime>(mealtimeRepository.GetAll());
			Products = new ObservableCollection<Product>(productRepository.GetAll());
		}

		#endregion Constructor
	}
}
