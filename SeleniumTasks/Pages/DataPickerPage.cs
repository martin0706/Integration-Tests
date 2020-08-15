
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;

namespace SeleniumTasks.Pages
{
    public class DataPickerPage:NaviogationBarPage
    {
        public DataPickerPage(IWebDriver driver):base(driver)
        {

        }

        public IWebElement DataInput => Driver.FindElement(By.Id("datePickerMonthYearInput"));

        public IWebElement SelectedDay => Days[17];

        public SelectElement Month => new SelectElement(Driver.FindElement(By.ClassName("react-datepicker__month-select")));
        public List<IWebElement> Days => Driver.FindElements(By.ClassName("react-datepicker__day")).ToList();
        public IWebElement Title => Driver.FindElement(By.ClassName("react-datepicker__current-month"));

        public int GetDayFromDataInput()
        {
            //System.Threading.Thread.Sleep(2000);
            var dataAsText = DataInput.GetAttribute("value");
            var partsOfDate = dataAsText.Split("/");
            return int.Parse(partsOfDate[1]);
        }

        public void OpenCalendar()
        {
            DataInput.Click();
        }

        public void HoverOnElement(IWebElement element)
        {
            Actions.MoveToElement(element).Build().Perform();
        }

        public void SelectDayOfMonth(int day)
        {
            foreach (var d in Days)
            {
                
                if (d.Text == day.ToString() )
                {
                    
                    d.Click();
    
                    break;
                }
            }
        }

        

        public void SelectMonthByName(string nameOfMonth)
        {
            Month.SelectByText(nameOfMonth);
        }

        public void CleanInputData()
        {

            DataInput.SendKeys(Keys.Control + "a");
            DataInput.SendKeys(Keys.Delete);
        }
    }
}
