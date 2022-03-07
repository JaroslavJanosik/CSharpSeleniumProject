using IndraAvitech.PageObjects;
using IndraAvitech.Tests;
using NUnit.Framework;
using System;
using System.IO;

namespace IndraAvitech
{
    [TestFixture]
    public class SendEmailTests : BaseTest    
    {        
        HomePage _homePage;        
        private readonly string emailSubject = "Test Email " + DateTime.UtcNow.ToString("yyyymmddhhmmss");
        private readonly string fileUploadPath = Path.GetFullPath("attachment.txt");
        private readonly string emailBody = "Hi,\n\nthis is a test email.\n\nKind Regards\n\nSafetica Test";

        [SetUp]
        public void Setup()
        {            
            _homePage = new HomePage(_driver);
        }

        [Test]
        public void SendEmailTest()
        {
            _homePage.SendEmail(recipient, emailSubject, emailBody, fileUploadPath);
            _homePage.CheckThatEmailWasSent(recipient, emailSubject);
        }
    }
}
