using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Model
{
    internal class UserModel
    {
        public Guid Id {get; set;}
        public string Login {get; set;}
        public string Password { get; set;}
        public UserModel() { }
        public override string ToString()
        {
            return Login + " " + Password;
        }
    }
}
