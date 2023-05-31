using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Entities
{
    public class AnswersEntity
    {
        public Guid Id { get; set; }
        public Guid Fk_Question { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public AnswersEntity()
        {
            Id = Guid.NewGuid();
        }
        public AnswersEntity(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }
        public AnswersEntity(Guid id, Guid fk_Question, bool isCorrect, string text)
        {
            Id = id;
            Fk_Question = fk_Question;
            IsCorrect = isCorrect;
            Text = text;
        }
    }
}
