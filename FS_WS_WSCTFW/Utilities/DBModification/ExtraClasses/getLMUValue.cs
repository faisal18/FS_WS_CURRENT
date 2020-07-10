using System;
using OpenQA.Selenium;
using System.Threading;


namespace ClinicianAutomation.ExtraClasses
{
    public class getLMUValue
    {
        public int getdate(string license)
        {


            //IWebDriver driver = new ChromeDriver();
            //string path = System.Web.HttpContext.Current.Server.MapPath("C:\\inetpub\\wwwroot");
            OpenQA.Selenium.Chrome.ChromeDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver("C:\\inetpub\\wwwroot\\");

            try
            {

                
                driver.Navigate().GoToUrl("http://lmu.dimensions-healthcare.com/LMU");
                //driver.Manage().Window.Maximize();

                IWebElement Username = driver.FindElement(By.Id("username"));
                Username.SendKeys("fansari");

                IWebElement Password = driver.FindElement(By.Id("password"));
                Password.SendKeys("cHEmiSTRY@18LMU");

                IWebElement login = driver.FindElement(By.TagName("button"));
                login.Click();
                Thread.Sleep(2000);

                IWebElement filter = driver.FindElement(By.CssSelector("body > div.header-content > div.header_links_container > div:nth-child(3) > a"));
                filter.Click();
                Thread.Sleep(2000);

                IWebElement ClinTab = driver.FindElement(By.CssSelector("#ui-id-3"));
                ClinTab.Click();
                Thread.Sleep(4000);

                IWebElement search_button = driver.FindElement(By.CssSelector("#page > table > tbody > tr:nth-child(2) > td > div > div.pDiv > div.pDiv2 > div:nth-child(1) > div > span"));
                search_button.Click();
                Thread.Sleep(500);

                IWebElement search_box = driver.FindElement(By.CssSelector("#page > table > tbody > tr:nth-child(2) > td > div > div.sDiv > div > input"));
                search_box.SendKeys(license.ToString());
                search_box.SendKeys(Keys.Enter);
                Thread.Sleep(2000);


                IWebElement active_to = driver.FindElement(By.CssSelector("#listGrid > tbody > tr > td:nth-child(7) > div"));
               
                DateTime currentdate = Convert.ToDateTime(DateTime.Today.ToString());
                DateTime active = DateTime.ParseExact(active_to.Text.ToString(), "dd/MM/yyyy", null);
                TimeSpan difference = currentdate - active;
                double days = difference.TotalDays;
                driver.Close();
                if (days > 0 || days == 0)
                {
                    return 0;//InActive
                } 
                else if (days < 0)
                {
                    return 1;//Active
                } 
                else
                {
                    return 2;
                }


            }
            catch (Exception)
            {
                driver.Close();
                return -1;
            }

        }
    }
}