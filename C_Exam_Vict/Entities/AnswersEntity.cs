using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Entities
{
    internal class AnswersEntity
    {
        public Guid Id { get; set; }
        public Guid Fk_Question { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public AnswersEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
