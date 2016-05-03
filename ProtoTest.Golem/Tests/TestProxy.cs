﻿using NUnit.Framework;
using ProtoTest.Golem.Core;
using ProtoTest.Golem.Tests.PageObjects.Google;
using ProtoTest.Golem.WebDriver;

namespace ProtoTest.Golem.Tests
{
    internal class TestProxy : WebDriverTestBase
    {
        [TestFixtureSetUp]
        public void setup()
        {
            Config.Settings.httpProxy.startProxy = true;
            Config.Settings.httpProxy.useProxy = true;
        }

        [Test]
        public void TestProxyNotNull()
        {
            Assert.IsNotNull(proxy);
        }

        [Test]
        public void TestProxyWorks()
        {
            OpenPage<GoogleHomePage>("http://www.google.com/");
        }

        [Test]
        public void TestProxyFilter()
        {
            OpenPage<GoogleHomePage>("http://www.google.com/");
            var entries = proxy.FilterEntries("www.google.com");
            Assert.AreEqual(10, entries.Count);
            Assert.AreEqual("http://www.google.com/", entries[0].Request.Url);
        }

        [Test]
        public void TestHTTPValidation()
        {
            OpenPage<GoogleHomePage>("http://www.google.com/");
            proxy.VerifyRequestMade("http://www.google.com/");
        }

        [Test]
        public void TestResponseCodeValidation()
        {
            OpenPage<GoogleHomePage>("http://www.google.com/");
            proxy.VerifyNoErrorsCodes();
        }
    }
}