using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace C_Exam_Vict
{
    // команды - альтернатива ивентам и лучше использовать их - будет меньше бардака    
    internal sealed class Command : ICommand
    {
        // тут будут храниться методы для команды. Команды разные и метод для каждой из них будет свой
        private Func<bool> canExecuteMethod;
        private readonly Action executeMethod;
        // при создании команды, объект получит нужные методы

        public Command(Action executeMethod)
        {

            this.executeMethod = executeMethod;
        }
        public Command(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.canExecuteMethod = canExecuteMethod;
            this.executeMethod = executeMethod;
        }

        //реализация трех методов ICommand
        //CanExecute - может ли команда выполняться
        bool ICommand.CanExecute(object parameter)
        {
            return canExecuteMethod();
        }
        //Execute - выполнение команды
        void ICommand.Execute(object parameter)
        {
            executeMethod();
        }
        // CanExecuteChanged - вызывается при изменении условий, указывающий, может ли команда выполняться.
        //public event EventHandler CanExecuteChanged;
        //protected void OnCanExecuteChanged(EventArgs e)
        //{ // вызываем эту-же команду
        //    CanExecuteChanged?.Invoke(this, e);
        //}
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

    }

}
