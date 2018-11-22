using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DataDrivenFramework.DataAccessLayer;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitFramework
{
   public class Setup
    {
        protected  String xmlDir;
        protected String propDir;
        protected String jsonDir;
        protected String excelDir;
        String reportDir;
        string ParentDir;
        
        protected FileReader reader;
        protected ExtentReports extent;
        protected ExtentTest test;
        protected IWebDriver driver;
        String screenShotDir;
        [OneTimeSetUp]
        public void Initialization()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
             ParentDir = new Uri(actualPath).LocalPath;
            xmlDir = ParentDir + @"\Test Data\magentodata.xml";
            propDir = ParentDir + @"\Test Data\data.properties";
            jsonDir = ParentDir + @"\Test Data\data.json";
            excelDir = ParentDir + @"\Test Data\HybridDemo.xlsx";
            String currentDate = DateTime.Now.ToString().Replace('/', '-').Replace(':', '-');
            reportDir = ParentDir + @"Reports\ExtentReport"+currentDate+".html";
            screenShotDir = ParentDir + @"Reports\screenshot" + currentDate + ".png";

            reader = new FileReader();
            ExtentHtmlReporter report = new ExtentHtmlReporter(reportDir);
            extent = new ExtentReports();
            extent.AttachReporter(report);
        }

        [TearDown]
        public void GetResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed)
            {
                var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(screenShotDir);
                test.Log(Status.Fail, stackTrace + errorMessage);
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotDir));
            }
        }

        [OneTimeTearDown]
        public void Endreport()
        {
            driver.Close();
            extent.Flush();

        }
    }
}
