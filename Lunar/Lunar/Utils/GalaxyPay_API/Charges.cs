namespace Lunar.Utils.GalaxyPay_API
{
    public class Charges
    {
        public string myId { get; set; }
        public int value { get; set; }
        public string additionalInfo { get; set; }
        public string payday { get; set; }
        public bool payedOutsideGalaxPay { get; set; }
        public string mainPaymentMethodId { get; set; }
        public Customer Customer { get; set; }
        public Paymentmethodcreditcard PaymentMethodCreditCard { get; set; }
        public Paymentmethodboleto PaymentMethodBoleto { get; set; }
        public Paymentmethodpix PaymentMethodPix { get; set; }
        public Invoiceconfig InvoiceConfig { get; set; }
        public Extrafield1[] ExtraFields { get; set; }
    }

    public class Customer
    {
       // public string myId { get; set; }
        public int galaxPayId { get; set; }
        public string name { get; set; }
        public string document { get; set; }
        public string[] emails { get; set; }
        public long[] phones { get; set; }
        public bool invoiceHoldIss { get; set; }
        public string municipalDocument { get; set; }
        public string password { get; set; }
        public Address Address { get; set; }
        public Extrafield[] ExtraFields { get; set; }
    }

    public class Address
    {
        public string zipCode { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }

    public class Extrafield
    {
        public string tagName { get; set; }
        public string tagValue { get; set; }
    }

    public class Paymentmethodcreditcard
    {
        public Link Link { get; set; }
        public Antifraud Antifraud { get; set; }
        public Card Card { get; set; }
        public string cardOperatorId { get; set; }
        public bool preAuthorize { get; set; }
        public int qtdInstallments { get; set; }
    }

    public class Link
    {
        public int minInstallment { get; set; }
        public int maxInstallment { get; set; }
    }

    public class Antifraud
    {
        public string ip { get; set; }
        public string sessionId { get; set; }
    }

    public class Card
    {
        public string myId { get; set; }
        public string hash { get; set; }
        public string number { get; set; }
        public string holder { get; set; }
        public string expiresAt { get; set; }
        public string cvv { get; set; }
    }

    public class Paymentmethodboleto
    {
        public int fine { get; set; }
        public int interest { get; set; }
        public string instructions { get; set; }
        public int deadlineDays { get; set; }
        public Discount Discount { get; set; }
    }

    public class Discount
    {
        public int qtdDaysBeforePayDay { get; set; }
        public string type { get; set; }
        public int value { get; set; }
    }

    public class Paymentmethodpix
    {
        public int fine { get; set; }
        public int interest { get; set; }
        public string instructions { get; set; }
        public Deadline Deadline { get; set; }
        public Discount1 Discount { get; set; }
    }

    public class Deadline
    {
        public string type { get; set; }
        public int value { get; set; }
    }

    public class Discount1
    {
        public int qtdDaysBeforePayDay { get; set; }
        public string type { get; set; }
        public int value { get; set; }
    }

    public class Invoiceconfig
    {
        public string description { get; set; }
        public int smu { get; set; }
        public string type { get; set; }
        public string createOn { get; set; }
        public int qtdDaysBeforePayDay { get; set; }
        public int qtdDaysAfterPay { get; set; }
    }

    public class Extrafield1
    {
        public string tagName { get; set; }
        public string tagValue { get; set; }
    }
}
