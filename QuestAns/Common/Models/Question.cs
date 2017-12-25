using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public double Ranking { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
