using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace SendEmailProject.Framework
{
    public class BaseTest
    {
        protected TestConfiguration _testConfiguration;
        protected Driver _driver;
        protected GmailClient _gmailClient;
        protected string _recipient;

        [SetUp]
        public void SetUp()
        {
            ExtentReporting.CreateTest(TestContext.CurrentContext.Test.MethodName);
            _testConfiguration = TestConfigHelper.AddConfig("appsettings.json");
            _recipient = _testConfiguration.RecipientEmail;
            _driver = new Driver(_testConfiguration);
            _gmailClient = new GmailClient(_testConfiguration.RecipientClientID, _testConfiguration.RecipientClientSecret);
        }

        [TearDown]
        public void TearDown()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            if (testStatus == TestStatus.Failed)
            {
                var screenShot = ExtentReporting.TakeScreenshot(_driver);
                ExtentReporting.LogFail(message, screenShot);
            }

            ExtentReporting.EndReporting();
            _driver.Destroy();
        }
    }
}