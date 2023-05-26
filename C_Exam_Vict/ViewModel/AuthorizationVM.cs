using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Model;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using C_Exam_Vict.Services;

namespace C_Exam_Vict.ViewModel
{
    public class AuthorizationVM : ViewModelBase, INotifyPropertyChanged
    {
       private UserService _userService;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        //ctor
        public AuthorizationVM(IViewsManager vm) : base(vm)
        {
            _userService = Singletone.GetUserService;

        }
        //Properties

        /// <summary>
        /// Введенная строка в TextBox
        /// </summary>
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
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                    ErrorMessage = "";
                }
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

        ///// <summary>
        ///// отправка команды на проверку данных о пользователе 
        ///// </summary>
        private ICommand _checkLoginPaswordCommand;
        public ICommand CheckLoginPaswordCommand
        {
            get
            {
                return _checkLoginPaswordCommand = _checkLoginPaswordCommand ??
                  new Command(CheckLoginPasword, CanCheckLoginPasword);
            }
        }
        private bool CanCheckLoginPasword()
        {
            return _login.Length > 0;
        }

        private void CheckLoginPasword()
        {
            try 
            {
                _userService.SingIn(_login, _password);
                viewsManager.LoadView(ViewType.MainMenu);
                Login = "";
                Password = "";
                ErrorMessage = "";
            }
            catch (Exception e) { ErrorMessage = e.Message; }
        }
        /// <summary>
        /// Переход к RegistrationView
        /// </summary>
        private ICommand _LoadRegistrationCommand;
        public ICommand LoadRegistrationCommand
        {
            get
            {
                return _LoadRegistrationCommand = _LoadRegistrationCommand ??
                  new Command(OnLoadRegistration, CanLoadRegistration);
            }
        }
        private bool CanLoadRegistration()
        {
            return true;
        }
        private void OnLoadRegistration()
        {
            Login = "";
            viewsManager.LoadView(ViewType.Registration);
        }

    }
}
