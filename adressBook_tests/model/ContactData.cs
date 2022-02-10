using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAdressbookTests
{
   public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;

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

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
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
                    return CleanUp(HomePhone) + CleanUp(MobilePhone)
                        + CleanUp(WorkPhone).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string AllEmails { get; set; }
    }
}