using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Exam_Vict.Entities
{
    internal class TopicEntity
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public TopicEntity()
        {
            Id = Guid.NewGuid();
        }
        public TopicEntity(string name) : this()
        {
            Name = name;
        }
    }
}
