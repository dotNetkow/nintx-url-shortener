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
        /// <summary>
        /// Main state machine for the application.
        /// 
        /// Allow user to submit URL to be shortened or 
        /// redirect user to long form URL if found.
        /// </summary>
        /// <param name="linkId"></param>
        /// <param name="longUrl"></param>
        /// <returns></returns>
        public ActionResult Index(string linkId, string longUrl)
        {
            if (!string.IsNullOrEmpty(longUrl))
            {
                if (IsValidUrl(longUrl)) { 
                    return SubmitUrl(longUrl);
                }
                else
                {
                    ViewBag.ErrorInvalidUrl = "Invalid URL! Please submit a different one.";
                    return View();
                }
            }

            if (string.IsNullOrEmpty(linkId))
            {
                return View();
            }
            else
            {
                string foundOrigUrl = UrlManager.GetUrlByEncodedId(linkId);

                if (string.IsNullOrEmpty(foundOrigUrl))
                {
                    ViewBag.ErrorInvalidShortCode = "Shortened URL not found. Please shorten another!";
                    return View();
                }

                if (!foundOrigUrl.StartsWith("http://") && !foundOrigUrl.StartsWith("https://"))
                {
                    foundOrigUrl = "http://" + foundOrigUrl;
                }

                return new RedirectResult(foundOrigUrl);
            }
        }

        /// <summary>
        /// Shorten and store a new URL from a User.
        /// </summary>
        /// <param name="longUrl">URL submitted by the user</param>
        /// <returns>UrlAdded successfully page</returns>
        private ActionResult SubmitUrl(string longUrl)
        {
            string encodedUrl = UrlManager.InsertUrl(longUrl);

            UrlModel model = new UrlModel()
            {
                ShortenedUrl = encodedUrl
            };

            return View("UrlAdded", model);
        }

        /// <summary>
        /// Determines if URL is valid.
        /// </summary>
        /// <param name="url">URL to check</param>
        /// <returns>true if URL is valid</returns>
        private bool IsValidUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
        }
    }
}
