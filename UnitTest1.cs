using System;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace PageGoogleAutomação
{
    [TestClass]
    public class BuscaGoogleTest
    {
        IWebDriver _driver;
        WebDriverWait _espera;

        public object ExpectedConditions { get; private set; }

        [TestInitialize]
        public void Initialize(string V)
        {
            _driver = new InternetExplorerDriver(ConfigurationManager.AppSettings[V]);
            _espera = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URLBuscaGoogleTest"]);
        }

        public WebDriverWait Get_espera()
        {
            return _espera;
        }

        public object GetExpectedConditions()
        {
            return ExpectedConditions;
        }

        [TestMethod]
        public void BuscarConcert_PrimeiroResultadoConcert(WebDriverWait _espera, object expectedConditions)
        {
            IWebElement caixaPesquisaGoogle = _driver.FindElement(By.XPath("//input[@title='Pesquisar']"));
            caixaPesquisaGoogle.SendKeys("Selenium WebDriver");

            IWebElement btnPesquisaGoogle = _driver.FindElement(By.XPath("//input[@name='btnK']"));
            btnPesquisaGoogle.SendKeys(Keys.Enter);


            Assert.IsNotNull((_espera.Until( expectedConditions(By.ClassName("rc"))) as IWebElement).FindElement(By.ClassName("r"))
                                                                                                 .FindElement(By.XPath("//a[@href='https://www.concert.com.br/']")));
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Close();
            _driver.Quit();
        }
    }
}