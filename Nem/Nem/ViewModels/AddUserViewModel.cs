using Nem.Controllers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nem.ViewModels
{
    class AddUserViewModel
    {
        #region Properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Zipcode { get; set; }
        #endregion Properties
        #region Command
        public ICommand AddUser { get; } = new AddUserCommand();
        #endregion Command
    }
}
