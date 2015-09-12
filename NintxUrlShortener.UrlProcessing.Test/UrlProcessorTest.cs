using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NintxUrlShortener.UrlProcessing.Test
{
    [TestClass]
    public class UrlProcessorTest
    {
        [TestMethod]
        public void Encode_Normal_100()
        {
            Assert.AreEqual("bM", UrlProcessor.Encode(100));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encode_NegativeNumber()
        {
            string result = UrlProcessor.Encode(-5);
        }

        [TestMethod]
        public void Encode_Normal_521431()
        {
            Assert.AreEqual("clOl", UrlProcessor.Encode(521431));
        }

        [TestMethod]
        public void Decode_Normal_100()
        {
            Assert.AreEqual(100, UrlProcessor.Decode("bM"));
        }

        [TestMethod]
        public void Decode_Normal_521431()
        {
            Assert.AreEqual(521431, UrlProcessor.Decode("clOl"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decode_Empty()
        {
            UrlProcessor.Decode("");
        }
    }
}
