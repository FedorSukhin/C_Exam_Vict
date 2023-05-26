using C_Exam_Vict.Model;
using C_Exam_Vict.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace C_Exam_Vict.ViewModel
{
    class ResultVM : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private VictorinaModel _victorina;
        public ResultVM(IViewsManager vm) : base(vm)
        {
            _victorina = Singletone.GetVictorina;
            BackToMainMenu = new Command(MainMenu, () => true);
            _victorina.OnVictorinaStop += SetResultCount;
        }
        private void SetResultCount(Object? obj,EventArgs e)
        {
            (ResultString,RightAnswersCount) = _victorina.ResultVictorina(3);
            QuestionCount = (_victorina.GetCurrentNumberQuestion() + 1).ToString();        
        }

        //Properties

        private string _questionCount;
        public string QuestionCount
        {
            get { return _questionCount; }
            set
            {
                _questionCount = value;
                OnPropertyChanged();
            }
        }
        private string _rightAnswersCount;
        public string RightAnswersCount
        {
            get { return _rightAnswersCount; }
            set
            {
                _rightAnswersCount = value;
                OnPropertyChanged();
            }
        }
        private string _resultString;
        public string ResultString
        {
            get { return _resultString; }
            set
            {
                _resultString = value;
                OnPropertyChanged();
            }
        }


        //Command
        public ICommand BackToMainMenu { get; init; }

        private void MainMenu()
        {
            try
            {
                viewsManager.LoadView(ViewType.MainMenu);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
    }
}
