using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QA.Models;

namespace QA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionsProvider questionsProvider;

        public HomeController(IQuestionsProvider questionsProvider)
        {
            this.questionsProvider = questionsProvider;
        }

        public ActionResult Index()
        {
            IEnumerable<Question> questions = questionsProvider.GetTopQuestions(5);
            return View(questions);
        }

        public ActionResult AskQuestion()
        {
            if(User.Identity.IsAuthenticated)
                return View(new Question());
            return View("Index");

        }

        [HttpPost]
        public ActionResult SaveQuestion(Question question)
        {
            question.UserId = User.Identity.GetUserId();
            questionsProvider.SaveQuestion(question);

            return RedirectToAction("AnswerTheQuestion");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AnswerTheQuestion()
        {
            Question q = questionsProvider.GetQuestion();
            Answer answer = new Answer();
            QuestionWithAnswer qa = new QuestionWithAnswer(q, answer);
            return View(qa);
        }

        [HttpPost]
        public ActionResult SaveAnswer(QuestionWithAnswer qa)
        {
            Answer answer = qa.A;
            answer.QuestionId = qa.Q.Id;
            answer.UserId = User.Identity.GetUserId();
            questionsProvider.SaveAnswer(answer);

            return View("Index", questionsProvider.GetTopQuestions(5));
        }
    }
}