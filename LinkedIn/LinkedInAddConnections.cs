using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;
using System.Text.RegularExpressions;

namespace LinkedIn
{
    public static class WebElementExtensions
    {
        public static bool ElementIsPresent(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
    [TestFixture()]
    class LinkedInAddConnections

    {

        IWebDriver driver;
        [OneTimeSetUp]
        public void IntializeBrowser()
        {
            //create chrome browser instance
            driver = new ChromeDriver();
        }

        [Test, Order(1)]
        //send application URL as a paramenter
        [TestCase("https://www.linkedin.com/")]
        public void LaunchApplication(String url)
        {
            //maximize browser
            driver.Manage().Window.Maximize();

            //navigate to LinkedIN application
            driver.Navigate().GoToUrl(url);

        }

        [Test, Order(2)]
        //send username and passwor to login into application
        [TestCase("bhargavqa389@gmail.com", "bhargavqa@389")]
        public void LoginIntoApplication(String username, String password)
        {
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
            Thread.Sleep(2000);
        }

        [Test, Order(3)]
        //send searchText and Note to send invitation
        [TestCase("vamshi", "Hi, I would like to connect you to my LinkedIn account", 5, "Connect Me to your LinkedIn", "San Antonio")]
        public void SearchUsersandAdd(String searchName, String noteText, int pageCount, String subjectText, String city)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //Locator for Search Input
            IWebElement searchBox_Input = driver.FindElement(By.XPath("//*[@id='nav-search-artdeco-typeahead']//input"));
            //Locator for Search Icon
            IWebElement searchBox_SearchIcon = driver.FindElement(By.XPath("(//li-icon[@type='search-icon'])[1]"));

            //Enter Name in search box and click on search
            searchBox_Input.SendKeys(searchName + " in " + city);
            searchBox_SearchIcon.Click();

            Thread.Sleep(2000);

            //Locator for first name in search list
            IWebElement firstNamein_List = driver.FindElement(By.XPath("(//span[contains(@class,'actor-name')])[1]"));
            IWebElement searchResults_Text = driver.FindElement(By.XPath("//h3[contains(@class,'search-results__total')]"));
            js.ExecuteScript("arguments[0].scrollIntoView();", searchResults_Text);



          //this for loop loops based on page count
            for (int j = 0; j < pageCount; j++) {
                //Locator for serach list
                IList<IWebElement> DisplayedNames_List = driver.FindElements(By.XPath("//span[contains(@class,'actor-name')]"));
                Thread.Sleep(2000);

                //for loop to collect data from list and worl with that
                for (int i = 0; i < DisplayedNames_List.Count; i++)
            {
                   
                    //Converts every name to lower case compares if it matches then it enter into if condition
                    if (DisplayedNames_List[i].Text.ToLower().Contains(searchName))
                {
                    Thread.Sleep(2000);

                    //assigning text to a string called name
                    string name = DisplayedNames_List[i].Text;
                    Thread.Sleep(2000);
                    
                     //This IF verifies the locality of the user if it as expected then enter into the IF block
                    if (driver.ElementIsPresent(By.XPath("//span[text()='" + name + "']/ancestor::a[contains(@class,'search-result__result-link')]/following-sibling::p[contains(@class,'subline-level-2')]/span[contains(text(),'" + city + "')]")))
                    {
                       
                            //This IF verifies whether 1st is dispalyed beside the name if it is displayed then go's to else part(displaying 1st means already connected)
                        if (driver.FindElement(By.XPath("//span[text()='" + name + "']/following-sibling::span/span[2]")).Text.Contains('1'))
                        {
                            Console.WriteLine("The User " + name + "already connected");
                                if (i == 0 || i == 1) { }
                                else
                                {
                                    js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.XPath("(//span[@class='name-and-distance']/span[@class='name actor-name'])[" + (i - 1) + "]")));
                                }
                            }
                        //Below if condition will check whether button contains Message text if button contains message text it will print the name
                        else if (driver.ElementIsPresent(By.XPath("//span[text()='" + name + "']/ancestor::div[contains(@class,'search-result__info')]/following-sibling::div//a//li-icon")))
                        {
                                Thread.Sleep(2000);
                                Console.WriteLine("Button related to user " + name + " has Message text with Lock icon. this is Premium account so opening that account and adding it.");
                            driver.FindElement(By.XPath("//span[@class='name-and-distance']/span[text()='" + name + "']")).Click();
                            Thread.Sleep(2000);
                            IWebElement profileActions_Button = driver.FindElement(By.XPath("//span[contains(@class,'pv-s-profile-actions__overflow')]/button"));
                            profileActions_Button.Click();
                            Thread.Sleep(2000);
                            IWebElement connect_Button = driver.FindElement(By.XPath("//button[contains(@class,'pv-s-profile-actions pv-s-profile-actions--connect')]/span[1]"));
                            connect_Button.Click();
                            Thread.Sleep(2000);

                            //Locator for Add Note button
                            IWebElement addNote_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-secondary-large')]"));

                            //Clicks on Add Note button
                            addNote_Button.Click();
                            Thread.Sleep(2000);

                            //Locator for Note text area
                            IWebElement note_InputBox = driver.FindElement(By.Id("custom-message"));
                            //Locator for Cancel button
                            //IWebElement cancel_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-secondary-large')]"));
                            //Locator for Send Invitation button
                            IWebElement sendInvitation_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-primary-large')]"));

                            //Enter note in note text ares
                            note_InputBox.SendKeys(noteText);
                            Thread.Sleep(2000);

                            //Clicks on Send invitation button
                            sendInvitation_Button.Click();
                            Thread.Sleep(2000);

                            Console.WriteLine("Invitation sent to " + name + ". ");


                            Thread.Sleep(2000);
                            driver.Navigate().Back();
                            Thread.Sleep(2000);
                                if (driver.ElementIsPresent(By.XPath("//h3[contains(@class,'search-results__total')]")))
                                { }
                                else {
                                    driver.Navigate().Back();
                                    Thread.Sleep(2000);
                                }
                               
                                Thread.Sleep(2000);
                                if (i == 0 || i == 1) { }
                                else
                                {
                                    js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.XPath("(//span[@class='name-and-distance']/span[@class='name actor-name'])[" + (i - 1) + "]")));
                                }

                            } //Below if condition will check whether button contains Connect text if button contains Connect text it will connect to those users
                            else if (driver.ElementIsPresent(By.XPath("//span[text()='" + name + "']/ancestor::div[contains(@class,'search-result__info')]/following-sibling::div//button[text()='Connect']")))
                            {
                                //js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.XPath("//span[@class='name-and-distance']/span[text()='" + name + "']")));
                                Console.WriteLine("The " + name + " has connect button sending Invitation. ");
                            Thread.Sleep(2000);
                            //Clicks on Connect button based on name
                            driver.FindElement(By.XPath("//span[text()='" + name + "']/ancestor::div[contains(@class,'search-result__info')]/following-sibling::div//button")).Click();
                            Thread.Sleep(2000);

                            //Locator for Add Note button
                            IWebElement addNote_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-secondary-large')]"));

                            //Clicks on Add Note button
                            addNote_Button.Click();
                            Thread.Sleep(2000);

                            //Locator for Note text area
                            IWebElement note_InputBox = driver.FindElement(By.Id("custom-message"));
                            //Locator for Cancel button
                            //IWebElement cancel_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-secondary-large')]"));
                            //Locator for Send Invitation button
                            IWebElement sendInvitation_Button = driver.FindElement(By.XPath("//div[@class='send-invite__actions']/button[contains(@class,'button-primary-large')]"));

                            //Enter note in note text ares
                            note_InputBox.SendKeys(noteText);
                            Thread.Sleep(2000);

                            //Clicks on Send invitation button
                            sendInvitation_Button.Click();
                            Thread.Sleep(2000);

                            Console.WriteLine("Invitation sent to " + name + ". ");
                                //driver.Navigate().Refresh();
                                //Thread.Sleep(30000);
                                if (i == 0 || i == 1) { }
                                else
                                {
                                    js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.XPath("(//span[@class='name-and-distance']/span[@class='name actor-name'])[" + (i - 1) + "]")));
                                }
                            }
                        else
                        {
                            //Print names which are not matching to entered name
                            Console.WriteLine("name which don't match to entered name " + name + ". ");
                                if (i == 0 || i == 1) { }
                                else
                                {
                                    js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.XPath("(//span[@class='name-and-distance']/span[@class='name actor-name'])[" + (i - 1) + "]")));
                                }
                            }

                    }




                }
                DisplayedNames_List = driver.FindElements(By.XPath("//span[contains(@class,'actor-name')]"));
            }
            Thread.Sleep(2000);

                //IList<IWebElement> noOfPage_List = driver.FindElements(By.XPath("//li[@class='page-list']/ol/li"));

                if (j == pageCount - 1)
                {
                }
                else {
                    if (driver.ElementIsPresent(By.XPath("//button/div[@class='next-text']")))
                    {
                        js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.XPath("//button/div[@class='next-text']")));
                        IWebElement next_Button = driver.FindElement(By.XPath("//button/div[@class='next-text']"));
                        next_Button.Click();
                        Thread.Sleep(2000);

                    }
                    else {
                        j = pageCount;
                    }
                }

                       }

        }

        [Test, Order(4)]
        public void LogOut()
        {
            //Logout from application
            //Locator for Me Dorpdown
            IWebElement me_DropDown = driver.FindElement(By.XPath("//button[@id='nav-settings__dropdown-trigger']/div[@class='nav-item__title-container']/span[contains(@class,'nav-item__dropdown-trigger--icon')]"));

            //Click on me dropdown
            me_DropDown.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);

            //Locator for Sign Out Link
            IWebElement signOut_Link = driver.FindElement(By.XPath("//a[@data-control-name='nav.settings_signout']"));

            //Click on Signout
            signOut_Link.Click();
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            //Close Browser
            driver.Close();
        }


    }
}
