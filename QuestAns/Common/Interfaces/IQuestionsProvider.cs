using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    public interface IQuestionsProvider
    {
        IEnumerable<Question> GetTopQuestions(int amount);
        Question GetQuestion();
        Answer GetAnswer();
        void SaveQuestion(Question question);
        void SaveAnswer(Answer answer);
    }
}
