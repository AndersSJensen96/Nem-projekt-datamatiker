using Nem.Controllers;
using Nem.Controllers.Commands;
using Nem.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Nem.ViewModels
{
    public class AddProductViewModel
    {
        #region Fields

        private ShelfRepository shelfRepository = new ShelfRepository();

        #endregion Fields

        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitValue { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }

        public bool Organic { get; set; }

        public bool Animal{ get; set; }

        public bool Meat { get; set; }

        public bool Spice { get; set; }

        public Shelf ProductShelf { get; set; }
        public ObservableCollection<Shelf> Shelfs { get; set; }

        #endregion Properties

        #region Commands

        public ICommand AddProduct { get; } = new AddProductCommand();

        #endregion Commands

        #region Constructor

        public AddProductViewModel()
        {
            Shelfs = new ObservableCollection<Shelf>(shelfRepository.GetAllShelfs());
        }

        #endregion Constructor
    }
}