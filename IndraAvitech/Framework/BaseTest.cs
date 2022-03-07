using ArtinTestProject;
using IndraAvitech.PageObjects;
using NUnit.Framework;
using IndraAvitech.Framework;

namespace IndraAvitech.Tests
{
    public class BaseTest
    {
        TestConfiguration _testConfiguration;
        protected Driver _driver;
        LoginPage _loginPage;
        HomePage _homePage;
        protected string recipient;

        [SetUp]
        public void Open()
        {
            _testConfiguration = TestConfigHelper.AddConfig("appsettings.json");
            recipient = _testConfiguration.RecipientEmail;
            _driver = new Driver(_testConfiguration);            
            _driver.GoTo(_testConfiguration.BaseUrl);
            _loginPage = new LoginPage(_driver);
            _homePage = new HomePage(_driver);
            _loginPage.LoginToEmail(_testConfiguration.Username, _testConfiguration.Password);
        }

        [TearDown]
        public void Close()
        {
            _homePage.LogOut();
            Assert.AreEqual(_testConfiguration.BaseUrl, _driver.Url);
            _driver.Destroy();
        }
    }
}
