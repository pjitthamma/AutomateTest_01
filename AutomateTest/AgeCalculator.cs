using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomateTest
{
    [TestFixture]
    public static class AgeCalculator
    {
        static IWebDriver driver = null;
        static string url = "https://www.calculatestuff.com/miscellaneous/age-calculator";
        static string date_of_birth = "1992-09-21";
        static string age_at_date = DateTime.Now.ToString("yyyy/MM/dd");

        public static void Main(string[] args){}

        [SetUp]
        public static void SetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]
        [Description("Verify calculate function is working completey.")]
        public static void AutomateTest_01()
        {
            driver.Navigate().GoToUrl(url);
            WaitPageLoad();

            Console.WriteLine("### 1. Put the data set in feild.");
            InputDate(date_of_birth, age_at_date);

            Console.WriteLine("### 2. Click button and check the result box.");
            ClickCalculate();
            Assert.True(IsResultBoxDisplay(), "Result Box not displayed after Click button.");
        }

        [TearDown]
        public static void TearDown()
        {
            driver.Close();
        }

        #region External Function
        public static bool AjaxDoneResult { get; set; }
        public static void WaitPageLoad()
        {
            driver.WaitUntil(d => IsCalculatorBoxDisplay(), 5);
        }
        public static void WaitAjax(this IWebDriver driver, int second)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
                AjaxDoneResult = wait.Until(d => d.IsAjaxDone());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static bool IsCalculatorBoxDisplay()
        {
            IWebElement CalculationBox = driver.FindElement(By.CssSelector("#calculator-form"));
            try { return CalculationBox.Displayed; }
            catch { return false; }
        }
        public static bool IsResultBoxDisplay()
        {
            IWebElement ResultBox = driver.FindElement(By.CssSelector("#results"));
            try { return ResultBox.Displayed; }
            catch { return false; }
        }
        public static void InputDate(string dateOfBirth, string AgeAtDate)
        {
            IWebElement date_of_birth = driver.FindElement(By.CssSelector("#date_of_birth"));
            date_of_birth.SendKeys(dateOfBirth);

            IWebElement age_at_date = driver.FindElement(By.CssSelector("#age_at_date"));
            age_at_date.SendKeys(AgeAtDate);
        }
        public static void ClickCalculate()
        {
            driver.FindElement(By.CssSelector("#calculate-button")).Click();
            WaitAjax(driver,10);
        }
        #endregion
    }
}
