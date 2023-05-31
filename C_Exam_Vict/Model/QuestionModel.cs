using C_Exam_Vict.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Model
{
    public class QuestionModel
    {
        public string Text { get; set; }
        public Guid Id { get; set; }
        public List<AnswersEntity> Answers { get; set; }
        public QuestionModel()
        {
        }
        public QuestionModel(string text, Guid id, string answer, bool isCorrect)
        {
            Text = text;
            Id = id;
            Answers = new List<AnswersEntity>();
            Answers.Add(new AnswersEntity(answer, isCorrect));
        }
        public void AddAnswer(string answer, bool isCorrect)
        {
            Answers.Add(new AnswersEntity(answer, isCorrect));
        }
        public override string ToString()
        {
            string result = "";
            foreach (var item in Answers)
            {
                if (item.IsCorrect) result += "\n" + item.Text;
            }
            return Text + result + "\n";
        }
    }
}
