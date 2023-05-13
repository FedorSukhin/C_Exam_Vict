using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Windows;

namespace C_Exam_Vict.User
{
    internal class RegistrationVM : UserMeneger, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public RegistrationVM(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        //Properties

        ///// <summary>
        ///// Введенная строка в TextBox
        ///// </summary>
        //private string _InputText;
        //public string InputText
        //{
        //    get { return _InputText; }
        //    set
        //    {
        //        _InputText = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputText)));
        //    }
        //}


        //Commands

        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        private Command _ShowMessageCommand;
        public Command ShowMessageCommand
        {
            get
            {
                return _ShowMessageCommand = _ShowMessageCommand ??
                  new Command(OnShowMessage, CanShowMessage);
            }
        }
        private bool CanShowMessage()
        {
            return true;
        }

        private void OnShowMessage()
        {
            //из интерфейса IMainWindowsCodeBehindё
            // _MainCodeBehind.ShowMessage($"Вы ввели: {InputText}");
        }
        //public IMainWindowsCodeBehind CodeBehind { get; set; }


        ///// <summary>
        ///// Переход к AuthrizationView
        ///// </summary>
        //private Command _LoadAuthorizationCommand;
        //public Command LoadAuthorizationCommand
        //{
        //    get
        //    {
        //        return _LoadAuthorizationCommand = _LoadAuthorizationCommand ??
        //          new Command(OnLoadAuthorization, CanLoadAuthorization);
        //    }
        //}
        ////public void LoadView(ViewType typeView)
        ////{
           
        ////            //загружаем вьюшку, ее вьюмодель
        ////            AuthorizationView viewAuth = new AuthorizationView();
        ////            AuthorizationVM vmAuth = new AuthorizationVM(this);
        ////            //связываем их м/собой
        ////            viewAuth.DataContext = vmAuth;
        ////            //отображаем
        ////            this.OutputView.Content = viewAuth;
            
        ////}
        //private bool CanLoadAuthorization()
        //{
        //    return true;
        //}
        //private void OnLoadAuthorization()
        //{
        //    CodeBehind.LoadView(ViewType.Authorization);
        //}
    }
}
