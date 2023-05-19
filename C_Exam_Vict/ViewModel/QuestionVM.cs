using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using C_Exam_Vict.Model;
using C_Exam_Vict.Services;
using C_Exam_Vict.Windows;

namespace C_Exam_Vict.ViewModel
{
    public class AnswerView
    {
        public string Text { get; set; }
        public bool IsChecked { get; set; }

    }


    internal class QuestionVM : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private VictorinaModel _victorina;

        public QuestionVM(IViewsManager vm) : base(vm)
        {
            Answers = new ObservableCollection<AnswerView>();
            _victorina = Singletone.GetVictorina;
            _victorina.OnVictorinaStart += OnVictorinaStart;
            NextQuestionCommand = new Command(NextQuestion, ()=>true);
        }
        //Properties
        private string _numberQuestion;
        public string NumberQuestion
        {
            get => _numberQuestion;
            set
            {
                _numberQuestion = value;
                OnPropertyChanged();
            }
        }

        private string _questionText = "";
        public string QuestionText
        {
            get => _questionText;
            set
            {
                _questionText = value;
                OnPropertyChanged();
            }
        }

        private void OnVictorinaStart(Object? obj, EventArgs e)
        {
            var quest = _victorina.GetCurrentQuestion();
            ShowQuestion(quest);
        }
        public ObservableCollection<AnswerView> Answers { get; set; }

        private void ShowQuestion(QuestionModel quest)
        {
            QuestionText = quest.Text;
            NumberQuestion = $"Number question {_victorina.GetCurrentNumberQuestion() + 1}";
            Answers = new ObservableCollection<AnswerView>(quest.Answers.Select(x =>
                new AnswerView { IsChecked = false, Text = x.Text }));
            OnPropertyChanged(nameof(Answers));
        }


        //private void OnUserChange(object? param, EventArgs e)
        //{
        //    AnswersList = _currentUser.GetCurrentUser()!.Login;
        //}

        //Commands

        public ICommand NextQuestionCommand { get; init; }

        private void NextQuestion()
        {
            try
            {
                ShowQuestion(_victorina.GetNextQuestion());
                viewsManager.LoadView(ViewType.Question);
            }
            catch (Exception e) {MessageBox.Show(e.Message); }
        }

    }
}
