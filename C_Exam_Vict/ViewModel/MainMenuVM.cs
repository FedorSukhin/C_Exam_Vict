using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using C_Exam_Vict.Services;
using C_Exam_Vict.Repositories;

namespace C_Exam_Vict.ViewModel
{
    internal class MainMenuVM : ViewModelBase, INotifyPropertyChanged
    {
        private VictorinaRepos _victorinaRepos;
        public UserService _currentUser;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public MainMenuVM(IViewsManager vm) : base(vm)
        {
            Topics = new ObservableCollection<string>();
            _victorinaRepos = new VictorinaRepos();
            AddTopics();
            _currentUser = new UserService();
            _currentUser.GetCurrentUser();
        }

        //Properties

        //выбранная тема 
        public ObservableCollection<string> Topics { get; set; }

        public void AddTopics()
        {
            var list = _victorinaRepos.GetTopics();
            foreach (var item in list)
            {
                Topics.Add(item);
            }
        }

        private string _topic="";
        public string Topic
        {
            get { return _topic; }
            set
            {
                _topic = value;
                OnPropertyChanged("Topic");
            }
        }

        ///// <summary>
        ///// Список доступных тем
        ///// </summary>

        private string _errormessage = "";
        public string ErrorMessage
        {
            get { return _errormessage; }
            set
            {
                _errormessage = value;
                OnPropertyChanged();
            }
        }

        //Commands

        ///// <summary>
        ///// запускаем викторину 
        ///// </summary>

        private ICommand _startVictorinCommand;
        public ICommand StartVictorinCommand
        {
            get
            {
                return _startVictorinCommand = _startVictorinCommand ??
                  new Command(StartVictorin, CanStartViktorin);
            }
        }
        private bool CanStartViktorin()
        {
            return _topic.Length > 0;
        }

        private void StartVictorin()
        {
            try 
            {
                _victorinaRepos.GetQuestions(Topic,20);
                viewsManager.LoadView(ViewType.Question);
            }
            catch (Exception e) { ErrorMessage = e.Message; }
        }

     

      

    }
}
