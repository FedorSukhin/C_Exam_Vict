using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using C_Exam_Vict.Model;
using Dapper;
using Npgsql;
using C_Exam_Vict.Entities;
using System.Windows.Controls;
using C_Exam_Vict.ViewModel;

namespace C_Exam_Vict.Repositories
{
    internal class VictorinaRepos
    {
        private const string ConnectionString = "Server = localhost; Port = 5432; Database = quiz; Uid = postgres; Pwd = myword";
        public List<string> GetTopics()
        {
            List<string> Topics = new List<string>();
            using (var dbTopiks = new NpgsqlConnection(ConnectionString))
            {
                var topiks = dbTopiks.Query("select \"Name\" from\"Topics\"");
                foreach (var item in topiks)
                {
                    Topics.Add(item.Name);
                }
                return Topics;
            }
        }
        public List<QuestionModel> GetQuestions(string nameTopic, int countQuestions)
        {
            Dictionary<Guid, QuestionModel> dict = new Dictionary<Guid, QuestionModel>();
            using (var dbQuestions = new NpgsqlConnection(ConnectionString))
            {
                var quest = dbQuestions.Query<QuestionsOutBD>(@"
                     select A.""Text"" AS ""Atext"",A.""IsCorrect"",Questions.""QId"", Questions.""Qtext""
                     from (
                            select  random() AS R, ""Questions"".""Id"" AS ""QId"", ""Questions"".""Text"" AS ""Qtext""
                            from ""Questions""
                            join ""Topics"" T on ""Questions"".""Fk_Topic"" = T.""Id""
                            where T.""Name""=@name
                            order by R
                            limit @count) AS Questions
                     join ""Answers"" A on A.""Fk_question""= Questions.""QId""", new { name = nameTopic, count = countQuestions });
                foreach (var item in quest)
                {
                    if (!dict.ContainsKey(item.QId)) { dict.Add(item.QId, new QuestionModel(item.Qtext, item.QId, item.Atext, item.IsCorrect)); }
                    else { dict[item.QId].AddAnswer(item.Atext, item.IsCorrect); }
                }
            }
            return dict.Values.ToList();
        }
    }
}