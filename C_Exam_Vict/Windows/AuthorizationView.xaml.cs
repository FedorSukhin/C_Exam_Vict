using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using C_Exam_Vict;
using C_Exam_Vict.ViewModel;

namespace C_Exam_Vict.Windows
{
    /// <summary>
    /// Interaction logic for AuthorisationView.xaml
    /// </summary>
    public partial class AuthorizationView : UserControl
    {
        public AuthorizationView(AuthorizationVM vm)
        {

            InitializeComponent();
            DataContext=vm;
            ((AuthorizationVM)DataContext).PropertyChanged += OnPasswordPropertyChange;
        }
        //так делать надо, нужно передавать в БД hashcode пароля и сравнивать их
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((AuthorizationVM)DataContext).Password = Password.Password;
        }

        private void OnPasswordPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Password")
            {
                if (Password.Password != ((AuthorizationVM)DataContext).Password)
                {
                    Password.Password = ((AuthorizationVM)DataContext).Password;
                    //Debugger.Break();
                }
            }
        }
    }
}
