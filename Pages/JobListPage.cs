using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RazorEngine;
using AventStack.ExtentReports;
using OpenQA.Selenium.Remote;

namespace VeeamCareers.Pages
{
    public class JobListPage : BasePage
    {


        By ClearFilterLocator = By.XPath("(//div[@class='form-group'])[5]//button");

        By DepartmentDDLocator = By.XPath("(//div[@class='form-group'])[2]//button");

        Func<string,By> GetLocatorForDepartment = (departmentName) =>  By.PartialLinkText(departmentName);

        By LanguageDDLocator = By.XPath("(//div[@class='form-group'])[3]//button");
     
        Func<string,By> GetLocatorForLanguage = (language) => By.XPath($"//label[text()='{language}']//preceding::input[@type='checkbox'][1]") ;

        By JobVacancyListLocator = By.XPath("//a[@class='card card-sm card-no-hover' and contains(@href,'/vacancies')]");

        public JobListPage(IWebDriver driver, ExtentTest testReport)
        {
            this.driver = driver;
            this.testReport = testReport;
        }

        public JobListPage ClickOnDepartmentDropdown()
        {
            ClickElement(DepartmentDDLocator);
            return this;
        }

        public JobListPage SelectDepartment(String departmentName)
        {
          
            ClickElement(GetLocatorForDepartment(departmentName));
            return this;
        }

        public JobListPage ClickOnLanguageDropdown()
        {
            ClickElement(LanguageDDLocator);
            return this;
        }

        public JobListPage ClearAllFilters()
        {

            ClickElement(ClearFilterLocator);
        //    driver.FindElement(ClearFilterLocator).Click();
            return this;
        }

        public JobListPage SelectLanguage(String language)
        {

            ClickElement(GetLocatorForLanguage(language));
            return this;
        }

        public  ReadOnlyCollection<IWebElement> GetJobVacancyResultList()
        {
            return RetrieveListOfElements(JobVacancyListLocator);
        }


        public int getNumberOfJobVacancyListed()
        {
            return GetJobVacancyResultList().Count;
        }


    }
}
