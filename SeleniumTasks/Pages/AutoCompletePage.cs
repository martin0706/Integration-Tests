using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumTasks.Pages
{
    class AutoCompletePage : NaviogationBarPage
    {
        public AutoCompletePage(IWebDriver driver) : base(driver)
        {

        }

        public List<IWebElement> Inputs => Driver.FindElements(By.XPath("//*[@class = 'auto-complete__input']/input")).ToList();


        public IWebElement TypeMultyInput => Inputs[0];

        public void TypeTextInMultyInput(string key)
        {
            Actions.MoveToElement(TypeMultyInput).Perform();
            TypeMultyInput.SendKeys(key);
        }

        public void EnterTextInMultyInput(string key)
        {
            Actions.MoveToElement(TypeMultyInput).Perform();
            TypeMultyInput.SendKeys(key);
            TypeMultyInput.SendKeys(Keys.Enter);
        }


        public IWebElement AutoCompleteMenu => Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"autoCompleteMultipleContainer\"]/div[2]")));

    }
}
