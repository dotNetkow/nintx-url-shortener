using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NintxUrlShortener;
using NintxUrlShortener.Controllers;

namespace NintxUrlShortener.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_NoParameters()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index(string.Empty, string.Empty) as ViewResult;

            Assert.AreEqual(null, result.ViewBag.ErrorInvalidUrl);
        }

        [TestMethod]
        public void Index_InvalidUrl()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index(string.Empty, "::") as ViewResult;

            Assert.AreEqual("Invalid URL! Please submit a different one.", result.ViewBag.ErrorInvalidUrl);
        }

        [TestMethod]
        public void Index_InvalidUrl_LocalPath()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index(string.Empty, "c:\test") as ViewResult;

            Assert.AreEqual("Invalid URL! Please submit a different one.", result.ViewBag.ErrorInvalidUrl);
        }

        [TestMethod]
        public void Index_ValidUrlSubmitted()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index(string.Empty, "http://www.test.com") as ViewResult;

            Assert.AreEqual("UrlAdded", result.ViewName);
        }

        [TestMethod]
        public void Index_ValidShortUrl()
        {
            HomeController controller = new HomeController();

            RedirectResult result = controller.Index("B", string.Empty) as RedirectResult;

            Assert.AreEqual("https://news.ycombinator.com/news", result.Url);
        }

        [TestMethod]
        public void Index_InvalidShortUrl()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index("BYCCX", string.Empty) as ViewResult;

            Assert.AreEqual("Shortened URL not found. Please shorten another!", result.ViewBag.ErrorInvalidShortCode);
        }
    }
}
