using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    public class AddProductToRecipeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddRecipeViewModel addRecipeViewModel = (AddRecipeViewModel)parameter;
            //Checks if the object exists in the collection and adds if it doesn't have it
            ProductQuantity productQuantity = new ProductQuantity();
            productQuantity.SelectedProduct = addRecipeViewModel.SelectedProduct;
            productQuantity.ProductUnitValue = addRecipeViewModel.ProductQuantity_ProductUnitValue;
            productQuantity.UserUnit = addRecipeViewModel.ProductQuantity_UserUnit;
            productQuantity.UserUnitValue = addRecipeViewModel.ProductQuantity_UserUnitValue;

            bool ifExsist = false;
            foreach (ProductQuantity item in addRecipeViewModel.ChosenProductQuantitys)
            {
                if (item.SelectedProduct == productQuantity.SelectedProduct)
                {
                    ifExsist = true;
                }
            }

            if (!ifExsist)
            {
                addRecipeViewModel.ChosenProductQuantitys.Add(productQuantity);
            }
           


            
            

        }
    }
}
