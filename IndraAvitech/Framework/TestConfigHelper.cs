using Microsoft.Extensions.Configuration;
using System;

namespace SendEmailProject.Framework
{
    public static class TestConfigHelper
    {
        public static TestConfiguration AddConfig(string path)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(path).Build();

            return config.Get<TestConfiguration>();
        }
    }
}
