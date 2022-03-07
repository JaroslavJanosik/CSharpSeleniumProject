using ArtinTestProject;
using IndraAvitech.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System.Windows.Forms;


namespace IndraAvitech.PageObjects
{
    class HomePage : BasePage
    {
        public HomePage(Driver driver) : base(driver)
        {
            PageFactory.InitElements(driver.Current, this);            
        }
        
        public Element CreateEmailButton => Driver.FindElement(By.XPath("//a[@data-command='compose:new']"));        
        public Element SendEmailButton => Driver.FindElement(By.XPath("(//button[@data-command='compose:send'])[2]"));        
        public Element RecipientField => Driver.FindElement(By.XPath("//input[@placeholder='Komu…']"));       
        public Element SubjectField => Driver.FindElement(By.XPath("//input[@placeholder='Předmět…']"));       
        public Element EmailBodyField => Driver.FindElement(By.XPath("//div[@placeholder='Text e-mailu…']"));        
        public Element FileUploadField => Driver.FindElement(By.XPath("//button[@title='Přidat přílohu']"));      
        public Element Attachment => Driver.FindElement(By.XPath("//a[contains(@class, 'preview') and @href]"));                
        public Element SentEmailsNav => Driver.FindElement(By.XPath("//a[@title='Odeslané']"));       
        public Element LastSentEmailName => Driver.FindElement(By.XPath("(//a[@class='name'])[1]"));       
        public Element LastSentEmailSubject => Driver.FindElement(By.XPath("(//a[@class='subject'])[1]"));
        public Element Notification => Driver.FindElement(By.XPath("//div[@class='notification']"));
        public Element UsersButton => Driver.FindElement(By.Id("user"));
        public Element LogOutButton => Driver.FindElement(By.XPath("//div/a[contains(@href, 'logout')]"));
        public Element LogInSection => Driver.FindElement(By.Id("login"));

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
            SendKeys.SendWait(@"{Enter}");            
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
            UsersButton.Click();
            LogOutButton.Click();
            Driver.Wait.Until(ExpectedConditions.ElementIsVisible(LogInSection.FoundBy));            
        }
    }  
}
