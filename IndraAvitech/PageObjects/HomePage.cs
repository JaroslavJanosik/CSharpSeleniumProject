using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using SendEmailProject.Framework;
using System.Threading;
using System.Windows.Forms;


namespace SendEmailProject.PageObjects
{
    class HomePage : BasePage
    {
        public HomePage(Driver driver) : base(driver)
        {
            PageFactory.InitElements(driver.Current, this);
        }

        private static By UsersButtonLocator => By.CssSelector("#badge");
        private static By LogOutButtonLocator => By.CssSelector("[data-dot='logout']");
        private Element CreateEmailButton => Driver.FindElement(By.XPath("//a[@data-command='compose:new']"));
        private Element SendEmailButton => Driver.FindElement(By.XPath("(//button[@data-command='compose:send'])[2]"));
        private Element RecipientField => Driver.FindElement(By.XPath("//input[@placeholder='Komu…']"));
        private Element SubjectField => Driver.FindElement(By.XPath("//input[@placeholder='Předmět…']"));
        private Element EmailBodyField => Driver.FindElement(By.XPath("//div[@placeholder='Text e-mailu…']"));
        private Element FileUploadField => Driver.FindElement(By.XPath("//button[@title='Přidat přílohu']"));
        private Element Attachment => Driver.FindElement(By.XPath("//a[contains(@class, 'preview') and @href]"));
        private Element SentEmailsNav => Driver.FindElement(By.XPath("//a[@title='Odeslané']"));
        private Element LastSentEmailName => Driver.FindElement(By.XPath("(//a[@class='name'])[1]"));
        private Element LastSentEmailSubject => Driver.FindElement(By.XPath("(//a[@class='subject'])[1]"));
        private Element Notification => Driver.FindElement(By.XPath("//div[@class='notification']"));
        private Element LoginWidget => Driver.FindElement(By.XPath("//szn-login-widget[@data-dot='login-badge']"));
        private Element LogInSection => Driver.FindElement(By.Id("login"));

        public void SendEmail(string recipient, string subject, string emailBody, string fileUploadPath)
        {
            CreateEmailButton.Click();
            RecipientField.SendKeys(recipient);
            SubjectField.SendKeys(subject);
            EmailBodyField.SendKeys(emailBody);
            FileUploadField.Click();
            Thread.Sleep(2000);
            SendKeys.SendWait(fileUploadPath);
            Thread.Sleep(2000);
            SendKeys.SendWait(@"{ENTER}");
            Driver.Wait.Until(ExpectedConditions.ElementExists(Attachment.FoundBy));
            SendEmailButton.Click();
            Driver.Wait.Until(ExpectedConditions.ElementIsVisible(Notification.FoundBy));
        }

        public void CheckThatEmailWasSent(string recipient, string subject)
        {
            SentEmailsNav.Click();
            Assert.AreEqual(LastSentEmailName.Text, recipient);
            Assert.AreEqual(LastSentEmailSubject.Text, subject);
        }

        public void LogOut()
        {
            ISearchContext shadowRoot = LoginWidget.Current.GetShadowRoot();
            Driver.FindShadowDomElement(shadowRoot, UsersButtonLocator).Click(shadowRoot);
            Driver.FindShadowDomElement(shadowRoot, LogOutButtonLocator).Click(shadowRoot);
            Driver.Wait.Until(ExpectedConditions.ElementIsVisible(LogInSection.FoundBy));
        }
    }
}
