using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System.IO;
using System.Reflection;

namespace SendEmailProject.Framework;

public class ExtentReporting
{
    private static ExtentReports _extentReports;
    private static ExtentTest _extentTest;

    private static ExtentReports StartReporting()
    {
        if (_extentReports == null)
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string actualPath = assemblyPath[..assemblyPath.LastIndexOf("bin")];
            string reportPath = Path.Combine(actualPath, "Report");

            Directory.CreateDirectory(reportPath);
            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(new ExtentSparkReporter(Path.Combine(reportPath, "ExtentReport.html")));
        }

        return _extentReports;
    }

    public static void CreateTest(string testName)
    {
        _extentTest = StartReporting().CreateTest(testName);
    }

    public static void EndReporting()
    {
        StartReporting().Flush();
    }

    public static void LogInfo(string info)
    {
        _extentTest.Info(info);
    }

    public static void LogPass(string info)
    {
        _extentTest.Pass(info);
    }

    public static void LogFail(string info, Media screenshot)
    {
        _extentTest.Fail(info, screenshot);
    }

    public static Media TakeScreenshot(Driver driver)
    {
        var screenshot = ((ITakesScreenshot)driver.Current).GetScreenshot().AsBase64EncodedString;
        return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build();
    }
}