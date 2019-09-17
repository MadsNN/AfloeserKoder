using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afloeser.Models
{
    public class ADUser
    {
        string displayName;
        string sAMAccountName;
        string mail;
        string company;
        string department;
        string lastLogonTimestamp;
        string accountExpires;

        public ADUser(string displayName, string sAMAccountName, string mail, string company, string department)
        {
            this.displayName = displayName;
            this.sAMAccountName = sAMAccountName;
            this.mail = mail;
            this.company = company;
            this.department = department;
        }

        public ADUser(string displayName, string sAMAccountName, string mail, string company, string department, string lastLogonTimestamp)
        {
            this.displayName = displayName;
            this.sAMAccountName = sAMAccountName;
            this.mail = mail;
            this.company = company;
            this.department = department;
            this.lastLogonTimestamp = lastLogonTimestamp;
        }

        public ADUser(string displayName, string department)
        {
            this.displayName = displayName;
            this.department = department;
        }

        public ADUser(string displayName, string accountExpires, string sAMAccountName)
        {
            this.displayName = displayName;
            this.accountExpires = accountExpires;
            this.sAMAccountName = sAMAccountName;
        }


        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        public string SAMAccountName
        {
            get { return sAMAccountName; }
            set { sAMAccountName = value; }
        }
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        public string LastLogonTimestamp
        {
            get { return lastLogonTimestamp; }
            set { lastLogonTimestamp = value; }
        }

        public string AccountExpires
        {
            get { return accountExpires; }
            set { accountExpires = value; }
        }
    }
}
