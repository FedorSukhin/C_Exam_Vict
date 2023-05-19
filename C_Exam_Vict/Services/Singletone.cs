using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Model;

namespace C_Exam_Vict.Services
{
    internal class Singletone
    {
        private static UserService _userService = null;
        private static VictorinaModel _victorina= null;

        private static readonly object padlock = new object();

        Singletone()
        {
        }

        public static UserService GetUserService//возвращает UserService или создает его при отсутствии
        {
            get
            {
                lock (padlock)
                {
                    if (_userService == null)
                    {
                        _userService = new UserService();
                    }
                    return _userService;
                }
            }
        }
        public static VictorinaModel GetVictorina//возвращает UserService или создает его при отсутствии
        {
            get
            {
                lock (padlock)
                {
                    if (_victorina == null)
                    {
                        _victorina = new VictorinaModel();
                    }
                    return _victorina;
                }
            }
        }
    }
}
