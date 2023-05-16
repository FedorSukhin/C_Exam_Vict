using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace C_Exam_Vict.Entities
{
    internal class JsonConverterDb
    {
        public class Answers
        {
            public string answer_a { get; set; }
            public string answer_b { get; set; }
            public string answer_c { get; set; }
            public string answer_d { get; set; }
            public string answer_e { get; set; }
            public string answer_f { get; set; }
        }

        //Класс в который принимаются правильный ответ или нет из json
        public class Correct_answers
        {
            public string answer_a_correct { get; set; }
            public string answer_b_correct { get; set; }
            public string answer_c_correct { get; set; }
            public string answer_d_correct { get; set; }
            public string answer_e_correct { get; set; }
            public string answer_f_correct { get; set; }
        }

        //Класс в который принимаются название темы из json
        public class Tags
        {
            public string name { get; set; }
        }

        //Класс в который принимаются все необходимые значения из json
        public class Test
        {
            [JsonPropertyName("answers")]
            public Answers answers { get; set; }
            [JsonPropertyName("correct_answers")]
            public Correct_answers correct_answers { get; set; }
            [JsonPropertyName("tags")]
            public List<Tags> tags { get; set; }
            public string question { get; set; }
            public string multiple_correct_answers { get; set; }
        }
        internal class JsonConvertDb
        {

           // public List<Question> questions = new List<Question>();
            public List<Test> test = new List<Test>();
            public JsonConvertDb()
            {

            }
            //для сериализвции с сайта
            public async Task Convert()
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Append(new KeyValuePair<string, IEnumerable<string>>("X - Api - Key", new string[] { "ZWb2Mkj5GUMNYap0VCIasrUBpeJuc6YA9RdG6jiS" }));
                try
                {
                    var response = await client.GetAsync(@"https://quizapi.io/api/v1/questions?apiKey=ZWb2Mkj5GUMNYap0VCIasrUBpeJuc6YA9RdG6jiS&limit=100");

                    var ddd = new JsonSerializerOptions() { IncludeFields = true, MaxDepth = 6, WriteIndented = true, PropertyNameCaseInsensitive = true };
                    test = JsonSerializer.Deserialize<List<Test>>(await response.Content.ReadAsStringAsync(), ddd);
                    WriteDb();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            //для сериализации из файла
            public void Convert(int a)
            {
                string fileName = "test1.json";
                string jsonFile = File.ReadAllText(fileName);
                var ddd = new JsonSerializerOptions() { IncludeFields = true, MaxDepth = 6, WriteIndented = true, PropertyNameCaseInsensitive = true };
                test = JsonSerializer.Deserialize<List<Test>>(jsonFile, ddd);
                WriteDb();
            }
            //формирование данных и запись в базу данных
            public void WriteDb()
            {
                var connectionString = "Server = localhost; Port = 5432; Database = quiz; Uid = postgres; Pwd = myword";
                using (var dbConnection = new NpgsqlConnection(connectionString))
                {
                    try
                    {
                        foreach (var question in test)
                        {
                            var topics = new List<TopicEntity>();
                            foreach (var tag in question.tags)
                            {
                                var tagger = dbConnection.QueryFirstOrDefault<TopicEntity>("select * from \"Topics\" where \"Name\" = @name", new { name = tag.name });
                                if (tagger == null)
                                {
                                    var newTopic = new TopicEntity { Name = tag.name };
                                    topics.Add(newTopic);
                                    dbConnection.Execute("insert into \"Topics\" VALUES (@name,@id) ", new { name = tag.name, id = newTopic.Id });
                                }
                                else
                                {
                                    topics.Add((TopicEntity)tagger);
                                }
                            }
                            var quest = dbConnection.QueryFirstOrDefault<QuestionEntity>("select * from \"Questions\" where \"Text\" = @text", new { text = question.question });
                            if (quest == null)
                            {
                                var newQuestion = new QuestionEntity { Text = question.question, Fk_Topic = topics.First().Id };
                                dbConnection.Execute("insert into \"Questions\" VALUEs (@id,@text,@fk)", new { text = newQuestion.Text, id = newQuestion.Id, fk = newQuestion.Fk_Topic });
                                var newAnsA = new AnswersEntity { Fk_Question = newQuestion.Id, Text = question.answers.answer_a, IsCorrect = question.correct_answers.answer_a_correct == "true" };
                                dbConnection.Execute("insert into \"Answers\" Values (@id,@text,@fk,@isCor)", new { id = newAnsA.Id, text = newAnsA.Text, fk = newAnsA.Fk_Question, isCor = newAnsA.IsCorrect });
                                var newAnsB = new AnswersEntity { Fk_Question = newQuestion.Id, Text = question.answers.answer_b, IsCorrect = question.correct_answers.answer_b_correct == "true" };
                                dbConnection.Execute("insert into \"Answers\" Values (@id,@text,@fk,@isCor)", new { id = newAnsB.Id, text = newAnsB.Text, fk = newAnsB.Fk_Question, isCor = newAnsB.IsCorrect });
                                if (!string.IsNullOrEmpty(question.answers.answer_c))
                                {
                                    var newAnsC = new AnswersEntity { Fk_Question = newQuestion.Id, Text = question.answers.answer_c, IsCorrect = question.correct_answers.answer_c_correct == "true" };
                                    dbConnection.Execute("insert into \"Answers\" Values (@id,@text,@fk,@isCor)", new { id = newAnsC.Id, text = newAnsC.Text, fk = newAnsC.Fk_Question, isCor = newAnsC.IsCorrect });
                                }
                                if (!string.IsNullOrEmpty(question.answers.answer_d))
                                {
                                    var newAnsD = new AnswersEntity { Fk_Question = newQuestion.Id, Text = question.answers.answer_d, IsCorrect = question.correct_answers.answer_d_correct == "true" };
                                    dbConnection.Execute("insert into \"Answers\" Values (@id,@text,@fk,@isCor)", new { id = newAnsD.Id, text = newAnsD.Text, fk = newAnsD.Fk_Question, isCor = newAnsD.IsCorrect });
                                }
                                if (!string.IsNullOrEmpty(question.answers.answer_e))
                                {
                                    var newAnsE = new AnswersEntity { Fk_Question = newQuestion.Id, Text = question.answers.answer_e, IsCorrect = question.correct_answers.answer_e_correct == "true" };
                                    dbConnection.Execute("insert into \"Answers\" Values (@id,@text,@fk,@isCor)", new { id = newAnsE.Id, text = newAnsE.Text, fk = newAnsE.Fk_Question, isCor = newAnsE.IsCorrect });
                                }
                                if (!string.IsNullOrEmpty(question.answers.answer_f))
                                {
                                    var newAnsF = new AnswersEntity { Fk_Question = newQuestion.Id, Text = question.answers.answer_f, IsCorrect = question.correct_answers.answer_f_correct == "true" };
                                    dbConnection.Execute("insert into \"Answers\" Values (@id,@text,@fk,@isCor)", new { id = newAnsF.Id, text = newAnsF.Text, fk = newAnsF.Fk_Question, isCor = newAnsF.IsCorrect });
                                }
                            }
                        }
                        //    var user = dbConnection.QueryFirstOrDefault<string>("select \"Name\" from \"Topics\" where \"Name\" = @name ", new { name = test. });

                        //    if (user == null)
                        //    {
                        //        dbConnection.Execute("insert into \"Users\" VALUES (@id,@user,@pass) ", new { user = Login.Text, pass = Password.Password, id = Guid.NewGuid() });
                        //        MainWindow mainWindow = new MainWindow();
                        //        mainWindow.Show();
                        //        this.Close();
                        //    }
                        //    else RegistErrorMassege.Text = "Login already exist!";
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

                }

            }
        }
    }
}
    //пример входных данных json
    //"answers": {
    //             "answer_a": "if i = 5 then",
    //            "answer_b": "if i = 5",
    //            "answer_c": "if i == 5 then",
    //            "answer_d": "if (i == 5)",
    //            "answer_e": null,
    //            "answer_f": null
    //        },
    //        "category": "Code",
    //        "correct_answer": "answer_a",
    //        "correct_answers": {
    //    "answer_a_correct": "false",
    //            "answer_b_correct": "false",
    //            "answer_c_correct": "false",
    //            "answer_d_correct": "true",
    //            "answer_e_correct": "false",
    //            "answer_f_correct": "false"
    //        },
    //        "description": null,
    //        "difficulty": "Easy",
    //        "explanation": null,
    //        "id": 981,
    //        "multiple_correct_answers": "false",
    //        "question": "How to write an IF statement in JavaScript?",
    //        "tags": [
    //            {
    //                "name": "JavaScript"
    //            }
    //        ],
