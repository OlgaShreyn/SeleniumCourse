using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SelenClassLibrary1.pages
{
    public class BasketPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public BasketPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        
        private By chooseCourierDelivery = By.CssSelector("[data-delivery-type-name=courier] .b-radio-e-bg");
        
        public void ChooseCourierDelivery()
        {
            driver.FindElement(chooseCourierDelivery).Click();
        }
    }
}