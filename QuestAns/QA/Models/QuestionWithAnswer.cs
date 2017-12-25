using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA.Models
{
    public class QuestionWithAnswer
    {
        public QuestionWithAnswer()
        {

        }

        public QuestionWithAnswer(Question question, Answer answer)
        {
            this.Q = question;
            this.A = answer;
        }

        public Question Q { get; set; }
        public Answer A { get; set; }
    }
}