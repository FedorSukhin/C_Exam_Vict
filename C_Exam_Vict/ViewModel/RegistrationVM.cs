using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using C_Exam_Vict.Services;
using C_Exam_Vict.Windows;

namespace C_Exam_Vict.ViewModel
{
    internal class RegistrationVM : ViewModelBase,  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

       public RegistrationVM(IViewsManager vm): base(vm)
        {           
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
        ///// Сообщение пользователю
        ///// </summary>
        //private Command _ShowMessageCommand;
        //public Command ShowMessageCommand
        //{
        //    get
        //    {
        //        return _ShowMessageCommand = _ShowMessageCommand ??
        //          new Command(OnShowMessage, CanShowMessage);
        //    }
        //}
        //private bool CanShowMessage()
        //{
        //    return true;
        //}

        //private void OnShowMessage()
        //{
        //    //из интерфейса IMainWindowsCodeBehindё
        //    // _MainCodeBehind.ShowMessage($"Вы ввели: {InputText}");
        //}
       
    }
}
