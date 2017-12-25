using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Common.Models;
using Common.Interfaces;
using System.Configuration;

namespace DAL.Providers
{
    public class QuestionsProvider: IQuestionsProvider
    {
        public Answer GetAnswer()
        {
            throw new NotImplementedException();
        }

        public Question GetQuestion()
        {
            Question question;
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                sqlConnection.Open();
                question = sqlConnection.Query<Question>("SELECT TOP 1 * FROM Questions ORDER BY NEWID()").First();
            }

            return question;
        }

        public IEnumerable<Question> GetTopQuestions(int amount)
        {
            List<Question> questions;
            List<Answer> answers;
            
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                sqlConnection.Open();

                questions = sqlConnection.Query<Question>("SELECT TOP 10 * FROM Questions ORDER BY Ranking DESC").ToList();

                for (int i = 0; i < questions.Count; i++)
                {
                    answers = sqlConnection.Query<Answer>("SELECT TOP 10 * FROM Answers WHERE QuestionId = @Id ORDER BY Ranking DESC", questions[i]).ToList();
                    questions[i].Answers = new List<Answer>(amount);
                        questions[i].Answers.AddRange(answers);
                }
            }

            /*
            Answer[] a = answers.ToArray();
            Question[] q = questions.ToArray();

            for (int i = 0; i < q.Count(); i++)
            {
                q[i].Answers = new List<Answer>();
            }
            for (int i = 0; i < a.Count(); i++)
            {
                for (int j = 0; j < q.Count(); j++)
                {
                    if (a[i].QuestionId == q[j].Id)
                    {
                        q[j].Answers.Add(a[i]);
                        break;
                    }
                }
            }
            */
            return questions;
        }

        public void SaveAnswer(Answer answer)
        {
            using (SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                sqlConnection.Open();
                sqlConnection.Query("INSERT INTO Answers(Text, UserId, QuestionId) VALUES(@Text, @UserId, @QuestionId)", answer);
            }

        }

        public void SaveQuestion(Question question)
        {
            using (SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                sqlConnection.Open();
                sqlConnection.Query("INSERT INTO Questions(Text, UserId) VALUES(@Text, @UserId)", question);
            }
        }
    }
}
