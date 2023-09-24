namespace Lunar.Utils.GalaxyPay_API
{
    public class RetornoBoletoGerado 
    { 
        public class RetBoleto
        {
            public bool type { get; set; }
            public Charge Charge { get; set; }
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
            public Transaction[] Transactions { get; set; }
            public object[] ExtraFields { get; set; }
            public Paymentmethodboleto PaymentMethodBoleto { get; set; }
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
            public object[] phones { get; set; }
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

        public class Paymentmethodboleto
        {
            public int fine { get; set; }
            public int interest { get; set; }
            public string instructions { get; set; }
            public int deadlineDays { get; set; }
        }

        public class Transaction
        {
            public int galaxPayId { get; set; }
            public int value { get; set; }
            public string payday { get; set; }
            public object paydayDate { get; set; }
            public int installment { get; set; }
            public string status { get; set; }
            public string statusDescription { get; set; }
            public string createdAt { get; set; }
            public int chargeGalaxPayId { get; set; }
            public string chargeMyId { get; set; }
            public object[] ConciliationOccurrences { get; set; }
            public Boleto Boleto { get; set; }
            public Antifraud Antifraud { get; set; }
        }

        public class Boleto
        {
            public string pdf { get; set; }
            public string bankLine { get; set; }
            public int bankNumber { get; set; }
            public string barCode { get; set; }
            public string bankEmissor { get; set; }
            public string bankAgency { get; set; }
            public string bankAccount { get; set; }
        }

        public class Antifraud
        {
            public object ip { get; set; }
            public object sessionId { get; set; }
            public bool sent { get; set; }
            public object approved { get; set; }
        }
    }
}
