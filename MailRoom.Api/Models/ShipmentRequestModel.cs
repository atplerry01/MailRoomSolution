using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MailRoom.Api.Models
{
    //[JsonProperty(PropertyName = "ShipmentInfo")]
    public class ShipmentInfo
    {
        public string DropOffType { get; set; }
        public string ServiceType { get; set; }
        public int Account { get; set; }
        public string Currency { get; set; }
        public string UnitOfMeasurement { get; set; }
    }

    public class Commodities
    {
        public int NumberOfPieces { get; set; }
        public string Description { get; set; }
        public string CountryOfManufacture { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int CustomsValue { get; set; }
    }

    public class InternationalDetail
    {
        public Commodities Commodities { get; set; }
        public string Content { get; set; }
    }

    public class Contact
    {
        public string PersonName { get; set; }
        public string CompanyName { get; set; }
        public long PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }

    public class Address
    {
        public string StreetLines { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class Shipper
    {
        public Contact Contact { get; set; }
        public Address Address { get; set; }
    }

    public class Recipient
    {
        public Contact  Contact { get; set; }
        public Address Address { get; set; }
    }

    public class Ship
    {
        public Shipper Shipper { get; set; }
        public Recipient Recipient { get; set; }
    }

    public class Dimensions
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    //[JsonProperty(PropertyName = "Feedback_IM&SR")]
    //[DataMember(Name = "MINIO_ACCESS_KEY")]
    public class RequestedPackage
    {
        [JsonProperty("@number")]
        public string number { get; set; }
        public int Weight { get; set; }
        public Dimensions Dimensions { get; set; }
        public string CustomerReferences { get; set; }
    }

    public class Packages
    {
        public IList<RequestedPackage> RequestedPackages { get; set; }
    }

    public class RequestedShipment
    {
        public ShipmentInfo ShipmentInfo { get; set; }
        public string ShipTimestamp { get; set; }
        public string PaymentInfo { get; set; }
        public InternationalDetail InternationalDetail { get; set; }
        public Ship Ship { get; set; }
        public Packages Packages { get; set; }
    }

    public class ShipmentRequest
    {
        public RequestedShipment RequestedShipment { get; set; }
    }

    public class RootObject
    {
        public ShipmentRequest ShipmentRequest { get; set; }
    }

}