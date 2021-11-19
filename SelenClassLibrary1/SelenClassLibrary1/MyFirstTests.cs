using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SelenClassLibrary1
{
    public class MyFirstTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("incognito");
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); // ожидание явное - что угодно - нужно прописывать 100мс
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // неявное ожидание для появления элемента на странице (нужен чаще) 30мс
            // частотность запросов на проверку появился или нет - не поменять
        }

        [Test]
        public void FirstTest()
        {
            driver.Navigate().GoToUrl("https://ru.wikipedia.org/");
            var queryInput = driver.FindElement(By.Name("search"));
            var searchButton = driver.FindElement(By.Name("go"));
            queryInput.SendKeys("Selenium");
            searchButton.Click();

            Assert.IsTrue(driver.Title.Contains("Selenium — Википедия"), "Заголовок не совпадает");
        }
        
        [Test]
        public void Locators()
        {
            driver.Navigate().GoToUrl("https://www.labirint.ru/");
            var input = driver.FindElement(By.Id("search-field"));
            var blocksMenu = driver.FindElements(By.ClassName("b-header-b-menu-e-list-item"));
            driver.Navigate().GoToUrl("https://www.labirint.ru/guestbook");
            var form = driver.FindElement(By.Name("searchformadvanced"));
            var fT = form.Text;
            var all = driver.FindElement(By.XPath("//span[text()='Все товары']"));
            all = driver.FindElement(By.CssSelector(".sorting-items.sorting-active"));
            var aT = all.Text;
            var years = driver.FindElements(By.CssSelector("select[name=year_begin]>option:not([selected])"));
            var count = years.Count;
            var firsText = years[0].GetAttribute("innerText");
            var link = driver.FindElement(By.CssSelector("[data-event-content='Как сделать заказ']"));
            link = driver.FindElement(By.LinkText("Как сделать заказ"));
        }
        
        [Test]
        public void HomeworkLocators()
        {
            driver.Navigate().GoToUrl("https://www.labirint.ru/");
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}