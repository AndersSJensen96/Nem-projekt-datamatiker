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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nem.ViewModels;

namespace Nem.Views
{
	/// <summary>
	/// Interaction logic for DisplayRecipeListView.xaml
	/// </summary>
	public partial class DisplayRecipeListView : UserControl
	{
		public DisplayRecipeListView()
		{
			InitializeComponent();
		}

		private void Open_Recipe(object sender, MouseButtonEventArgs e)
		{
			DisplayRecipeListViewModel displayRecipeListViewModel = DataContext as DisplayRecipeListViewModel;
			DisplayRecipeViewModel displayRecipeViewModel = new DisplayRecipeViewModel(displayRecipeListViewModel.SelectedRecipe);
			Application.Current.MainWindow.DataContext = displayRecipeViewModel;
		}
	}
}
