using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.VictorinaClasses
{
    internal class User
    {
        public Guid Id;
        public string Login;
        public string Password;

        public string BirthDate;

        public List<string> LastVictorinList { get; set; }

        public User() { }
        public override string ToString()
        {
            return Login + " " + Password + " " + BirthDate.ToString();
        }
    }
}
