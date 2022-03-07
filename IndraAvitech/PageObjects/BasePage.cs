using ArtinTestProject;

namespace IndraAvitech.PageObjects
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
