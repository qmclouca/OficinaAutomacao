using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace OficinaAutomacao
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configurations
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();
            //optionsHeadless.AddArgument("--headless");

            IWebDriver driver = new ChromeDriver(service, options);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Actions actions = new Actions(driver);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            #endregion Configurations

            #region Navigation to Form Fields
            driver.Navigate().GoToUrl("https://practice-automation.com/");
            driver.Manage().Window.Maximize();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"post-36\"]/div/div[2]/div/div[2]/div[1]/div/a"))).Click();            
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("name"))).SendKeys("Rodolfo");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("drink3"))).Click();
            
            IWebElement colorElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"color4\"]")));
            js.ExecuteScript("arguments[0].scrollIntoView({ block: 'center' });", colorElement);
            Thread.Sleep(500);
            colorElement.Click();

            IWebElement siblingsElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"siblings\"]")));
            js.ExecuteScript("arguments[0].scrollIntoView({ block: 'center' });", siblingsElement);
            Thread.Sleep(500);
            siblingsElement.Click();

            actions.SendKeys(Keys.ArrowDown).SendKeys(Keys.ArrowDown).SendKeys(Keys.Enter).Perform();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"email\"]"))).SendKeys("qmclouca@gmail.com");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"message\"]"))).SendKeys("Formulário preenchido com sucesso!!!");
            IWebElement btnSubmit = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"submit-btn\"]")));
            js.ExecuteScript("arguments[0].scrollIntoView({ block: 'center' });", btnSubmit);
            Thread.Sleep(500);
            btnSubmit.Click();            

            #endregion Navigation to Form Fields
            Console.ReadKey();
            service.Dispose();
            driver.Quit();
        }
    }
}