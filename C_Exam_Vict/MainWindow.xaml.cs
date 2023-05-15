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
using C_Exam_Vict.Services;
using C_Exam_Vict.Windows;


namespace C_Exam_Vict
{
    public partial class MainWindow : Window
    {
        private ViewsManager viewsManager;
        public MainWindow()
        {
            InitializeComponent();
            viewsManager = new ViewsManager(Output);
            //установка стартовой View после загрузки главного окна
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ////загрузка стартовой View
            viewsManager.LoadView(ViewType.Authorization);
        }
    }
}
