using NUnit.Framework;
using SendEmailProject.Framework;
using SendEmailProject.PageObjects;
using System;
using System.IO;

namespace SendEmailProject.Tests
{
    [TestFixture]
    public class SendEmailTests : BaseTest
    {
        private HomePage _homePage;
        private LoginPage _loginPage;
        private readonly string emailSubject = $"Test Email {DateTime.UtcNow:yyyymmddhhmmss}";
        private readonly string fileUploadPath = Path.GetFullPath("TestData\\attachment.txt");
        private readonly string emailBody = "Hi,\n\nthis is a test email.\n\nKind Regards\n\nJaroslav";

        [SetUp]
        public void TestSetUp()
        {
            _homePage = new HomePage(_driver);
            _loginPage = new LoginPage(_driver);
            _loginPage.Open(_testConfiguration.BaseUrl);
        }

        [Test]
        public void SendEmailTest()
        {
            _loginPage.LoginToEmail(_testConfiguration.Username, _testConfiguration.Password);
            _homePage.SendEmail(_recipient, emailSubject, emailBody, fileUploadPath);
            _homePage.CheckThatEmailWasSent(_recipient, emailSubject);
            _gmailClient.CheckThatEmailWasReceived($"<{_testConfiguration.UserEmail}>", emailSubject, TimeSpan.FromSeconds(30));
            _homePage.LogOut();
        }
    }
}