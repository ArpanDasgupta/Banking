using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLib
{
    public class Login
    {
        public long Account_No { get; set; }
        public long CRN { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
