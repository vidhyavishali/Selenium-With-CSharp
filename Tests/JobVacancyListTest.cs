using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VeeamCareers.Pages;
using VeeamCareers.VeeamBase;

namespace VeeamCareers.Tests
{
    [TestFixture]
    public class JobVacancyListTest : TestReportBase
    {
        [Test]
        [Author("Vidhya Vishali KV")]
        [Description("Number of Job Vacancies verification")]
        [TestCaseSource(nameof(JobVacancySearchData))]
        public void TestNumberOfJobVacancies(String department, String language, int expectedNumberOfResults)
        {


           JobListPage jobListPageObject =  new JobListPage(driver, testReport); 

           int actualNumberOfResults = jobListPageObject.ClearAllFilters().ClickOnDepartmentDropdown().SelectDepartment(department).
               ClickOnLanguageDropdown().SelectLanguage(language).ClickOnLanguageDropdown().getNumberOfJobVacancyListed();
            Thread.Sleep(5000);
            Assert.That(expectedNumberOfResults, Is.EqualTo(actualNumberOfResults));
        }

        static Object[] JobVacancySearchData = { 

            new object[] {"Research & Development", "English", 11 },
            new object[] {"Technical Customer Support", "German", 2 },
            new object[] {"IT","Spanish",0 }
        
        };

    }
}
