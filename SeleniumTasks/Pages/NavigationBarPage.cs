using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTasks.Pages
{
    public class NaviogationBarPage : BasePage
    {
        public NaviogationBarPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement AutoCompleteButton => Driver.FindElement(By.XPath($".//*[normalize-space(text())='Auto Complete']"));

        public IWebElement DataPickerButton => Driver.FindElement(By.XPath($".//*[normalize-space(text())='Date Picker']"));
    }
}
