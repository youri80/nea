using System;
using System.Collections.Generic;

namespace er_nea.Models
{
    public class Share
    {
        public Share()
        {
        }

        public string Mfid { get; set; }

        public string Cls { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
