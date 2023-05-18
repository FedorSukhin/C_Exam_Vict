using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Entities;

namespace C_Exam_Vict.Model
{


    internal class VictorinaModel
    {
        private string _nameVictorina;
        private int _countQuestion;
        Dictionary<Guid, QuestionModel> Questions = new Dictionary<Guid, QuestionModel>();
        private List<QuestionModel> QuestionsList = new List<QuestionModel>();
        public List<QuestionModel> QuestionsUser = new List<QuestionModel>();
        public VictorinaModel() { }
        public VictorinaModel(string nameVictorina, Dictionary<Guid, QuestionModel> questions)
        {
            NameVictorina = nameVictorina;
            Questions = questions;
            ConvertDicList();
            QuestionsUserFalse();
            _countQuestion = questions.Count;
        }

        public string NameVictorina
        {
            get { return _nameVictorina; }
            set { _nameVictorina = value; }
        }
        private void ConvertDicList()
        {
            int i = 0;
            foreach (var item in Questions)
            {
                QuestionsList[i] = item.Value;
                QuestionsUser[i] = item.Value;
                i++;
            }
        }
        private void QuestionsUserFalse()
        {
            foreach (var item in QuestionsUser)
            {
                foreach (var item1 in item.Answers)
                {
                    item1.IsCorrect = false;
                }
            }
        }
        public string[] ResultVictorina(int allowableFalse)
        {
            int CountRight = 0;
            int CountUserRight = 0;
            int CountUserQuestionsRight = 0;
            string[] Result = { "", "" };
            for (int i = 0; QuestionsUser.Count() > i; i++)
            {
                for (int j = 0; QuestionsUser[i].Answers.Count() > j; j++)
                {
                    if (QuestionsList[i].Answers[j].IsCorrect) CountRight++;
                    if (QuestionsUser[i].Answers[j].IsCorrect == QuestionsList[i].Answers[j].IsCorrect) CountUserRight++;

                }
                if (CountRight == CountUserRight)
                {
                CountUserQuestionsRight++;
                }
                CountRight = 0;
                CountUserRight = 0;
            }            
            Result[0]="Count of right answers of questions"+CountUserQuestionsRight;
            if (CountUserQuestionsRight>QuestionsList.Count- allowableFalse) 
            {
                Result[1] = "You Win!!"; 
            }
            else
            {
                Result[1] = "You Lose!!"; 
            }
            return Result;      
        }
    }
}
