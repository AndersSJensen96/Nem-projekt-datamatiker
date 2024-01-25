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
    /// Interaction logic for DisplayProductListView.xaml
    /// </summary>
    public partial class DisplayProductListView : UserControl
    {
		public DisplayProductListView()
        {
            InitializeComponent();
		}


        private void Varensnavn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DisplayProductViewModel displayProductViewModel = new DisplayProductViewModel();
            DisplayProductListViewModel displayProductListViewModel = this.DataContext as DisplayProductListViewModel;
            displayProductViewModel.SelectedProduct = displayProductListViewModel.SelectedProduct;
            Application.Current.MainWindow.DataContext = displayProductViewModel;
        }
    }
}
