using Microsoft.AspNetCore.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeeamCareers.VeeamBase;

namespace VeeamCareers.Pages
{/**
  * This Class is base for all Pages. It holds the method to access the report object, driver object and web elements.
  * To locate, click and send keys to web elements.
  */
    public class BasePage : TestReportBase
    {
        
        public void WaitForElement(By locator)
        {
            try
            {

                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }

            catch (Exception ex)
            {
                ReportStep("Waiting for Element :" + locator + " failed with exception :" + ex.Message, "fail");
            }

        }


        public void ClickElement(By locator)
        {

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            WaitForElement(locator);
            try
            {
                driver.FindElement(locator).Click();
                ReportStep($"Element : {locator} is clicked", "pass");

            }
            catch (Exception e)
            {
                ReportStep($"Element {locator} click not successful :{e.Message}", "fail");
            }
           
        }


        public void SendKeysToElement(By locator, String text)
        {
            try
            {
                driver.FindElement(locator).SendKeys(text);
                ReportStep($"Send keys {text} to Element : {locator}", "pass");
            }
            catch (Exception e)
            {
                ReportStep($"Send keys to element {locator} not successful : {e.Message}", "fail");
            }
        }

        public IWebElement? LocateElement(By byLocator)
        {
            try
            {

                IWebElement element = driver.FindElement(byLocator);
                ReportStep($"Element is located using {byLocator}", "pass");
                return element;
            }
            catch (Exception e)
            {
                ReportStep($"Element could not be located for {byLocator}: {e.Message}", "fail");
                return null;
            }
        }

        public ReadOnlyCollection<IWebElement> RetrieveListOfElements(By byLocator)
        {
         
                ReadOnlyCollection<IWebElement> elementList = driver.FindElements(byLocator);
                ReportStep($"Elements are retrieved using {byLocator}", "pass");
                 return elementList;
            
        }
}
}
