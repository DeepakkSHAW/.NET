using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DKBank.API.Model
{
    public class Customers
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastname { get; set; }

        public DateTime dtDOB { get; set; }
    }
}
