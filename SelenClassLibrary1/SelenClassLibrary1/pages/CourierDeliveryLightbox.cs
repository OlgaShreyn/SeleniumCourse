using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SelenClassLibrary1.pages
{
    public class CourierDeliveryLightbox
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public CourierDeliveryLightbox(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        
        private By city = By.CssSelector(".b-form-input[data-suggeststype=district]");
        private By street = By.CssSelector(".b-form-input[data-suggeststype=streets]");
        private By building = By.CssSelector(".b-form-input[name^=building]");
        private By flat = By.CssSelector(".b-form-input[name^=flat]");
        private By cityError = By.CssSelector(".b-form-e-row-m-district [id^=formvalidate-label");
        private By suggestedCity = By.CssSelector("a[id^=suggest]");
        private By confirm = By.CssSelector(".responsive-children .js-dlform-wrap [value=Готово]");
        private By courierDeliveryLightbox = By.CssSelector(".responsive-children .js-dlform-wrap");
        private By loader = By.ClassName("loading-panel");

        
        public void Confirm()
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loader));
            driver.FindElement(confirm).Click();
        }

        public void SetDate()
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loader));
            (driver as IJavaScriptExecutor).ExecuteScript(
                $"$('.js-delivery-date').datepicker('setDate','{DateTime.Today.AddDays(8).ToString("dd.MM.yyyy")}')");
        }

        public void AddAdress(string streetName, string buildingName, string flatName)
        {
            driver.FindElement(street).SendKeys(streetName);
            driver.FindElement(building).SendKeys(buildingName);
            driver.FindElement(flat).SendKeys(flatName);
        }
        
        public void EnterRightCity()
        {
            var cityElement = driver.FindElement(city);
            cityElement.Clear();
            cityElement.SendKeys("Екатеринбург");
            driver.FindElement(suggestedCity).Click();
        }
        
        public void EnterCity(string cityName, bool isValidCity=true)
        {
            var cityElement = driver.FindElement(city);
            cityElement.Clear();
            cityElement.SendKeys(cityName);

            if (isValidCity)
            {
                driver.FindElement(suggestedCity).Click();
            }
            else
            {
                cityElement.SendKeys(Keys.Tab);
            }
        }

        private void EnterInvalidCity()
        {
            var cityElement = driver.FindElement(city);
            cityElement.SendKeys("lala");
            cityElement.SendKeys(Keys.Tab);
        }

        public bool IsVisibleErrorCity()
        {
            return driver.FindElement(cityError).Displayed;
        }
        
        public bool IsVisibleLightBox()
        {
            return driver.FindElement(courierDeliveryLightbox).Displayed;
        }
    }
}