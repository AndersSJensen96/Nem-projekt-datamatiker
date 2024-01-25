using Nem.Controllers;
using Nem.Controllers.Commands;
using Nem.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Nem.ViewModels
{
    public class DisplayProductListViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<Product> filteredProducts;
        private ProductRepository productRepository = new ProductRepository();

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Fields

        #region Properties

        public ObservableCollection<Product> FilteredProducts
        {
            get { return filteredProducts; }
            set { filteredProducts = value; OnPropertyChanged("FilteredProducts"); }
        }
        public ObservableCollection<Product> Products { get; set; }
        public Product SelectedProduct { get; set; }
        public string SearchBoxInput { get; set; } = "";



        #endregion Properties

        #region Commands

        public ICommand RemoveCommand { get; } = new RemoveProductCommand();
        public ICommand SearchForProductCommand { get; } = new SearchForProductCommand();

        #endregion Commands

        #region Constructor

        public DisplayProductListViewModel()
        {
            Products = new ObservableCollection<Product>(productRepository.GetAll());
            FilteredProducts = Products;
        }

        #endregion Constructor

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