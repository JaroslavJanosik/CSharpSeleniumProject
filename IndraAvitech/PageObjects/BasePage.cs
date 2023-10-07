using SendEmailProject.Framework;

namespace SendEmailProject.PageObjects
{
    class BasePage
    {
        protected Driver Driver { get; private set; }

        public BasePage(Driver driver)
        {
            Driver = driver;
        }
    }
}
