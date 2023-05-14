using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Model
{
    internal class AuthorisationM : INotifyPropertyChanged
    {
        private Guid _id;
        private string _login;
        private string _password;
        //public AuthorisationM() { }
        public string LoginAuthor
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("LoginAuthor");
            }
        }
        public string PasswordAuthor
        {
            get { return _password; }
            set 
            {
            _password = value;
            }
        }
        public void CheckAuthorisation()
        {
        
        
        
        
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
//public string DogDescription
//{
//    get { return dogDescription; }
//    set
//    {
//        dogDescription = value;
//        OnPropertyChanged("DogDescription");
//    }
//}