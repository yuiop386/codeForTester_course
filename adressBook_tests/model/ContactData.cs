﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAdressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string address;
        private string homePhone;
        private string workPhone;
        private string mobilePhone;
        private string email;
        private string email2;
        private string email3;

        public ContactData()
        {
        }

        public ContactData(string firstname)
        {
            Firstname = firstname;
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData(string firstname, string lastname, string id)
        {
            Firstname = firstname;
            Lastname = lastname;
            Id = id;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if ((Firstname == other.Firstname) && (Lastname == other.Lastname))
            {
                return true;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return (Firstname + Lastname).GetHashCode();
        }

        public override string ToString()
        {
            return ("contact= " + Lastname + " " + Firstname);
        }

        // 1 if this > other, 0 if this = other, -1 if this < other
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Lastname.CompareTo(other.Lastname) == 0)
            {
                return Firstname.CompareTo(other.Firstname);
            }
            else return Lastname.CompareTo(other.Lastname);
        }

        public string[] DetailedInfo { get; set; }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        public string Address
        {
            get
            {
                if (address != null)
                {
                    return address;
                }
                return "";
            }
            set
            {
                address = value;
            }
        }

        public string HomePhone
        {
            get
            {
                if (homePhone != null)
                {
                    return homePhone;
                }
                return "";
            }
            set
            {
                homePhone = value;
            }
        }

        public string MobilePhone
        {
            get
            {
                if (mobilePhone != null)
                {
                    return mobilePhone;
                }
                return "";
            }
            set
            {
                mobilePhone = value;
            }
        }

        public string WorkPhone
        {
            get
            {
                if (workPhone != null)
                {
                    return workPhone;
                }
                return "";
            }
            set
            {
                workPhone = value;
            }
        }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones 
        {
            get 
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return CleanUpPhones(HomePhone) + CleanUpPhones(MobilePhone)
                        + CleanUpPhones(WorkPhone).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string Email
        {
            get
            {
                if (email != null)
                {
                    return email;
                }
                return "";
            }
            set
            {
                email = value;
            }
        }

        public string Email2
        {
            get
            {
                if (email2 != null)
                {
                    return email2;
                }
                return "";
            }
            set
            {
                email2 = value;
            }
        }

        public string Email3
        {
            get
            {
                if (email3 != null)
                {
                    return email3;
                }
                return "";
            }
            set
            {
                email3 = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmails(Email) + CleanUpEmails(Email2)
                        + CleanUpEmails(Email3)).Trim();
                }
            }

            set
            {
                allEmails = value;
            }
        }

        private string CleanUpPhones(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, @"[A-Za-z(): -]", "") + "\r\n";
        }

        private string CleanUpEmails(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        .Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            };
        }

    }
}