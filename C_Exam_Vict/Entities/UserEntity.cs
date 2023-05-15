using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Model
{
    internal class UserEntity
    {
        public Guid Id {get; set;}
        public string Login {get; set;}
        public string Password { get; set;}
        public UserEntity() { }
        public override string ToString()
        {
            return Login + " " + Password;
        }
    }
}
