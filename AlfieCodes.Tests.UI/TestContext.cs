namespace AlfieCodes.Tests.UI
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    [ SetUpFixture ]
    public class TestContext
    {
        public static IWebDriver Driver { get; set; }
        public static WebDriverWait Wait { get; set; }

        public TestContext()
        {
            Driver = new ChromeDriver( Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\" ));
        }

        [OneTimeSetUp]
        public void Setup()
        {
        }

    }
}
