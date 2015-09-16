using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Converter.BL;

namespace Converter.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string Convert(string Amount)
        {
            Converter.BL.Amount _converter = new BL.Amount();
            return _converter.AmounttoWords(Amount);
        }

    }
}