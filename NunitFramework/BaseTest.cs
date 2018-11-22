using AventStack.ExtentReports;
using DataDrivenFramework.DataAccessLayer;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnitFramework.MagentoPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace NunitFramework
{
    public class BaseTest : Setup
    {
        [Test]
        public void PositiveCredential()
        {
            test = extent.CreateTest("PositiveCredential");
            var data = reader.XmlToDic(xmlDir);
            driver = new ChromeDriver(@"D:\Mine\Company\Maveric\Driver");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = data["url"]; //wait for infinite time

            HomePage home = new HomePage(driver);
            home.ClickOnMyAccount();
            LoginPage login = new LoginPage(driver);
            login.EnterEmailAddress(data["username"]);
            login.EnterPassword(data["password"]);
            login.ClickOnLogin();
            MainPage main = new MainPage(driver, wait);
            String actualTitle = main.GetTitle();
            Assert.AreEqual("My Account", actualTitle);
            main.ClickOnLogOut();

            test.Log(Status.Pass, "Positive test passed!!!");
        }
        [Test]
        public void NegativeCredential()
        {
            test = extent.CreateTest("NegativeCredential");
            var data = reader.XmlToDic(xmlDir);
            driver = new ChromeDriver(@"D:\Mine\Company\Maveric\Driver");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = data["url"]; //wait for infinite time

            HomePage home = new HomePage(driver);
            home.ClickOnMyAccount();
            LoginPage login = new LoginPage(driver);
            login.EnterEmailAddress(data["username"]+"55");
            login.EnterPassword(data["password"]);
            login.ClickOnLogin();
            Assert.IsTrue(login.GetPageSource().Contains("Invalid login or password."), "Invalid Check");

            test.Log(Status.Pass, "NegativeCredential test passed!!!");
        }

    }
}
