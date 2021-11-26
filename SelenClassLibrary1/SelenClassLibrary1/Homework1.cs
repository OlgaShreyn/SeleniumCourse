using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SelenClassLibrary1
{
    public class Homework1
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
            driver.Navigate().GoToUrl("https://www.labirint.ru/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void LabirintTest()
        {
            var cookiePolicyAgree = By.ClassName("cookie-policy__button");
            var booksMenu = By.CssSelector("[data-event-content='Книги']");
            var allBooks = By.CssSelector("[class='b-menu-second-item'] [href='/books/']");
            var addBookInCart = By.XPath("(//a[contains(@id, 'buy')])[1]");
            var issueOrder = By.XPath("(//a[contains(@id, 'buy') and text()='ОФОРМИТЬ'])[1]");
            var beginOrder = By.Id("basket-default-begin-order");
            var chooseCourierDelivery = By.CssSelector("[data-delivery-type-name=courier] .b-radio-e-bg");

            var city = By.CssSelector(".b-form-input[data-suggeststype=district]");
            var street = By.CssSelector(".b-form-input[data-suggeststype=streets]");
            var building = By.CssSelector(".b-form-input[name^=building]");
            var flat = By.CssSelector(".b-form-input[name^=flat]");
            var cityError = By.CssSelector(".b-form-e-row-m-district [id^=formvalidate-label");

            var suggestedCity = By.CssSelector("a[id^=suggest]");
            var confirm = By.CssSelector(".responsive-children .js-dlform-wrap [value=Готово]");
            var courierDeliveryLightbox = By.CssSelector(".responsive-children .js-dlform-wrap");
            var loader = By.ClassName("loading-panel-inner");

            driver.FindElement(cookiePolicyAgree).Click();
            new Actions(driver)
                .MoveToElement(driver.FindElement(booksMenu))
                .Click(driver.FindElement(allBooks))
                .Build()
                .Perform();
            Assert.That(driver.Url == "https://www.labirint.ru/books/");
            driver.FindElement(addBookInCart).Click();
            driver.FindElement(issueOrder).Click();
            driver.FindElement(beginOrder).Click();
            driver.FindElement(chooseCourierDelivery).Click();
            var cityElement = driver.FindElement(city);
            cityElement.SendKeys("lala");
            cityElement.SendKeys("\t");
            Assert.That(driver.FindElement(cityError).Displayed);
            cityElement.Clear();
            cityElement.SendKeys("Екатеринбург");
            driver.FindElement(suggestedCity).Click();
            driver.FindElement(street).SendKeys("Малопрудная ул.");
            driver.FindElement(building).SendKeys("5");
            driver.FindElement(flat).SendKeys("600");
            
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loader));
            (driver as IJavaScriptExecutor).ExecuteScript($"$('.js-delivery-date').datepicker('setDate','{DateTime.Today.AddDays(8).ToString("dd.MM.yyyy")}')");
            // ставится на 30 ноября максимум, больше не смогла - руками тоже месяц не меняется
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loader));
            //в новой версии нет, что использовать тогда?
            driver.FindElement(confirm).Click();
            Assert.That(driver.FindElement(courierDeliveryLightbox).Displayed == false);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}