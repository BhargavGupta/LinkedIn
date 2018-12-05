using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LinkedIn
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.linkedin.com/");
            driver.Manage().Window.Maximize();
            IWebElement userName_Input = driver.FindElement(By.XPath("//input[@id='login-email']"));
            IWebElement passWord_Input = driver.FindElement(By.XPath("//input[@id='login-password']"));
            IWebElement signIn_Button = driver.FindElement(By.XPath("//input[@id='login-submit']"));
            userName_Input.SendKeys("bhargavgupta607@gmail.com");
            passWord_Input.SendKeys("Anvrsry?3006");
            signIn_Button.Click();
        }
    }
}
