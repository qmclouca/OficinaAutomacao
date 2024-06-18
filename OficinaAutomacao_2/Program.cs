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
            IWebElement buttonNewTab = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"post-36\"]/div/div[2]/div/div[2]/div[3]/div/a")));
            js.ExecuteScript("arguments[0].scrollIntoView({ block: 'center' });", buttonNewTab);
            Thread.Sleep(500);
            buttonNewTab.Click();
            #region using New Tab
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"post-1147\"]/div/p[3]/button"))).Click();
            Thread.Sleep(1000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            Thread.Sleep(1000);
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Thread.Sleep(1000);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            #endregion using New Tab

            #region New Window
            string originalWindow = driver.CurrentWindowHandle;
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"post-1147\"]/div/p[7]/button"))).Click();
            wait.Until(driver => driver.WindowHandles.Count == 2);

            var windowsHandles = driver.WindowHandles;

            string newWindow = windowsHandles.First(handles => handles != originalWindow);
            driver.SwitchTo().Window(newWindow);            
            js.ExecuteScript("alert('Janela selecionada com sucesso!');");
            Thread.Sleep(10000);
            driver.Close();
            #endregion New Window
            #endregion Navigation to Form Fields

            driver.Quit();
        }
    }
}