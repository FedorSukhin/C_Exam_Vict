using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Services;

namespace C_Exam_Vict.ViewModel
{
    abstract public class ViewModelBase//базовый класс для всех VM с привязанным интерфейсом, чтоб все View можно было объединить в Dictionary
    {
        protected IViewsManager viewsManager;
        public ViewModelBase(IViewsManager vm) 
        {
            viewsManager = vm;
        }

    }
}
