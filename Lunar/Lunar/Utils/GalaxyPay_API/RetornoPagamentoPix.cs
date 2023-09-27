using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.GalaxyPay_API
{
    public class RetornoPagamentoPix
    {

        public class GalaxPayRetornoPix
        {
            public int totalQtdFoundInPage { get; set; }
            public Transaction[] Transactions { get; set; }
        }

        public class Transaction
        {
            public int galaxPayId { get; set; }
            public int value { get; set; }
            public string payday { get; set; }
            public string paydayDate { get; set; }
            public int installment { get; set; }
            public string status { get; set; }
            public string statusDescription { get; set; }
            public string createdAt { get; set; }
            public object myId { get; set; }
            public object additionalInfo { get; set; }
            public object datetimeLastSentToOperator { get; set; }
            public int chargeGalaxPayId { get; set; }
            public string chargeMyId { get; set; }
            public int fee { get; set; }
            public object[] ConciliationOccurrences { get; set; }
            public Boleto Boleto { get; set; }
            public Antifraud Antifraud { get; set; }
            public Pix Pix { get; set; }
            public Charge Charge { get; set; }
        }

        public class Boleto
        {
            public string pdf { get; set; }
            public object bankLine { get; set; }
            public object bankNumber { get; set; }
            public object barCode { get; set; }
            public object bankEmissor { get; set; }
            public object bankAgency { get; set; }
            public object bankAccount { get; set; }
        }

        public class Antifraud
        {
            public object ip { get; set; }
            public object sessionId { get; set; }
            public bool sent { get; set; }
            public object approved { get; set; }
        }

        public class Pix
        {
            public string reference { get; set; }
            public string qrCode { get; set; }
            public string image { get; set; }
            public string page { get; set; }
            public string endToEnd { get; set; }
        }

        public class Charge
        {
            public string myId { get; set; }
            public int galaxPayId { get; set; }
            public int value { get; set; }
            public string paymentLink { get; set; }
            public string mainPaymentMethodId { get; set; }
            public string status { get; set; }
            public object additionalInfo { get; set; }
            public string createdAt { get; set; }
            public string updatedAt { get; set; }
            public bool payedOutsideGalaxPay { get; set; }
            public Customer Customer { get; set; }
            public object[] Transactions { get; set; }
            public object[] ExtraFields { get; set; }
            public Paymentmethodpix PaymentMethodPix { get; set; }
        }

        public class Customer
        {
            public string myId { get; set; }
            public int galaxPayId { get; set; }
            public string name { get; set; }
            public string document { get; set; }
            public string createdAt { get; set; }
            public string updatedAt { get; set; }
            public string[] emails { get; set; }
            public long[] phones { get; set; }
            public Address Address { get; set; }
            public object[] ExtraFields { get; set; }
        }

        public class Address
        {
            public string zipCode { get; set; }
            public string street { get; set; }
            public string number { get; set; }
            public object complement { get; set; }
            public string neighborhood { get; set; }
            public string city { get; set; }
            public string state { get; set; }
        }

        public class Paymentmethodpix
        {
            public int fine { get; set; }
            public int interest { get; set; }
            public string instructions { get; set; }
            public Deadline Deadline { get; set; }
        }

        public class Deadline
        {
            public string type { get; set; }
            public int value { get; set; }
        }

    }
}
