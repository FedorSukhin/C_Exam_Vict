﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using C_Exam_Vict.Windows;
using System.Windows.Controls;
using C_Exam_Vict.ViewModel;

namespace C_Exam_Vict.Services
{

    public interface IViewsManager //свой интерфейс для отображения конкретного окна
    {
        /// <summary>
        /// Загрузка нужной View
        /// </summary>
        /// <param name="view">экземпляр UserControl</param>
        public void LoadView(ViewType typeView);
    }

    public enum ViewType //для простоты обозначений названий окон
    {
        Authorization,
        Registration,
        MainMenu,
        Question,
        Result
    }

    public class ViewsManager : IViewsManager //класс управления вставки конкретного окна в MainWindow
    {
        private ContentPresenter _outputView; //привязка к событию в XMAL, отвечающую за смену окон
        private class VVM// для записи пары V и VM в Dictionary
        {
            public UserControl View { get; set; }
            public ViewModelBase ViewModel { get; set; }
        }

        private Dictionary<ViewType, VVM> _views; //набор всех окон

        public ViewsManager(ContentPresenter cp)//конструктор менеджера окон, с передачей MainWindow
        {
            _outputView = cp;//принимаем сообщение о смене окна
            _views = new Dictionary<ViewType, VVM>();//создаем набор типа окна и соответствующих описаний его
            //заполняем набор значениями
            var vm= new AuthorizationVM(this);
            _views.Add(ViewType.Authorization, new VVM { View = new AuthorizationView(vm), ViewModel = vm });
            _views.Add(ViewType.Registration, new VVM { View = new RegistrationView(), ViewModel = new RegistrationVM(this) });
            _views.Add(ViewType.MainMenu, new VVM { View = new MainMenuView(), ViewModel = new MainMenuVM(this) });
            _views.Add(ViewType.Question, new VVM { View = new QuestionView(), ViewModel = new QuestionVM(this)});
            _views.Add(ViewType.Result, new VVM { View = new ResultView(), ViewModel = new ResultVM(this)});
        }
        //метод загрузки конкретного окна
        public void LoadView(ViewType typeView)
        {
            var vvm = _views[typeView];//выбираем конкретную пару 
            _outputView.Content = vvm.View;//загружаем окно в MainWindow
            vvm.View.DataContext = vvm.ViewModel;//привязываем VM К V
        }

    }
}
