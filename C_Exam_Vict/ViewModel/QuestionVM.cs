using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Services;
using C_Exam_Vict.Windows;

namespace C_Exam_Vict.ViewModel
{
    internal class QuestionVM : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public QuestionVM(IViewsManager vm) : base(vm)
        { }
        //Properties



        //Commands


    }
}
