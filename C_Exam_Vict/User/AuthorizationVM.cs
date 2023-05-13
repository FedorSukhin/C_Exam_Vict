using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.User
{
    internal class AuthorizationVM: UserMeneger, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public AuthorizationVM(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        //Properties

        /// <summary>
        /// Введенная строка в TextBox
        /// </summary>
        private string _InputText;
        public string InputText
        {
            get { return _InputText; }
            set
            {
                _InputText = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputText)));
            }
        }


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
            _MainCodeBehind.ShowMessage($"Вы ввели: {InputText}");
        }

    }
}
