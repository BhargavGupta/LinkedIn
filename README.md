
Description:
Scripts to automate LinkedIn. Login into application search for a name in search input box. Collect search data in a list and connect to members who have connect button

Technology/Framework
Technology: C#, Selenium, NUnit Framework
Prerequisites:
Visual Studio, C#, Selenium, NUnit Framework, Browser.
Installing Visual Studio:
1)	Navigate to the URL https://www.visualstudio.com/downloads/ and Click on the 'Free download' button displayed on Visual Studio Community 2017.
2)	Click on Downloaded file and click on Continue.
3)	A window will open select checkboxes related to “Universal Windows Platform development” and “.NET desktop development” and click on install.
4)	Once software installed restart Visual Studio.

Create a new project:
1)	Click on File menu click New and then Project.
2)	Select the option 'Visual C#'.
3)	Click on Console App (.Net Framework).
4)	Enter name as "LinkedIn".
5)	Click OK.
6)	Program.cs file will create.
Setup Selenium in Visual Studio:
1)	Click on Tools menu.
2)	Click on NuGet Package Manager.
3)	Click on Manage NuGet Packages for Solution.
4)	Click on browse tab
5)	Enter Selenium in search input a list will be displayed select 1st from left side list, select project from right side window and click on install.

6)	Click on Ok button in popup
7)	After installation done  ===============Finished=========== will be displayed in the Output window.
Setup NUnit Framework in Visual Studio:
1)	Click on Tools menu.
2)	Click on NuGet Package Manager.
3)	Click on Manage NuGet Packages for Solution.
4)	Click on browse tab
5)	Enter NUnit in search input a list will be displayed select 1st from left side list, select project from right side window and click on install. (same as discussed in selenium installation)
Setup NUnit Test Adapter in Visual Studio:
1)	Click on Tools menu.
2)	Click on NuGet Package Manager.
3)	Click on Manage NuGet Packages for Solution.
4)	Click on browse tab
5)	Enter NUnit Test Adapter in search input a list will be displayed select 1st from left side list, select project from right side window and click on install. (same as discussed in selenium installation)

Setup Chrome Driver in Visual Studio:
1)	Click on Tools menu.
2)	Click on NuGet Package Manager.
3)	Click on Manage NuGet Packages for Solution.
4)	Click on browse tab
5)	Enter Chrome Web driver in search input a list will be displayed select Selenium.WebDriver.ChromeDriver from left side list, select project from right side window and click on install. (same as discussed in selenium installation)


Test Creation:
1)	Right click on project Add and click on Class.
2)	Provide name to class “LinkedInAddConections”
3)	Click on Add Button
Sample code for initializing browser and navigating to Linked IN:
       [OneTimeSetUp]
        public void IntializeBrowser() {
            //create chrome browser instance
            driver = new ChromeDriver();
        }

        [TestCase("https://www.linkedin.com/")]
        public void LaunchApplication(String url) {
            //maximize browser
            driver.Manage().Window.Maximize();

            //navigate to LinkedIN application
            driver.Navigate().GoToUrl(url);

        }
4)	Clone project to local repository 
5)	Click on File menu in Visual Studio
6)	Mouse over to Open and click on Project/Solution
7)	A window will open navigate to local repository.
8)	Select the file with extension “.sln” and click on open
9)	Project will open in visual studio
10)	Change Username, Password, SearchName and Note as per your requirement. 

Running Tests:
1)	Click on Build menu and click Build Solution
2)	Test Explorer window will open.
3)	Right click on “LinkedInAddConnections” class and click Run Selected Tests
4)	Execution will start
5)	After execution is done click on “SearchUsersandAdd” method in Test Explorer
6)	Click on Output link which is in bottom of Test Explorer
7)	Output will be displayed as below for Searchname =”ramesh”

Ramesh Suddala have Message button so skipping it.
Ramesh Kumar Mallula have connect button sending Invitation.
Invitation sent to Ramesh Kumar Mallula.
Ramesh Dharmarajan (D Ramesh) have Message button so skipping it. 
Ramesh Rame have Message button so skipping it
Ramesh Timilsina have Message button so skipping it 
Ramesh Babu K have Message button so skipping it 
Ramesh Narra have connect button sending Invitation
Invitation sent to Ramesh Narra
Ramesh Eggadi have Message button so skipping it 
Ramesh Babu have Message button so skipping it 
Dev Ramesh M have Message button so skipping it

