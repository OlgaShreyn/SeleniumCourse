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
        public void LocatorsTest()
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
        public void ActionsTest()
        {
            var myLab = By.CssSelector(".top-link-main_cabinet[data-sendto='authorize']");
            var link = By.CssSelector(".b-header-b-personal-e-list-item_cabinet .b-menu-list-title.b-header-e-border-top[data-sendto='authorize']");
            var lightbox = By.ClassName("lab-modal-content");
            
            driver.Navigate().GoToUrl("https://www.labirint.ru/");
            new Actions(driver)
                .MoveToElement(driver.FindElement(myLab))
                .Click(driver.FindElement(link))
                .Build()
                .Perform();
            
            Assert.IsTrue(driver.FindElement(lightbox).Displayed, "После клика на вход или регистрация в лабиринте не отобразился лайтбокс");
        }
        
        [Test]
        public void ActionsTest2()
        {
            var searchinAr = By.Id("btwo");
            var name = By.Name("sname");
            var keysWords = By.CssSelector("input[name='keywords']");
            var selectYearTo = By.CssSelector(".year[name='year_end']");
            var link = By.ClassName("hd");
            
            driver.Navigate().GoToUrl("https://www.labirint.ru/guestbook/");

            driver.FindElement(searchinAr).Click();
            driver.FindElement(name).SendKeys("Olja");
            var keysElement = driver.FindElement(keysWords);
            keysElement.SendKeys("math");
            keysElement.Clear();
            var yearElement = new SelectElement(driver.FindElement(selectYearTo));
            yearElement.SelectByText("2019");
            Assert.That(yearElement.SelectedOption.Text == "2019", "Год выбран неверно");

            var linkElement = driver.FindElement(link);
            Assert.IsTrue(linkElement.Displayed, "Нет сообщения");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}