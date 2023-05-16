using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using C_Exam_Vict.Model;
using Dapper;
using Npgsql;

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
                    Topics.Add(item.ToString());
                }
                return Topics;
            }
        }
    }
}