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

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("incognito");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.labirint.ru/");
        }

        [Test]
        public void Locators()
        {
            var cookiePolicyAgree = By.ClassName("cookie-policy__button");
            var booksMenu = By.CssSelector("[data-event-content='Книги']");
            var allBooks = By.CssSelector("li[class='b-menu-second-item']>a[href='/books/']");
            var addBookInCart = By.CssSelector(".products-row[data-title='Все в жанре «Книги»'] a[data-position='1']");
            var issueOrder = By.CssSelector(".products-row[data-title='Все в жанре «Книги»'] a[data-position='1']");
            var beginOrder = By.Id("basket-default-begin-order");
            var chooseCourierDelivery = By.CssSelector("label[data-delivery-type-name=courier]>.b-radio-e-bg");
            var city = By.CssSelector(".b-form-input[data-suggeststype=district]");
            var street = By.CssSelector(".b-form-input[data-suggeststype=streets]");
            var building  = By.CssSelector(".b-form-input[name^=building]");
            var flat  = By.CssSelector(".b-form-input[name^=flat]");
            var cityError = By.CssSelector(".b-form-e-row-m-district > span[id^=formvalidate-label");
            
            var suggestedCity  = By.CssSelector("a[id^=suggest]");
            var confirm  = By.CssSelector("input[value=Готово]");
            var courierDeliveryLightbox  = By.ClassName("js-dlform-wrap");
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}