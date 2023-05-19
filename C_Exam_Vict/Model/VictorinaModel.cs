using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Entities;
using C_Exam_Vict.Repositories;

namespace C_Exam_Vict.Model
{
    internal class VictorinaModel 
    {
        private int _numberQuestion = 0;
        private string _nameVictorina;
        private VictorinaRepos _victorinaRepos;

        private List<QuestionModel> _questionsList = new List<QuestionModel>();
        public List<QuestionModel> QuestionsUser = new List<QuestionModel>();
        public event EventHandler OnVictorinaStart;
        


        public VictorinaModel()
        {
            _victorinaRepos = new VictorinaRepos();
        }
        private void GetQuestions(string nameTopic, int countQuestions)
        {
            _questionsList = _victorinaRepos.GetQuestions(nameTopic, countQuestions);
            QuestionsUser = _questionsList.ToList();
            QuestionsUserFalse();            
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
        public void StartVictorina(string topic, int countQuestions)
        {
            _numberQuestion = 0;
            GetQuestions(topic, countQuestions);
            OnVictorinaStart(null,null);
        }
        public QuestionModel GetCurrentQuestion()=> QuestionsUser[_numberQuestion];
        public QuestionModel GetNextQuestion()=> QuestionsUser[++_numberQuestion];
        public int GetCurrentNumberQuestion() => _numberQuestion;
        

        public void StopVictorina() 
        {
        
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
                    if (_questionsList[i].Answers[j].IsCorrect) CountRight++;
                    if (QuestionsUser[i].Answers[j].IsCorrect == _questionsList[i].Answers[j].IsCorrect) CountUserRight++;

                }
                if (CountRight == CountUserRight)
                {
                    CountUserQuestionsRight++;
                }
                CountRight = 0;
                CountUserRight = 0;
            }
            Result[0] = "Count of right answers of questions" + CountUserQuestionsRight;
            if (CountUserQuestionsRight > _questionsList.Count - allowableFalse)
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
