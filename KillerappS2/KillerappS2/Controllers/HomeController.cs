using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KillerappS2.Models;
using System.Data.SqlClient;
using Logic;
using Models;
using Microsoft.AspNetCore.Http;

namespace KillerappS2.Controllers
{
    public class HomeController : Controller
    {
        private Song_Logic songlogic = new Song_Logic();
        private Viewmodel viewmodel = new Viewmodel();
        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString("_Name");
            var account_id = HttpContext.Session.GetInt32("_Id");
            if(name == null && account_id == 0)
            {
                return RedirectToAction("Log_in", "Log_in");
            }
            else
            {
                return View(viewmodel);
            }
            
        }

        public IActionResult Searchpage()
        {
            var name = HttpContext.Session.GetString("_Name");
            var account_id = HttpContext.Session.GetInt32("_Id");
            if (name == null && account_id == 0)
            {
                return RedirectToAction("Log_in", "Log_in");
            }
            else
            {
                return View(viewmodel);
            }
        }
        
        [HttpPost]
        public IActionResult SearchSongs(Song song)
        {
            viewmodel.Songs.Clear();
            viewmodel.GetSongsDatabase(song);
            return RedirectToAction("Searchpage");
        }

        public IActionResult UpdateTotal_Streams(Song song)
        {
            songlogic.UpdateTotal_Streams(song);
            return RedirectToAction("Searchpage");
        }
        
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
  
    }
}
