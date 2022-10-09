using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V104.Network;
using System.Collections;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System.Reflection;
using System.Diagnostics;

namespace VeeamCareers.VeeamBase
{

    /**
     * The class has Test suite configurations. One time set up, and set ups for each tests, and reporting methods.
  */
    public class TestReportBase
    {
        public IWebDriver driver;
        public ExtentReports eReport;
        public ExtentTest testReport;


        [OneTimeSetUp]
        /**
         * Sets the driver initially before the entire test run starts. Have also included scripts to decline cookies if required.
         */
        public void setUpDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
            driver.Url = ("https://cz.careers.veeam.com/vacancies");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            try
            {
                driver.FindElement(By.Id("cookiescript_reject")).Click();
            }
            catch (NoSuchElementException)
            {
                // do nothing
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


        }

        private String GetReportPath()
        {
            return Directory.GetParent(@"../../../").FullName
                + "\\" + "Reports\\";
        }

        [OneTimeSetUp]
        public void InitializeReport()
        {
            eReport = new ExtentReports();
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(GetReportPath());
            eReport.AttachReporter(htmlReporter);


        }

        [SetUp]
        public void BeforeEachTest()
        {

            testReport = eReport.CreateTest(TestContext.CurrentContext.Test.Name);
            testReport.AssignAuthor("Vidhya Vishali KV");
            testReport.AssignCategory("Job Vacancy List Validation Test");
        }



        [TearDown]
        public void AfterEachTest()
        {

            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;

            Status logStatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logStatus = Status.Fail;
                    break;
                case TestStatus.Passed:
                    logStatus = Status.Pass;
                    break;
                case TestStatus.Skipped:
                    logStatus = Status.Skip;
                    break;
                case TestStatus.Warning:
                    logStatus = Status.Warning;
                    break;
                default:
                    logStatus = Status.Info;
                    break;

            }
            testReport.Info(TestContext.CurrentContext.Test.Name + " is Complete");
            
        }




        [OneTimeTearDown]
        public void TearDown()
        {

            eReport.Flush();
            File.Move(GetReportPath() + "//index.html", GetReportPath() + "//Report_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".html");
            driver.Quit();
        }

        public void ReportStep(String info, String status)
        {

            if (status.Equals("pass"))
                testReport.Pass(info);
            else if (status.Equals("fail"))
                testReport.Fail(info);

        }
    }
}
