using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DataModels
{
    public class CheckoutEntry
    {
        public string city { get; set; }
        public string streetAddress { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string postIndex { get; set; }
        public string internal_country { get; set; }
        public string RutNumber { get; set; }
    }
}
