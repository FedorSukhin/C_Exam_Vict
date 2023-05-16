using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Entities
{
    //select*
    //from(
    //select *, random() AS R, "Questions"."Id" AS "QId"
    //from "Questions"
    //join "Topics" T on "Questions"."Fk_Topic" = T."Id"
    //where T."Name" = 'Linux'
    //order by R
    //limit 20) AS Questions
    //join "Answers" A on A."Fk_question" = Questions."QId"
    internal class QuestionsOutBD
    {
        public int QuestionsCount { get; set; }
        public string QuestionsTopic { get; set; }
        public QuestionsOutBD() { }
    }
}
