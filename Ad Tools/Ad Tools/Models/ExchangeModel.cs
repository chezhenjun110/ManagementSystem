using System.Collections.Generic;
using System.Web.Mvc;

namespace Ad_Tools.Models
{
    public class ExchangeModel
    {
        public string AccountType { get; set; }
        public string DataCenter { get; set; }
        public List<SelectListItem> Database { get; set; }
        public List<SelectListItem> EAitem { get; set; }
        public List<SelectListItem> QuotaPlan { get; set; }
        public List<SelectListItem> MailboxPolicys { get; set; }
        public string EmailAddress { get; set; }
        public string DisplayName{ get; set; }
        public string LinkedAccountname { get; set; }
        public string LinkedAccountdomain { get; set; }
        public string SendAsDelegates { get; set; }
        public string FMADelegates { get; set; }
        public bool BlackBerryEnabled { get; set; }
        public bool IMAPEnabled { get; set; }
        public bool POP3Enabled { get; set; }
        public bool HideFromOAB { get; set; }
        public bool RestrictedUsage { get; set; }
        
    }
}