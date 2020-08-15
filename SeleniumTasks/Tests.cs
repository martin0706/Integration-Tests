
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTasks.Pages;
using System.IO;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NuGet.Frameworks;
using System.Drawing;

namespace SeleniumTasks
{
    public class Tests
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private AutoCompletePage _autoCompletePage;
        private DataPickerPage _dataPickerPage;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Url = "https://demoqa.com/";
            _driver.Manage().Window.Maximize();

            _dataPickerPage = new DataPickerPage(_driver);
            _homePage = new HomePage(_driver);
            _autoCompletePage = new AutoCompletePage(_driver);
        }

        [Test]
        public void AutoCompleteTest()
        {

            _homePage.Widget.Click();
            _autoCompletePage.ScrollTo(_autoCompletePage.AutoCompleteButton);
            _autoCompletePage.AutoCompleteButton.Click();


            _autoCompletePage.EnterTextInMultyInput("Red");
            _autoCompletePage.TypeTextInMultyInput("re");
            var text = _autoCompletePage.AutoCompleteMenu.Text;

            Assert.AreEqual("Green", text);
        }

        [TestCase ("January")]
        [Test]
        public void DataPickerTestBackgoundColorOnSelectedElement(string month)
        {

            _homePage.Widget.Click();
            _dataPickerPage.ScrollTo(_dataPickerPage.DataPickerButton);
            _dataPickerPage.DataPickerButton.Click();

            _dataPickerPage.CleanInputData();
            _dataPickerPage.SelectMonthByName($"{month}");
            _dataPickerPage.SelectDayOfMonth(15);
            _dataPickerPage.OpenCalendar();
            
            string elementColor = _dataPickerPage.Days[17].GetCssValue("color");
            Color color = ColorHelper.ParseColor(elementColor);

            string elementBackground = _dataPickerPage.Days[17].GetCssValue("background");
            Color background = ColorHelper.ParseColor(elementBackground);

            Assert.AreEqual("rgb(33, 107, 165)", elementBackground);
            Assert.AreEqual("rgb(255, 255, 255)", elementColor);


        }
        [TestCase("January")]
        [Test]
        public void DataPickerTestWhenHoverOnSelectedElement(string month)
        {

            _homePage.Widget.Click();
            _dataPickerPage.ScrollTo(_dataPickerPage.DataPickerButton);
            _dataPickerPage.DataPickerButton.Click();

            _dataPickerPage.CleanInputData();
            _dataPickerPage.SelectMonthByName($"{month}");
            _dataPickerPage.SelectDayOfMonth(15);
            _dataPickerPage.OpenCalendar();

            _dataPickerPage.HoverOnElement(_dataPickerPage.Days[17]);
            
            string elementColor = _dataPickerPage.Days[17].GetCssValue("color");
            string elementBackground = _dataPickerPage.Days[17].GetCssValue("background");
           

            Assert.AreEqual("rgb(29, 93, 144)", elementBackground);
            Assert.AreEqual("rgb(255, 255, 255)", elementColor);
        }
        [TestCase("January")]
        [Test]
        public void DataPickerTestWhenHoverOnAnyOtherElement(string month)
        {

            _homePage.Widget.Click();
            _dataPickerPage.ScrollTo(_dataPickerPage.DataPickerButton);
            _dataPickerPage.DataPickerButton.Click();

            _dataPickerPage.CleanInputData();
            _dataPickerPage.SelectMonthByName($"{month}");
            _dataPickerPage.SelectDayOfMonth(15);
            _dataPickerPage.OpenCalendar();


            _dataPickerPage.HoverOnElement(_dataPickerPage.Days[16]);

            string elementColor = _dataPickerPage.Days[16].GetCssValue("color");
            string elementBackground = _dataPickerPage.Days[16].GetCssValue("background");


            Assert.AreEqual("rgb(240, 240, 240)", elementBackground);
            Assert.AreEqual("rgb(0, 0, 0)", elementColor);
        }


        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
           //     screenshot.SaveAsFile($"{Directory.GetCurrentDirectory()}/{TestContext.CurrentContext.Test.Name}.png", ScreenshotImageFormat.Png);
            }

            _driver.Quit();
        }
    }
}