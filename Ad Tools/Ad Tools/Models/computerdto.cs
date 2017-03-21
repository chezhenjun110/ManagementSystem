using Ad_Tools.Common;
using ADTOOLS.DTO;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Web;

namespace Ad_Tools.Models
{
    public class computerdto
    {
        public string Address { get; set; }

        public string ADName { get; set; }

        public string AssetNumber { get; set; }

        public string CNName { get; set; }

        public string company { get; set; }

        public string ComputerOnwer { get; set; }

     //   private ComputerPrincipal ComputerPrin { get; set; }

        public string Description { get; set; }

      //  private DirectoryEntry DirEntry { get; set; }

        public string DistinguishedName { get; set; }

        public string dNSHostName { get; set; }

        public string FPC { get; set; }

  //   public IPAddress[] ip { get; set; }

        public string LastLoggedUser { get; set; }

        public string LDAPPath { get; set; }

        public string managedBy { get; set; }

        public string Name { get; set; }

        public string operatingSystem { get; set; }

        public string operatingSystemVersion { get; set; }

        public string OU_CNName { get; set; }

        public string OUName { get; set; }

        public string site { get; set; }

        public string Telephone { get; set; }

        public string TypeName { get; set; }
        public computerdto(ComputerDTO dt)
        {
            this.Address = dt.Address;
            this.ADName = dt.ADName;
            this.AssetNumber = dt.AssetNumber;
            this.CNName = dt.CNName;
            this.company = dt.company;
            this.ComputerOnwer = dt.ComputerOnwer;
            this.Description = dt.Description;
            this.DistinguishedName = dt.DistinguishedName;
            this.dNSHostName = dt.dNSHostName;
            this.FPC = dt.FPC;
            this.LastLoggedUser = dt.LastLoggedUser;
            this.LDAPPath = dt.LDAPPath;
            this.managedBy = dt.managedBy;
            this.Name = dt.Name;
            this.operatingSystem = dt.operatingSystem;
            this.operatingSystemVersion = dt.operatingSystemVersion;
            this.OUName = OuString.OuStringFormat(dt.OUName);
       
           
            this.site = dt.site;
            this.Telephone = dt.Telephone;
            this.TypeName = dt.TypeName;
            
         
        }
    }
}