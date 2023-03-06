using BacA_Exam2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace BacA_Exam2.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("whosTurn")))
            {
                return RedirectToAction("NewGame");
            }


            return View();
        }

        public IActionResult Roll()
        {
            Random rand = new Random();
            int roll = rand.Next(1, 7);
            int? total = HttpContext.Session.GetInt32("Total");
            int newTotal = (total ?? 0) + roll;

            if (roll == 1)
            {
                HttpContext.Session.SetInt32("Die", roll);
                HttpContext.Session.SetInt32("Total", 0);
                return RedirectToAction("Hold");
            }
            else
            {
                HttpContext.Session.SetInt32("Die", roll);
                HttpContext.Session.SetInt32("Total", newTotal);
            }

            return Redirect("/");
        }

        public IActionResult Hold()
        {

            return Redirect("/");
        }

        public IActionResult NewGame()
        {
            HttpContext.Session.Clear();

            //Set up the game
            HttpContext.Session.SetString("whosTurn", "Player 1");
            HttpContext.Session.SetInt32("Player1_Score", 0);
            HttpContext.Session.SetInt32("Player2_Score", 0);
            HttpContext.Session.SetInt32("Die", 0);
            HttpContext.Session.SetInt32("Total", 0);
            HttpContext.Session.SetString("Winner", "");
            HttpContext.Session.SetString("whosTurn", "Player 1");

            return Redirect("/");
        }
    }
}