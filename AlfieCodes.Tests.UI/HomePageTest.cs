using NUnit.Framework;

namespace AlfieCodes.Tests.UI
{
    using OpenQA.Selenium;

    [TestFixture]
    public class Tests : TestContext
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Category("HomePage")]
        public void Customer_Can_Go_To_Home_Page()
        {
            var url = "https://tt-test-frontend.azurewebsites.net";

            Driver.Navigate().GoToUrl( url );
            var signInButton = Driver.FindElement( By.PartialLinkText( "SIGN IN" ) );
            signInButton.Click();

            var email = Driver.FindElement( By.Id( "Email" ) );
            var password = Driver.FindElement( By.Id( "Password" ) );
            var button = Driver.FindElement( By.Id( "signin" ) );

            email.Click();
            email.SendKeys( "test+2@razor.co.uk" );

            password.Click();
            password.SendKeys( "TreeQueenFire1" );

            button.Click();

            Assert.IsTrue( Driver.FindElement( By.ClassName("userName") ).Text == "Hi Andrew" );

        }
    }
}