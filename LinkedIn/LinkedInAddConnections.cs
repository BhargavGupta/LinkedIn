using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;

namespace LinkedIn
{
    [TestFixture()]
    class LinkedInAddConnections

    {

        IWebDriver driver;
        [OneTimeSetUp]
        public void IntializeBrowser() {
            //create chrome browser instance
            driver = new ChromeDriver();
        }

        [Test, Order(1)]
        //send application URL as a paramenter
        [TestCase("https://www.linkedin.com/")]
        public void LaunchApplication(String url) {
            //maximize browser
            driver.Manage().Window.Maximize();

            //navigate to LinkedIN application
            driver.Navigate().GoToUrl(url);

        }

        [Test, Order(2)]
        //send username and passwor to login into application
        [TestCase("bhargavqa389@gmail.com", "bhargavqa@389")]
        public void LoginIntoApplication(String username,String password) {
            //Locator for UserName Input
            IWebElement userName_Input = driver.FindElement(By.XPath("//input[@id='login-email']"));

            //Locator for Password Input
            IWebElement passWord_Input = driver.FindElement(By.XPath("//input[@id='login-password']"));

            //Locator for Sign Button
            IWebElement signIn_Button = driver.FindElement(By.XPath("//input[@id='login-submit']"));

            //Enter Username in user name input field
            userName_Input.SendKeys(username);

            //Enter Password in Password Field
            passWord_Input.SendKeys(password);

            //Click on Sign In Button
            signIn_Button.Click();
            Thread.Sleep(5000);
        }
        
        [Test, Order(3)]
        //send searchText and Note to send invitation
        [TestCase("ramesh","Hi, I would like to connect you to my LinkedIn account")]
        public void SearchUsersandAdd(String searchName, String noteText) {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //Locator for Search Input
            IWebElement searchBox_Input = driver.FindElement(By.XPath("//*[@id='nav-search-artdeco-typeahead']//input"));
            //Locator for Search Icon
            IWebElement searchBox_SearchIcon = driver.FindElement(By.XPath("(//li-icon[@type='search-icon'])[1]"));

            //Enter Name in search box and click on search
            searchBox_Input.SendKeys(searchName);
            searchBox_SearchIcon.Click();

            Thread.Sleep(5000);

            //Locator for first name in search list
            IWebElement firstNamein_List = driver.FindElement(By.XPath("(//span[contains(@class,'actor-name')])[1]"));
            js.ExecuteScript("arguments[0].scrollIntoView();", firstNamein_List);
            //Locator for serach list
            IList<IWebElement> DisplayedNames_List = driver.FindElements(By.XPath("//span[contains(@class,'actor-name')]"));
            Thread.Sleep(5000);

            

            //for loop loops through every name in the list and verifies if there is connect button and then connects to user
            for (int i = 0; i < DisplayedNames_List.Count; i++)
            {
                //Converts every name to lower case compares if it matches then it enter into if condition
                if (DisplayedNames_List[i].Text.ToLower().Contains(searchName)) {
                    Thread.Sleep(2000);

                    //assigning name to a string
                    string name = DisplayedNames_List[i].Text;
                    
                    


                    //Below if condition will check whether button contains Message text if button contains message text it will print hte name
                    if (driver.FindElement(By.XPath("//span[text()='" + name + "']/ancestor::div[contains(@class,'search-result__info')]/following-sibling::div")).Text.Contains("Message"))
                    {
                        js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.XPath("//span[text()='" + name + "']")));
                        Console.WriteLine("The "+name+" has Message button so skipping it.");

                    }
                    else if (driver.FindElement(By.XPath("//span[text()='" + name + "']/ancestor::div[contains(@class,'search-result__info')]/following-sibling::div//button")).Text.Equals("Connect"))
                    {
                      // js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.XPath("//span[text()='" + name + "']")));
                        Console.WriteLine("The " + name + " has connect button sending Invitation. ");
                        Thread.Sleep(2000);
                        //Clicks on Connect button based on name
                        driver.FindElement(By.XPath("//span[text()='" + name + "']/ancestor::div[contains(@class,'search-result__info')]/following-sibling::div//button")).Click();
                        Thread.Sleep(5000);

                        //Locator for Add Note button
                        IWebElement addNote_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-secondary-large')]"));

                        //Clicks on Add Note button
                        addNote_Button.Click();
                        Thread.Sleep(2000);

                        //Locator for Note text area
                        IWebElement note_InputBox = driver.FindElement(By.Id("custom-message"));
                        //Locator for Cancel button
                        IWebElement cancel_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-secondary-large')]"));
                        //Locator for Send Invitation button
                        IWebElement sendInvitation_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-primary-large')]"));

                        //Enter note in note text ares
                        note_InputBox.SendKeys(noteText);
                        Thread.Sleep(2000);

                        //Clicks on Send invitation button
                        sendInvitation_Button.Click();
                        Thread.Sleep(2000);

                        Console.WriteLine("Invitation sent to "+name+". ");
                        //driver.Navigate().Refresh();
                        //Thread.Sleep(30000);

                    }
                    else {
                        //Print names which are not matching to entered name
                        Console.WriteLine("name which don't match to entered name " + name+". ");
                    }
                    
                    
                    
                }
                DisplayedNames_List = driver.FindElements(By.XPath("//span[contains(@class,'actor-name')]"));
            }
        }

        [Test, Order(4)]
        public void LogOut() {
            //Logout from application
            //Locator for Me Dorpdown
            IWebElement me_DropDown = driver.FindElement(By.XPath("//button[@id='nav-settings__dropdown-trigger']/div[@class='nav-item__title-container']/span[contains(@class,'nav-item__dropdown-trigger--icon')]"));

            //Click on me dropdown
            me_DropDown.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5000);

            //Locator for Sign Out Link
            IWebElement signOut_Link = driver.FindElement(By.XPath("//a[@data-control-name='nav.settings_signout']"));

            //Click on Signout
            signOut_Link.Click();
        }

        [OneTimeTearDown]
        public void CloseBrowser() {
            //Close Browser
            driver.Close();
        }


    }
}
