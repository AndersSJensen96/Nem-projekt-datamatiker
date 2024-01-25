using Nem.Controllers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nem.Views
{
    /// <summary>
    /// Interaction logic for DisplayLoginView.xaml
    /// </summary>
    public partial class DisplayLoginView : UserControl
    {
        public DisplayLoginView()
        {
            InitializeComponent();
        }
		private void LoginUserView(object sender, RoutedEventArgs e)
		{
			DisplayLoginViewModel displayLoginViewModel = this.DataContext as DisplayLoginViewModel;
			if (ActiveSession.AuthenticateUser(displayLoginViewModel.Username, displayLoginViewModel.Password))
			{
				MessageBox.Show("Du er nu logget ind", "Confirmation", MessageBoxButton.OK);

				DisplayMealPlanListViewModel displayMealPlanListViewModel = new DisplayMealPlanListViewModel();
				Application.Current.MainWindow.DataContext = displayMealPlanListViewModel;
			}
			else
			{
				MessageBox.Show("Der er sket en fejl, prøv venligst igen", "Confirmation", MessageBoxButton.OK);
			}
		}

		private void CreateUserView(object sender, RoutedEventArgs e)
		{
			AddUserViewModel addUserViewModel = new AddUserViewModel();
			Application.Current.MainWindow.DataContext = addUserViewModel;
		}
	}
}


