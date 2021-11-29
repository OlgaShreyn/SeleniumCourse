using NUnit.Framework;
using SelenClassLibrary1.pages;

namespace SelenClassLibrary1
{
    public class Homework1:SeleniumTestBase
    {
        [Test]
        public void BasketPage_EnterInvalidCity_ErrorCity()
        {
            var homePage = new HomePage(driver, wait);
            homePage.OpenPage();
            homePage.AddBookInCard();
            
            var basketPage = new BasketPage(driver, wait);
            basketPage.ChooseCourierDelivery();
            
            var courierDeliveryLightbox = new CourierDeliveryLightbox(driver, wait);
            courierDeliveryLightbox.EnterCity("Екург", false);
            
            Assert.IsTrue(courierDeliveryLightbox.IsVisibleErrorCity());
        }

        [Test]
        public void BasketPage_FillAll_Success()
        {
            var homePage = new HomePage(driver, wait);
            homePage.OpenPage();
            homePage.AddBookInCard();
            
            var basketPage = new BasketPage(driver, wait);
            basketPage.ChooseCourierDelivery();

            var courierDeliveryLightbox = new CourierDeliveryLightbox(driver, wait);
            courierDeliveryLightbox.EnterCity("Екaтеринбург");
            courierDeliveryLightbox.AddAdress("Малопрудная ул.", "5", "600");
            courierDeliveryLightbox.SetDate();
            courierDeliveryLightbox.Confirm();
            
            Assert.IsFalse(courierDeliveryLightbox.IsVisibleLightBox());
        }
    }
}