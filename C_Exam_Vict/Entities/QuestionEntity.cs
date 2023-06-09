﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Entities
{
    internal class QuestionEntity: AnswersEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid Fk_Topic { get; set; }
        public QuestionEntity()
        {
            Id = Guid.NewGuid();
        }
        public QuestionEntity(string textQuestions, Guid fk_Topic) : this()
        {
            Text = textQuestions;
            Fk_Topic = fk_Topic;
        }
        public QuestionEntity(Guid id, string text, Guid fk_Topic)
        {
            Id = id;
            Text = text;
            Fk_Topic = fk_Topic;
        }
    }
}
