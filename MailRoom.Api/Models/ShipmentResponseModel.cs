using System.Collections.Generic;
using Newtonsoft.Json;

namespace MailRoom.Api.Models
{
    public class Notification
    {
        [JsonProperty("@code")]
        public string @code { get; set; }
        public object Message { get; set; }
    }

    public class PackageResult
    {
        [JsonProperty("@number")]
        public string @number { get; set; }
        public string TrackingNumber { get; set; }
    }

    public class PackagesResult
    {
        public IList<PackageResult> PackageResult { get; set; }
    }

    public class LabelImage
    {
        public string LabelImageFormat { get; set; }
        public string GraphicImage { get; set; }
    }

    public class ShipmentResponse
    {
        public IList<Notification> Notification { get; set; }
        public PackagesResult PackagesResult { get; set; }
        public IList<LabelImage> LabelImage { get; set; }
        public int ShipmentIdentificationNumber { get; set; }
    }

    public class Example
    {
        public ShipmentResponse ShipmentResponse { get; set; }
    }

}