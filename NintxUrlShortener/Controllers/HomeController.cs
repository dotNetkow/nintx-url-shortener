using NintxUrlShortener.Models;
using NintxUrlShortener.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NintxUrlShortener.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(string linkId, string longUrl)
        {
            if (string.IsNullOrEmpty(linkId))
            {
                return View();
            }
            else
            {
                string longU = UrlManager.GetUrlByEncodedId(linkId);

                Response.StatusCode = 302;
                Response.RedirectLocation = longU;

                return new ContentResult();
            }

        }

        //[HttpPost]
        //public ActionResult SubmitUrl(string longUrl)
        //{
        //    string encodedUrl = UrlManager.InsertUrl(longUrl);

        //    UrlModel model = new UrlModel() {
        //        ShortenedUrl = encodedUrl
        //    };
            
        //    return View("UrlAdded", model);
        //}
    }
}
