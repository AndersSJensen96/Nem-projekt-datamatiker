using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    public class RemoveProductToRecipeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddRecipeViewModel addRecipeViewModel = (AddRecipeViewModel)parameter;
            //Checks if the object exists in the collection and removes if it doesn't have it
            if (addRecipeViewModel.ChosenProductQuantitys.Contains(addRecipeViewModel.SelectedProductQuantity) && addRecipeViewModel.SelectedProductQuantity != null)
            {
                addRecipeViewModel.ChosenProductQuantitys.Remove(addRecipeViewModel.SelectedProductQuantity);
            }

         
        }
    }
}
