using Ad_Tools.Common;
using ADTOOLS.DTO;
using System.Collections.Generic;

namespace Ad_Tools.Models
{

    public class userdto
    {
        private UserDTO ud;
        private string v1;
        private string v2;


        // Properties
        public bool AccountDisabled { get; set; }
        public string AccountExpDate { get; set; }
        public bool AccountExpired { get; set; }
        public bool AccountLocked { get; set; }
        public int AccountStatus { get; set; }
        public string Address { get; set; }
        public string adminDescription { get; set; }
        public string adminDisplayName { get; set; }
        public string City { get; set; }
        public string comment { get; set; }
        public string Company { get; set; }
        public string dcxObjectType { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
       
        public string DisplayName { get; set; }
        public string DistinguishedName { get; set; }
        public bool ExchangeEnabled { get; set; }
        public string extensionAttribute1 { get; set; }
        public string extensionAttribute10 { get; set; }
        public string extensionAttribute11 { get; set; }
        public string extensionAttribute12 { get; set; }
        public string extensionAttribute13 { get; set; }
        public string extensionAttribute14 { get; set; }
        public string extensionAttribute15 { get; set; }
        public string extensionAttribute2 { get; set; }
        public string extensionAttribute3 { get; set; }
        public string extensionAttribute4 { get; set; }
        public string extensionAttribute5 { get; set; }
        public string extensionAttribute6 { get; set; }
        public string extensionAttribute7 { get; set; }
        public string extensionAttribute8 { get; set; }
        public string extensionAttribute9 { get; set; }
        public string FirstName { get; set; }
     
        public string homeDirectory { get; set; }
        public string homeDrive { get; set; }
        private string homeMDB { get; set; }
        public bool isRemoteHomeFolder { get; set; }
        public string LastName { get; set; }
        public string mail { get; set; }
        public string managedBy { get; set; }
        private bool mDBUseDefaults { get; set; }
        public List<string> MemberOf { get; set; }
        public string MobilePhone { get; set; }
        public bool MustChangePassword { get; set; }
        public string Office { get; set; }
        public string otherTelephone { get; set; }
        public string OUName { get; set; }
        public string Pager { get; set; }
        public bool PasswordCantChange { get; set; }
        public bool PasswordNeverExpires { get; set; }
        public string PersonType { get; set; }
        public string PostalCode { get; set; }
        public string PostOfficeBox { get; set; }
        public string profilePath { get; set; }
        public string Province { get; set; }
        public string scriptPath { get; set; }
        public string SipPhone { get; set; }
        public bool SkypeForBusinessEnabled { get; set; }
        public string Telephone { get; set; }
        public int num { get; set; }
        public string Title { get; set; }
        public string UserID { get; set; }
        public List<string> uPNSuffixes { get; set; }
        public string UserPrincipalName { get; set; }
        public string memebertable { get; set; }
        public bool exist { get; set; }
        public userdto(UserDTO ud,string con,List<string> upnName)
        {
            
            this.uPNSuffixes = upnName;
            this.AccountDisabled = ud.AccountDisabled;
            this.AccountExpDate = ud.AccountExpDate.ToString();
            this.AccountExpired = ud.AccountExpired;
            this.AccountLocked = ud.AccountLocked;
            this.AccountStatus = ud.AccountStatus;
            this.Address = ud.Address;
            this.adminDescription = ud.adminDescription;
            this.dcxObjectType = ud.dcxObjectType;
            this.Department = ud.Department;
            this.Description = ud.Description;
            this.extensionAttribute1 = ud.extensionAttribute1;
            this.extensionAttribute2 = ud.extensionAttribute2;
            this.extensionAttribute3 = ud.extensionAttribute3;
            this.extensionAttribute4 = ud.extensionAttribute4;
            this.extensionAttribute5 = ud.extensionAttribute5;
            this.extensionAttribute6 = ud.extensionAttribute6;
            this.extensionAttribute7 = ud.extensionAttribute7;
            this.extensionAttribute8= ud.extensionAttribute8;
            this.extensionAttribute9 = ud.extensionAttribute9;
            this.extensionAttribute10 = ud.extensionAttribute10;
            this.extensionAttribute11 = ud.extensionAttribute11;
            this.extensionAttribute12 = ud.extensionAttribute12;
            this.extensionAttribute13 = ud.extensionAttribute13;
            this.extensionAttribute14 = ud.extensionAttribute14;
            this.extensionAttribute15 = ud.extensionAttribute15;
            this.homeDrive = ud.homeDrive;
            this.isRemoteHomeFolder = ud.isRemoteHomeFolder;
            this.MobilePhone = ud.MobilePhone;
            this.Office = ud.Office;
            this.otherTelephone = ud.otherTelephone;
            this.UserID = ud.UserID;
            this.UserPrincipalName = ud.UserPrincipalName;
            this.Title = ud.Title;
            this.Telephone = ud.Telephone;
            this.SipPhone = ud.SipPhone;
            this.scriptPath = ud.scriptPath;
            this.Province = ud.Province;
            this.profilePath = ud.profilePath;
            this.PostOfficeBox = ud.PostOfficeBox;
            this.PostalCode = ud.PostalCode;
            this.PersonType = ud.PersonType;
            this.Pager = ud.Pager;
            this.MemberOf = ud.MemberOf;
            this.managedBy = ud.managedBy;
            this.mail = ud.mail;
            this.LastName = ud.LastName;
            this.homeDirectory = ud.homeDirectory;
            this.FirstName = ud.FirstName;
            this.extensionAttribute2 = ud.extensionAttribute2;
            this.DistinguishedName = OuString.OuStringFormat(ud.DistinguishedName); ;
            this.MemberOf = ud.MemberOf;
            this.adminDisplayName = ud.adminDisplayName;
            this.City = ud.City;
            this.comment = ud.comment;
            this.Company = ud.Company;
            this.DisplayName = ud.DisplayName;
            this.SkypeForBusinessEnabled = ud.SkypeForBusinessEnabled;
            this.ExchangeEnabled = ud.ExchangeEnabled;
            this.memebertable = con;
        }
        public userdto(UserDTO ud, string con,int num)
        {

            this.num = num;
            this.AccountDisabled = ud.AccountDisabled;
            this.AccountExpDate = ud.AccountExpDate.ToString();
            this.AccountExpired = ud.AccountExpired;
            this.AccountLocked = ud.AccountLocked;
            this.AccountStatus = ud.AccountStatus;
            this.Address = ud.Address;
            this.adminDescription = ud.adminDescription;
            this.dcxObjectType = ud.dcxObjectType;
            this.Department = ud.Department;
            this.Description = ud.Description;
            this.extensionAttribute1 = ud.extensionAttribute1;
            this.extensionAttribute2 = ud.extensionAttribute2;
            this.extensionAttribute3 = ud.extensionAttribute3;
            this.extensionAttribute4 = ud.extensionAttribute4;
            this.extensionAttribute5 = ud.extensionAttribute5;
            this.extensionAttribute6 = ud.extensionAttribute6;
            this.extensionAttribute7 = ud.extensionAttribute7;
            this.extensionAttribute8 = ud.extensionAttribute8;
            this.extensionAttribute9 = ud.extensionAttribute9;
            this.extensionAttribute10 = ud.extensionAttribute10;
            this.extensionAttribute11 = ud.extensionAttribute11;
            this.extensionAttribute12 = ud.extensionAttribute12;
            this.extensionAttribute13 = ud.extensionAttribute13;
            this.extensionAttribute14 = ud.extensionAttribute14;
            this.extensionAttribute15 = ud.extensionAttribute15;
            this.homeDrive = ud.homeDrive;
            this.isRemoteHomeFolder = ud.isRemoteHomeFolder;
            this.MobilePhone = ud.MobilePhone;
            this.Office = ud.Office;
            this.otherTelephone = ud.otherTelephone;
            this.UserID = ud.UserID;
            this.UserPrincipalName = ud.UserPrincipalName;
            this.Title = ud.Title;
            this.Telephone = ud.Telephone;
            this.SipPhone = ud.SipPhone;
            this.scriptPath = ud.scriptPath;
            this.Province = ud.Province;
            this.profilePath = ud.profilePath;
            this.PostOfficeBox = ud.PostOfficeBox;
            this.PostalCode = ud.PostalCode;
            this.PersonType = ud.PersonType;
            this.Pager = ud.Pager;
            this.MemberOf = ud.MemberOf;
            this.managedBy = ud.managedBy;
            this.mail = ud.mail;
            this.LastName = ud.LastName;
            this.homeDirectory = ud.homeDirectory;
            this.FirstName = ud.FirstName;
            this.extensionAttribute2 = ud.extensionAttribute2;
            this.DistinguishedName = OuString.OuStringFormat(ud.DistinguishedName); ;
            this.MemberOf = ud.MemberOf;
            this.adminDisplayName = ud.adminDisplayName;
            this.City = ud.City;
            this.comment = ud.comment;
            this.Company = ud.Company;
            this.DisplayName = ud.DisplayName;
            this.SkypeForBusinessEnabled = ud.SkypeForBusinessEnabled;
            this.ExchangeEnabled = ud.ExchangeEnabled;
            this.memebertable = con;
        }
        public userdto(UserDTO ud,bool exist)
        {

            this.exist = exist;
            this.Address = ud.Address;
            this.adminDescription = ud.adminDescription;
            this.dcxObjectType = ud.dcxObjectType;
            this.Department = ud.Department;
            this.Description = ud.Description;
            
            this.homeDrive = ud.homeDrive;
            this.isRemoteHomeFolder = ud.isRemoteHomeFolder;
            this.MobilePhone = ud.MobilePhone;
            this.Office = ud.Office;
            this.otherTelephone = ud.otherTelephone;
            this.UserID = ud.UserID;
            this.UserPrincipalName = ud.UserPrincipalName;
            this.Title = ud.Title;
            this.Telephone = ud.Telephone;
            this.SipPhone = ud.SipPhone;
            this.scriptPath = ud.scriptPath;
            this.Province = ud.Province;
            this.profilePath = ud.profilePath;
            this.PostOfficeBox = ud.PostOfficeBox;
            this.PostalCode = ud.PostalCode;
            this.PersonType = ud.PersonType;
            this.Pager = ud.Pager;
            this.MemberOf = ud.MemberOf;
            this.managedBy = ud.managedBy;
            this.mail = ud.mail;
            this.LastName = ud.LastName;
            this.homeDirectory = ud.homeDirectory;
            this.FirstName = ud.FirstName;
            this.extensionAttribute2 = ud.extensionAttribute2;
            this.DistinguishedName =OuString.OuStringFormat(ud.DistinguishedName);
            this.MemberOf = ud.MemberOf;
            this.adminDisplayName = ud.adminDisplayName;
            this.City = ud.City;
            this.comment = ud.comment;
            this.Company = ud.Company;
            this.DisplayName = ud.DisplayName;
           
            
        }
    }
}