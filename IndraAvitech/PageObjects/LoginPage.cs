using ArtinTestProject;
using IndraAvitech.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace IndraAvitech.PageObjects
{
    class LoginPage : BasePage
    {   
        public LoginPage(Driver driver) : base(driver)
        {
            PageFactory.InitElements(driver.Current, this);
        }
        
        public Element UserName => Driver.FindElement(By.Id("login-username"));
        public Element Password => Driver.FindElement(By.Id("login-password"));
        public Element SignInButton => Driver.FindElement(By.XPath("//button[@data-locale='login.submit']"));        

        public void LoginToEmail(string userName, string password)        {

            UserName.SendKeys(userName);
            Password.SendKeys(password);
            SignInButton.Click();            
        }        
    }
}
