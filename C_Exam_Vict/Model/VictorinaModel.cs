﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Entities;
using C_Exam_Vict.Repositories;
using C_Exam_Vict.ViewModel;

namespace C_Exam_Vict.Model
{
    public class QuestionUserResult
    {
        public bool IsAnsweredCorrectly { get; set; }
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public List<string> LoseAnswers { get; set; }
        public int QuestionNumber { get; set; }
    }
    internal class VictorinaModel
    {
        private class QuestionsUser : QuestionModel
        {
            public bool IsAnsweredCorrectly { get; set; }
            public List<int> AnswerNumbersUser { get; set; }
            public QuestionsUser(QuestionModel question)
            {
                Text = question.Text;
                Answers = question.Answers.ToList();
            }
            public override string ToString()
            {
                return base.ToString();
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
        public List<QuestionUserResult> GetAllUserQuestion()
        {
            List<QuestionUserResult> results = new List<QuestionUserResult>();
            foreach (var item in _questionsList)
            {
                QuestionUserResult temp = new QuestionUserResult();
                temp.Text = item.Text;
                temp.IsAnsweredCorrectly = item.IsAnsweredCorrectly;
            }
            return results;
        }
        public IEnumerable<QuestionUserResult> GetResults() =>
           _questionsList.Select((x, i) =>
          new QuestionUserResult
          {
              Text = x.Text,
              QuestionNumber = i + 1,
              IsAnsweredCorrectly = x.IsAnsweredCorrectly,
              Answers = x.Answers.Where(x=>x.IsCorrect).Select(x => x.Text).ToList(),
          });
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

            _questionsList[_numberQuestion].AnswerNumbersUser = answerNumbers.ToList();

            _questionsList[_numberQuestion].IsAnsweredCorrectly = corect.All(answerNumbers.Contains) && corect.Count == answerNumbers.Count;
            return _questionsList[_numberQuestion].IsAnsweredCorrectly;
        }

        public (string result, string count) ResultVictorina(int allowableFalse)  //tuple
        {
            string result = "";

            var count = _questionsList.Where(x => x.IsAnsweredCorrectly).Count();

            if (count > _questionsList.Count - allowableFalse)
            {
                result = "You Win!!";
            }
            else
            {
                result = "You Lose!!";
            }
            return (result, count.ToString());
        }
    }
}

