using BitlyAPI;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using URLShortnerMVC.Models;

namespace URLShortnerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        static async Task<string> shortIt(string longURL)
        {
            var bitly = new Bitly("e61f727aec6760256e23dab252b5ea13f7087a48");
            var linkResponse = await bitly.PostShorten(longURL);
            var newLink = linkResponse.Link;
            return newLink.ToString();
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        bool flag = false;
        //public string shortF = null;
        
        public IActionResult Index()
        {
            ViewBag.flagvalue = flag;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormCollection frm)
        {
            flag= true;
            ViewBag.flagvalue = flag;
            string result = await shortIt(frm["longurl"].ToString());
            ViewBag.shortURL = result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}