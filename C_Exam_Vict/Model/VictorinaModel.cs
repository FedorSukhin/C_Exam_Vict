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
        private class QuestionsUser : QuestionModel
        {
            public bool IsAnsweredCorrectly { get; set; }
            public QuestionsUser(QuestionModel question)
            {
                Text = question.Text;
                Answers = question.Answers.ToList();
            }
        }
        private int _numberQuestion = 0;
        private string _nameVictorina;
        private VictorinaRepos _victorinaRepos;
        private List<QuestionsUser> _questionsList = new List<QuestionsUser>();

        public event EventHandler OnVictorinaStart;
        public event EventHandler OnVictorinaStop;



        public VictorinaModel()
        {
            _victorinaRepos = new VictorinaRepos();
        }
        private void GetQuestions(string nameTopic, int countQuestions)
        {
            _questionsList = _victorinaRepos.GetQuestions(nameTopic, countQuestions).Select(x => new QuestionsUser(x)).ToList();
        }

        public void StartVictorina(string topic, int countQuestions)
        {
            _numberQuestion = 0;
            GetQuestions(topic, countQuestions);
            OnVictorinaStart(this, EventArgs.Empty);
        }
        public QuestionModel GetCurrentQuestion() => _questionsList[_numberQuestion];
        public QuestionModel? GetNextQuestion()
        {
            if (_numberQuestion == _questionsList.Count - 1)
            {
                OnVictorinaStop(this, EventArgs.Empty);
                return null;
            }
            return _questionsList[++_numberQuestion];
        }
        public int GetCurrentNumberQuestion() => _numberQuestion;
        public bool CheckUserAnswer(List<int> answerNumbers)
        {
            var corect = GetCurrentQuestion().Answers.Select((x, i) =>//присвоиваем каждому ответу значение 
            {//создаем анонимный класс
                return new
                {
                    Ind = i,
                    IsCorrect = x.IsCorrect
                };
            })
            .Where(x => x.IsCorrect) //берем каждый правильный ответ
            .Select(x => x.Ind).ToList();//Создаем лист с индексами правильных ответов

            _questionsList[_numberQuestion].IsAnsweredCorrectly = corect.All(answerNumbers.Contains) && corect.Count == answerNumbers.Count;
            return _questionsList[_numberQuestion].IsAnsweredCorrectly;
        }
        public string[] ResultVictorina(int allowableFalse)
        {
            int CountRight = 0;
            int CountUserRight = 0;
            int CountUserQuestionsRight = 0;
            string[] Result = { "", "" };
            for (int i = 0; _questionsList.Count() > i; i++)
            {
                for (int j = 0; _questionsList[i].Answers.Count() > j; j++)
                {
                    if (_questionsList[i].Answers[j].IsCorrect) CountRight++;
                    if (_questionsList[i].Answers[j].IsCorrect == _questionsList[i].Answers[j].IsCorrect) CountUserRight++;

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
