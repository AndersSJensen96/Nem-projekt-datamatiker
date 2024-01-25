using Nem.Controllers;
using Nem.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Nem.Views
{
    /// <summary>
    /// Interaction logic for DisplayRecipeView.xaml
    /// </summary>
    public partial class DisplayRecipeView : UserControl
    {
        public DisplayRecipeView()
        {
            InitializeComponent();
            if (ActiveSession.CurrentUser == null)
            {
                FoodPlanOptions.Visibility = Visibility.Hidden;
            }
            else
            {
                FoodPlanOptions.Visibility = Visibility.Visible;
            }
        }

        private void AddToMealPlan(object sender, System.Windows.RoutedEventArgs e)
        {
            DisplayRecipeViewModel displayRecipeViewModel = this.DataContext as DisplayRecipeViewModel;

                MessageBox.Show("Du er nu loggede ind?", "Confirmation", MessageBoxButton.OK);


        }
    }
}