using System;
using System.Diagnostics.Eventing.Reader;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SelenClassLibrary1
{
    public abstract class SeleniumTestBase
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("incognito");
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}