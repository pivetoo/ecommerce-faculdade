using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Ecommerce.UnitTests.TestesUI
{
    public class AutenticacaoTestes : IDisposable
    {
        private ChromeDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        [Test]
        public void DeveRegistrarLoginEditarPerfil()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _driver.Navigate().GoToUrl("http://localhost:5173/criar-conta");
            _driver.FindElement(By.Id("nome")).SendKeys("Teste Usuário");
            _driver.FindElement(By.Id("email")).SendKeys("novo@teste.com");
            _driver.FindElement(By.Id("senha")).SendKeys("senha123");
            _driver.FindElement(By.Id("confirmarSenha")).SendKeys("senha123");
            _driver.FindElement(By.Id("criarContaButton")).Click();

            _driver.Navigate().GoToUrl("http://localhost:5173");
            _driver.FindElement(By.Id("email")).SendKeys("novo@teste.com");
            _driver.FindElement(By.Id("senha")).SendKeys("senha123");
            _driver.FindElement(By.Id("loginButton")).Click();

            _driver.Navigate().GoToUrl("http://localhost:5173/perfil");
            _driver.FindElement(By.Id("email")).SendKeys("Email Editado");
            _driver.FindElement(By.Id("salvarPerfil")).Click();

            var emailPerfil = _driver.FindElement(By.Id("email")).GetAttribute("value");
            Assert.AreEqual("Email Editado", emailPerfil);
        }

        public void Dispose()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}
