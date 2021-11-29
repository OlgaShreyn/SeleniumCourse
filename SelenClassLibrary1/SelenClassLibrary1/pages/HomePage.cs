using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SelenClassLibrary1.pages
{
    public class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public HomePage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        private string url = "https://www.labirint.ru/";
        private By cookiePolicyAgree = By.ClassName("cookie-policy__button");
        private By booksMenu = By.CssSelector("[data-event-content='Книги']");
        private By allBooks = By.CssSelector("[class='b-menu-second-item'] [href='/books/']");
        private By addBookInCart = By.XPath("(//a[contains(@id, 'buy')])[1]");
        private By issueOrder = By.XPath("(//a[contains(@id, 'buy') and text()='ОФОРМИТЬ'])[1]");
        private By beginOrder = By.Id("basket-default-begin-order");
        
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(cookiePolicyAgree).Click();
        }
        
        public void AddBookInCard()
        {
            new Actions(driver)
                .MoveToElement(driver.FindElement(booksMenu))
                .Click(driver.FindElement(allBooks))
                .Build()
                .Perform();
            Assert.AreEqual("https://www.labirint.ru/books/", driver.Url, "Перешли на неверную страницу");
            driver.FindElement(addBookInCart).Click();
            driver.FindElement(issueOrder).Click();
            driver.FindElement(beginOrder).Click();
        }
    }
}