using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    class AddUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddUserViewModel addUserViewModel = (AddUserViewModel)parameter;
            UserRepository userRepository = new UserRepository();

            User costumer = new Customer
            {
                Username = addUserViewModel.Username,
                Password = addUserViewModel.Password,
                Email = addUserViewModel.Email,
                Name = addUserViewModel.Name,
                Zipcode = addUserViewModel.Zipcode
            };
            userRepository.CreateUser(costumer);
        }
    }
}
