using C_Exam_Vict;
using C_Exam_Vict.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Repositories
{
    internal class UserRepos
    {
        private const string ConnectionString = "Server = localhost; Port = 5432; Database = quiz; Uid = postgres; Pwd = myword";
        public UserEntity? GetUserByLogin(string login)
        {
            using var dbUser = new NpgsqlConnection(ConnectionString);
            return dbUser.QueryFirstOrDefault<UserEntity>("select*from\"Users\"where\"Login\" = @log", new { log = login });
        }
        public UserEntity? SetUserDB(string login, string password)
        {
            using var dbUser = new NpgsqlConnection(ConnectionString);
            var user = dbUser.QueryFirstOrDefault<UserEntity>("select * from \"Users\" where \"Login\"=@log", new { log = login });
            dbUser.Execute("insert into \"Users\" VALUES (@id,@user,@pass) ", new { user = login, pass = password, id = Guid.NewGuid() });
            return user;            
        }
    }
}