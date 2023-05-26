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
using C_Exam_Vict.Model;

namespace C_Exam_Vict.ViewModel
{
    internal class MainMenuVM : ViewModelBase, INotifyPropertyChanged
    {
        private VictorinaRepos _victorinaRepos;
        public UserService _currentUser;
        public VictorinaModel _victorinaModel;
        private UserService _userService;
        public event EventHandler OnVictorinaChange;

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
            _currentUser = Singletone.GetUserService;
            _currentUser.OnUserChange += OnUserChange;
            _victorinaModel = Singletone.GetVictorina;
        }

        //Properties

        private void OnUserChange(object? param, EventArgs e)
        {
            Login = _currentUser.GetCurrentUser()?.Login??"";//!- подавление ошибок ,? строка до конца будет NULL если Get.. вернет NULL, ??"" - если выражение будет NULL то вернет пустую строку 
        }

        //выбранная тема 
        public ObservableCollection<string> Topics { get; set; }

        //заполняем список допустимых тем
        public void AddTopics()
        {
            var list = _victorinaRepos.GetTopics();
            foreach (var item in list)
            {
                Topics.Add(item);
            }
        }
        //выбираем тему
        private string _topic = "";
        public string Topic
        {
            get { return _topic; }
            set
            {
                _topic = value;
                OnPropertyChanged("Topic");
            }
        }
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
        private string _login = "";
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
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
                _victorinaModel.StartVictorina(Topic, 20);
                viewsManager.LoadView(ViewType.Question);
            }
            catch (Exception e) { ErrorMessage = e.Message; }
        }
        private ICommand _changeUser;
        public ICommand ChangeUser
        {
            get
            {
                return _changeUser = _changeUser ??
                  new Command(Change, ()=>true);
            }
        }
        private void Change()
        {
            try
            {
               viewsManager.LoadView(ViewType.Authorization);
            }
            catch (Exception e) { ErrorMessage = e.Message; }


        }
    }
}