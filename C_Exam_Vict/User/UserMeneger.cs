using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace C_Exam_Vict.User
{
    class UserMeneger 
    {
        private User user;
        public UserMeneger() 
        {
        user = new User();
        
        }
        
        public IMainWindowsCodeBehind CodeBehind { get; set; }


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
            CodeBehind.LoadView(ViewType.Authorization);
        }


        /// <summary>
        /// Переход к RegistrationView
        /// </summary>
        private Command _LoadRegistrationCommand;
        public Command LoadRegistrationCommand
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
            CodeBehind.LoadView(ViewType.Registrarion);
        }


        /// <summary>
        /// Переход к MainMenu
        /// </summary>
        private Command _LoadMainMenuCommand;
        public Command LoadMainMenuCommand
        {
            get
            {
                return _LoadMainMenuCommand = _LoadMainMenuCommand ??
                  new Command(OnLoadMainMenu, CanLoadMainMenu);
            }
        }
        private bool CanLoadMainMenu()
        {
            return true;
        }
        private void OnLoadMainMenu()
        {
            CodeBehind.LoadView(ViewType.MainMenu);
        }
    }
}
