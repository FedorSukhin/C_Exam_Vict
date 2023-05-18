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
        public List<QuestionsOutBD> GetQuestions(string nameTopic)
        {
            List<QuestionsOutBD> list = new List<QuestionsOutBD>();
            using (var dbQuestions = new NpgsqlConnection(ConnectionString))
            {
                var quest = dbQuestions.Query("select * from (select  random() AS R, \"Questions\".\"Id\" AS \"QId\" ,\"Questions\".\"Text\" AS \"Qtext\" from \"Questions\"join \"Topics\" T on \"Questions\".\"Fk_Topic\" = T.\"Id\"where T.\"Name\"=@name order by R limit 20) AS Questions join \"Answers\" A on A.\"Fk_question\"= Questions.\"QId\";", new { name = nameTopic });
                return list;
            }


        }
    }
}