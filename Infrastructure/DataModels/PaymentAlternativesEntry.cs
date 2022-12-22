using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DataModels
{
    public class PaymentAlternativesEntry
    {
        public string paypalUser { get; set; }
        public string paypalPass { get; set; }
        public string acceptPhone { get; set; }

        public string declinePhone { get; set; }

    }
}
