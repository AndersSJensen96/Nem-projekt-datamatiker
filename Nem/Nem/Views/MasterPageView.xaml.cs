using Nem.Controllers;
using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nem.Views
{
    /// <summary>
    /// Interaction logic for MasterPageView.xaml
    /// </summary>
    public partial class MasterPageView : Window
    {
        public MasterPageView()
        {
            InitializeComponent();
        }

		private void Menu_Open(object sender, RoutedEventArgs e)
		{
			if (Menu.Visibility == Visibility.Hidden)
			{
				Menu.Visibility = Visibility.Visible;
			}
			else
			{
				Menu.Visibility = Visibility.Hidden;
			}

		}

		private void Open_DisplayProducts(object sender, RoutedEventArgs e)
		{
			if (ActiveSession.CurrentUser is Admin)
			{
				Menu.Visibility = Visibility.Hidden;
				DataContext = new DisplayProductListViewModel();
			}
			else
			{
				MessageBox.Show("Du har ikke rettigheder til at se vare");
			}
		}
		private void Open_AddProduct(object sender, RoutedEventArgs e)
		{
			if (ActiveSession.CurrentUser is Admin)
			{
				Menu.Visibility = Visibility.Hidden;
				DataContext = new AddProductViewModel();
			}
			else
			{
				MessageBox.Show("Du har ikke rettigheder til at tilføje vare");
			}
		}

		private void Open_DisplayRecipes(object sender, RoutedEventArgs e)
		{
			Menu.Visibility = Visibility.Hidden;
			DataContext = new DisplayRecipeListViewModel();
		}

		private void Open_AddRecipe(object sender, RoutedEventArgs e)
		{
			Menu.Visibility = Visibility.Hidden;
			DataContext = new AddRecipeViewModel();
		}

		private void Open_ShoppingList(object sender, RoutedEventArgs e)
		{
			Menu.Visibility = Visibility.Hidden;
			DataContext = new DisplayShoppingListViewModel();
		}
		private void Open_DisplayLogin(object sender, RoutedEventArgs e)
		{
			Menu.Visibility = Visibility.Hidden;
			DataContext = new DisplayLoginViewModel();
		}

		private void Open_MealPlanList(object sender, RoutedEventArgs e)
		{
			Menu.Visibility = Visibility.Hidden;

			if(ActiveSession.CurrentUser != null)
			{
				DataContext = new DisplayMealPlanListViewModel();
			}
			else
			{
				MessageBox.Show("du kan ikke bruge Madplanen uden at være logget ind");
				DataContext = new DisplayLoginViewModel();
			}
			
		}


		private void Open_AddMealPlan(object sender, RoutedEventArgs e)
		{
			Menu.Visibility = Visibility.Hidden;
			if (ActiveSession.CurrentUser != null)
			{
				DataContext = new AddMealPlanViewModel();
			}
			else
			{
				MessageBox.Show("du kan ikke bruge Madplanen uden at være logget ind");
				DataContext = new DisplayLoginViewModel();
			}
			
		}
		
	}
}
