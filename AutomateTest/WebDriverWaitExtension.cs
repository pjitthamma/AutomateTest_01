using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomateTest
{
    public static class WebDriverWaitExtension
    {
        public static TResult WaitUntil<TResult>(this IWebDriver driver, Func<IWebDriver, TResult> condition, int second)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            return wait.UntilCondition(condition, second);
        }

        public static TResult UntilCondition<TResult>(this WebDriverWait driverWait, Func<IWebDriver, TResult> condition, int waitingTime)
        {
            driverWait.Timeout = TimeSpan.FromSeconds(waitingTime);

            try { return driverWait.Until(condition); }
            catch (WebDriverTimeoutException) { return default(TResult); }
        }

        public static bool IsAjaxDone(this IWebDriver driver)
        {
            try
            {
                return (driver.IsAjaxDefined()) ? driver.CheckInjectJavascript("return window.jQuery != undefined && jQuery.active === 0 && document.readyState == 'complete'") : true;
            } catch { return false; }
        }

        public static bool IsAjaxDefined(this IWebDriver driver)
        {
            var javaScriptExecutor = driver as IJavaScriptExecutor;
            return javaScriptExecutor != null && (bool)javaScriptExecutor.ExecuteScript("return window.jQuery != undefined");
        }

        public static bool CheckInjectJavascript(this IWebDriver driver, string script)
        {
            var js = driver as IJavaScriptExecutor;
            return js != null && (bool)js.ExecuteScript(script);
        }
    }
}
