using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SendEmailProject.Framework;

namespace SendEmailProject.PageObjects
{
    class LoginPage : BasePage
    {
        public LoginPage(Driver driver) : base(driver)
        {
            PageFactory.InitElements(driver.Current, this);
        }

        private Element UserName => Driver.FindElement(By.Id("login-username"));
        private Element Password => Driver.FindElement(By.Id("login-password"));
        private Element SignInButton => Driver.FindElement(By.XPath("//button[@data-locale='login.submit']"));

        public void Open(string url)
        {
            Driver.GoTo(url);
        }

        public void LoginToEmail(string userName, string password)
        {

            UserName.SendKeys(userName);
            SignInButton.Click();
            Password.SendKeys(password);
            SignInButton.Click();
        }
    }
}
