using C_Exam_Vict.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using C_Exam_Vict.Model;
using System.Runtime.CompilerServices;

namespace C_Exam_Vict.ViewModel
{
    internal class AuthorizationVM: ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        //ctor
        public AuthorizationVM(IViewsManager vm):base(vm) 
        {

        }
        //Properties

        /// <summary>
        /// Введенная строка в TextBox
        /// </summary>
        //private string _loginAuthor;
        AuthorisationM _authorM;
        public string LoginAuthor
        {
           // get { return _InputText; }
            set
            {
                _authorM.LoginAuthor = value;
                OnPropertyChanged("LoginAuthor");
                
            }
        }


        //Commands

        ///// <summary>
        ///// отправка команды на проверку данных о пользователе 
        ///// </summary>
        private ICommand _SendLoginPaswordCommand;
        public ICommand SendLoginPaswordCommand
        {
            get
            {
                return _SendLoginPaswordCommand = _SendLoginPaswordCommand ??
                  new Command(SendLoginPasword, CanSendLoginPasword);
            }
        }
        private bool CanSendLoginPasword()

        {
            return true;
        }

        private void SendLoginPasword()
        {

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
            viewsManager.LoadView(ViewType.Registration);
            
        }

    }
}
