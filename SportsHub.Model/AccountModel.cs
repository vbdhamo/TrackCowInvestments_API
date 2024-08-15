using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.Model
{
    public class AccountModel
    {
        public int USERID { get; set; }
        public long TENANTCODE { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public int USERTYPE { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public DateTime CREATEDON { get; set; }
        public long CREATEDBY { get; set; }
        public long MODIFIEDBY { get; set; }
        public DateTime MODIFIEDON { get; set; }
        public bool STATUS { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string MOBILE_NUMBER { get; set; }
    }
}
