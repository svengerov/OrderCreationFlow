using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DataModels
{
    public class PaymentCreditCardsEntry
    {
        public string numberVisaNo3ds { get; set; }
        public string number3dsAcceptOne { get; set; }
        public string number3dsAcceptTwo { get; set; }
        public string numberVisa3dsFallback { get; set; }
        public string numberVisa3dsAcceptHyp { get; set; }
        public string numberMcNo3ds { get; set; }
        public string numberMc3dsHyp { get; set; }


        public string expireMonth { get; set; }
        public string expireYear { get; set; }
        public string cvvAccept { get; set; }
        public string cvvDecline { get; set; }
        public string cvvChallenge { get; set; }
        public string cvvHypFrictionless { get; set; }


    }
}
