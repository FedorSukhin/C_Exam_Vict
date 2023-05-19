using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using C_Exam_Vict.Services;
using C_Exam_Vict.Windows;
using System.Runtime.CompilerServices;

namespace C_Exam_Vict.ViewModel
{
    internal class RegistrationVM : ViewModelBase, INotifyPropertyChanged
    {
        private UserService _userService;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public RegistrationVM(IViewsManager vm) : base(vm)
        {
            _userService = Singletone.GetUserService;
        }

        //Properties

        ///// <summary>
        ///// Введенная строка в TextBox
        ///// </summary>

        private string _login = "";
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
                ErrorMessage = "";
            }
        }
        private string _password = "";
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                ErrorMessage = "";
            }
        }
        private string _errormessage = "";
        public string ErrorMessage
        {
            get { return _errormessage; }
            set
            {
                _errormessage = value;
                OnPropertyChanged();
            }
        }

        //Commands
        /// <summary>
        /// Переход к AuthrizationView
        /// </summary>
        private ICommand _LoadAuthorizationCommand;
        public ICommand LoadAuthorizationCommand
        {
            get
            {
                return _LoadAuthorizationCommand = _LoadAuthorizationCommand ??
                  new Command(OnLoadAuthorization, CanLoadAuthorization);
            }
        }
        private bool CanLoadAuthorization()
        {
            return true;
        }
        private void OnLoadAuthorization()
        {
            //от наследованного класса для загрузки в 
            viewsManager.LoadView(ViewType.Authorization);
        }

        ///// <summary>
        ///// отправка команды на проверку данных о пользователе 
        ///// </summary>
        private ICommand _newLoginPaswordCommand;
        public ICommand NewLoginPaswordCommand
        {
            get
            {
                return _newLoginPaswordCommand = _newLoginPaswordCommand ??
                  new Command(CheckLoginPasword, CanCheckLoginPasword);
            }
        }
        private bool CanCheckLoginPasword()
        {
            return _login.Length > 0 /*&& _password.Length > 0*/;
        }
        private void CheckLoginPasword()
        {
            try
            {
                _userService.SingOut(Login, Password);
                //viewsManager.LoadView(ViewType.MainMenu);
            }
            catch (Exception e) { ErrorMessage = e.Message; }
        }
    }
}
