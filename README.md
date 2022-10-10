# Veeam Careers Test
This is a Selenium - c# Project, using NUnit Framework. Reports are generated using ExtentReport. 

It demonstrates Veeam-careers job vacancies based on given search criteria and number of job vacancies to expected number.



<b><h3>Project Structure Explanation :</h3></b>

1. Test Scripts are written under tests folder. 
2. This Project is run using NUnit framework. 
3. The Page Object Model structure is used, each page represents a webpage, are written under Pages folder. It holds locators for each element in the page.
4. BasePage.cs is the base class for other pages. It has methods to access web driver, web elements and logs each step to the report.
5. TestReportBase.cs file holds setup and tear down for the test runs. It starts the driver, report, flushes them respectively.
6. Reports generated using extent report are stored under /Reports folder as html format.
