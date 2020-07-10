using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace ClinicianAutomation.ExtraClasses
{
    public class runUtilities
    {
        public int util(string license, string user, string password)
        {
            string clin_lics = license.ToString();
            string clin_user = user.ToString();
            string clin_pass = password.ToString();
            //string map = System.Web.HttpContext.Current.Server.MapPath("/App_Data/");
            //IWebDriver driver = new ChromeDriver(map.ToString());
            IWebDriver driver = new ChromeDriver();

            try
            {
                


                driver.Navigate().GoToUrl("http://automation.dimensions-healthcare.com/AdminUtils/Account/Login");
                //driver.Manage().Window.Maximize();
                Thread.Sleep(2000);

                IWebElement email_login = driver.FindElement(By.Id("MainContent_Email"));
                email_login.SendKeys("fansari@dimensions-healthcare.com");

                IWebElement password_login = driver.FindElement(By.Id("MainContent_Password"));
                password_login.SendKeys("F@ns@rI@123");

                IWebElement submit_login = driver.FindElement(By.CssSelector("#loginForm > div > div:nth-child(6) > div > input"));
                submit_login.Click();
                Thread.Sleep(3000);

                IWebElement gotoutilities = driver.FindElement(By.LinkText("DHPO Clinician Update"));
                gotoutilities.Click();

                IWebElement Clinlic = driver.FindElement(By.CssSelector("#MainContent_txtCliniLic"));
                Clinlic.SendKeys(clin_lics.ToString());

                IWebElement rd_btn = driver.FindElement(By.CssSelector("#MainContent_rdOperations_1"));
                rd_btn.Click();

                IWebElement username = driver.FindElement(By.Id("MainContent_txtClinUserName"));
                username.SendKeys(clin_user.ToString());

                IWebElement pass = driver.FindElement(By.Id("MainContent_txtClinPass"));
                pass.SendKeys(clin_pass.ToString());

                IWebElement generate = driver.FindElement(By.Id("MainContent_btnGenerateQuery"));
                generate.Click();
                Thread.Sleep(4000);

                IWebElement submit2 = driver.FindElement(By.Id("MainContent_btnSaveScript"));
                submit2.Click();
                Thread.Sleep(2000);

                IWebElement txtbox = driver.FindElement(By.Id("MainContent_txtFullLog"));
                string val = txtbox.Text.ToString();
                driver.Close();

                if (val.ToString() == "file save successfully")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }


            }
            catch (Exception)
            {
                driver.Close();
                return -1;
            }
        }

        public int util(string license)
        {

            string clin_lics = license.ToString();
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("http://automation.dimensions-healthcare.com/AdminUtils/Account/Login");
                //driver.Manage().Window.Maximize();
                Thread.Sleep(2000);

                IWebElement email_login = driver.FindElement(By.Id("MainContent_Email"));
                email_login.SendKeys("fansari@dimensions-healthcare.com");

                IWebElement password_login = driver.FindElement(By.Id("MainContent_Password"));
                password_login.SendKeys("F@ns@rI@123");

                IWebElement submit_login = driver.FindElement(By.CssSelector("#loginForm > div > div:nth-child(6) > div > input"));
                submit_login.Click();
                Thread.Sleep(3000);

                IWebElement gotoutilities = driver.FindElement(By.LinkText("DHPO Clinician Update"));
                gotoutilities.Click();

                IWebElement Clinlic = driver.FindElement(By.CssSelector("#MainContent_txtCliniLic"));
                Clinlic.SendKeys(clin_lics.ToString());

                IWebElement rd_btn = driver.FindElement(By.CssSelector("#MainContent_rdOperations_2"));
                rd_btn.Click();

                IWebElement generate = driver.FindElement(By.Id("MainContent_btnGenerateQuery"));
                generate.Click();
                Thread.Sleep(4000);

                IWebElement submit2 = driver.FindElement(By.Id("MainContent_btnSaveScript"));
                submit2.Click();
                Thread.Sleep(2000);

                IWebElement txtbox = driver.FindElement(By.Id("MainContent_txtFullLog"));
                string val = txtbox.Text.ToString();
                driver.Close();

                if (val.ToString() == "file save successfully")
                {
                    return 1;
                }
                else
                {
                    return 0;
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