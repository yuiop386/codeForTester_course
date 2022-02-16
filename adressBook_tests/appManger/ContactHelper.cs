using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAdressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Remove(ContactData toBeRemoved)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(toBeRemoved.Id);
            RemoveContact();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Remove(int toBeRemoved)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(toBeRemoved);
            RemoveContact();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(int rowNumber, ContactData newContactData)
        {
            manager.Navigator.GoToHomePage();
            ClickModifyButton(rowNumber);
            FillContactForm(newContactData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAddContactTo(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);

        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupInFilter(group.Name);
            SelectContact(contact.Id);
            CommitRemovingFromGroup();
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);

            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contact.Address);

            driver.FindElement(By.Name("home")).Clear();
            driver.FindElement(By.Name("home")).SendKeys(contact.HomePhone);
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys(contact.MobilePhone);
            driver.FindElement(By.Name("work")).Clear();
            driver.FindElement(By.Name("work")).SendKeys(contact.WorkPhone);

            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            driver.FindElement(By.Name("email2")).Clear();
            driver.FindElement(By.Name("email2")).SendKeys(contact.Email2);
            driver.FindElement(By.Name("email3")).Clear();
            driver.FindElement(By.Name("email3")).SendKeys(contact.Email3);

            return this;
        }

        public ContactData GetContactInformationFromForm(int rowNumber)
        {
            manager.Navigator.GoToHomePage();
            ClickModifyButton(rowNumber);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public string GetContactInformationFromDetails(int rowNumber)
        {
            manager.Navigator.GoToHomePage();
            ClickDetailsButton(rowNumber);
            return driver.FindElement(By.CssSelector("div#content"))
                .Text.Replace("\r\n", "").Replace("H: ", "").Replace("M: ", "")
                .Replace("W: ", "");
        }

        public ContactData GetContactInformationFromTable(int rowNumber)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver
                .FindElements(By.Name("entry"))[rowNumber].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        private void CommitRemovingFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectGroupInFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        private void SelectGroupToAddContactTo(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath("//input[@name='selected[]' and @value='" + id + "']")).Click();
            return this;
        }

        private ContactHelper SelectContact(int rowNumber)
        {
            driver.FindElement(By
                .XPath($"//table[@id='maintable']/tbody/tr[{rowNumber + 2}]/td/input")).Click();
            return this;
        }

        private ContactHelper ClickModifyButton(int rowNumber)
        {
            driver.FindElement(By.XPath($"//tr[{rowNumber + 2}]/td[8]/a/img")).Click();
            return this;
        }

        private ContactHelper ClickDetailsButton(int rowNumber)
        {
            driver.FindElement(By.XPath($"//tr[{rowNumber + 2}]/td[7]/a/img")).Click();
            return this;
        }

        private ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper NewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> elements 
                    = driver.FindElements(By.XPath("//tr[position()>1]"));
                foreach (var element in elements)
                {
                    var lastname = element.FindElement(By.XPath("td[2]"));
                    var firstname = element.FindElement(By.XPath("td[3]"));
                    var Id = element.FindElement(By.TagName("input")).GetAttribute("value");
                    contactCache.Add(new ContactData(firstname.Text, lastname.Text, Id));
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//tr[position()>1]")).Count;
        }

        public int GetNumberOfSerachResults()
        {
            manager.Navigator.GoToHomePage();
            string serachResults = driver.FindElement(By.Id("search_count")).Text;
            return Int32.Parse(serachResults);
        }
    }
}
