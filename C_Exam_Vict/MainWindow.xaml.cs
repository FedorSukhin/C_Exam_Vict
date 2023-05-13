using System;
using System.Collections.Generic;
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
using C_Exam_Vict.User;
using C_Exam_Vict.Windows;


namespace C_Exam_Vict
{
    public interface IMainWindowsCodeBehind //свой интерфейс для отображения конкретного окна
    {
        /// <summary>
        /// Показ сообщения для пользователя
        /// </summary>
        /// <param name="message">текст сообщения</param>
       void ShowMessage(string message);

        /// <summary>
        /// Загрузка нужной View
        /// </summary>
        /// <param name="view">экземпляр UserControl</param>
        void LoadView(ViewType typeView);
    }

    /// <summary>
    /// Типы вьюшек для загрузки
    /// </summary>
    public enum ViewType
    {
        Authorization,
        Registrarion,
        MainMenu
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindowsCodeBehind
    {
        UserMeneger userMeneger;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка вьюмодел для кнопок меню
            UserMeneger userMeneger = new UserMeneger();
            //даем доступ к этому кодбихайнд
            userMeneger.CodeBehind = this;
            //делаем эту вьюмодел контекстом данных
            this.DataContext = userMeneger;

            //загрузка стартовой View
            LoadView(ViewType.Registrarion);
        }

        public void LoadView(ViewType typeView)
        {

            switch (typeView)
            {
                case ViewType.Authorization:
                    //загружаем вьюшку, ее вьюмодель
                    AuthorizationView viewAuth = new AuthorizationView();
                    AuthorizationVM vmAuth = new AuthorizationVM(this);
                    //связываем их м/собой
                    viewAuth.DataContext = vmAuth;
                    //отображаем
                    this.OutputView.Content = viewAuth;
                    break;
                case ViewType.Registrarion:
                    RegistrationView viewReg = new RegistrationView();
                    RegistrationVM vmReg = new RegistrationVM(this);
                    viewReg.DataContext = vmReg;
                    this.OutputView.Content = viewReg;
                    break;
                //case ViewType.MainMenu:
                //    SecondUC viewS = new SecondUC();
                //    SecondViewModel vmS = new SecondViewModel(this);
                //    viewS.DataContext = vmS;
                //    this.OutputView.Content = viewS;
                //    break;
            }
        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
