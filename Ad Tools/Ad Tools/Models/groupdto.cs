using Ad_Tools.Common;
using ADTOOLS.DTO;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace Ad_Tools.Models
{
    public class groupdto
    {
        public string BelongsOUPath { get; set; }

        public string Description { get; set; }
        public string Note { get; set; }

        public string DisplayName { get; set; }

        public string DistinguishedName { get; set; }
        public string Email { get; set; }
        public List<string> MembersOf { get; set; }
        public List<string> Members { get; set; }
        public bool isSecurityGroup { get; set; }
        public GroupScope GroupScope { get; set; }
        public string memebertable { get; set; }
        public string members { get; set; }
        public string ManagedBy { get; set; }

        public string SamAccountName { get; set; }
        public groupdto(GroupsDTO gd ,string con,string members)
        {
            this.BelongsOUPath = OuString.OuStringFormat(gd.BelongsOUPath);
            this.Description = gd.Description;
            this.DisplayName = gd.DisplayName;
            this.DistinguishedName = gd.DistinguishedName;
            this.Email = gd.Email;
            this.isSecurityGroup = gd.isSecurityGroup;
            this.ManagedBy = gd.managedBy;
            this.SamAccountName = gd.SamAccountName;
            this.GroupScope=gd.GroupScope;
            this.MembersOf=gd.MembersOf;
            this.memebertable = con;
            this.Note = gd.Note;
            this.Members = gd.Members;
            this.members = members;
        }
    }
}