using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MailRoom.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MailRoom.Api.Controllers
{
    [Route("api/test")]
    public class Test1Controller : Controller
    {
        private readonly HttpClient client;

        public Test1Controller(HttpClient client)
        {
            this.client = client;

        }

        [HttpPost]
        public async Task<IActionResult> Create(string city)
        {

            RootObject rootObject = new RootObject()
            {
                ShipmentRequest = new ShipmentRequest()
                {
                    RequestedShipment = new RequestedShipment()
                    {
                        ShipmentInfo = new ShipmentInfo()
                        {
                            DropOffType = "REGULAR_PICKUP",
                            ServiceType = "N",
                            Account = 365080899,
                            Currency = "NGN",
                            UnitOfMeasurement = "SI"
                        },
                        ShipTimestamp = "2018-06-12T17:10:09 GMT+01:00", //DateTime.Now.ToString(),
                        PaymentInfo = "DAP",
                        InternationalDetail = new InternationalDetail()
                        {
                            Commodities = new Commodities()
                            {
                                NumberOfPieces = 1,
                                Description = "Customer Reference 1",
                                CountryOfManufacture = "CN",
                                Quantity = 1,
                                UnitPrice = 5,
                                CustomsValue = 10
                            },
                            Content = "DOCUMENTS"
                        },
                        Ship = new Ship()
                        {
                            Shipper = new Shipper()
                            {
                                Contact = new Contact()
                                {
                                    PersonName = "Tester 1",
                                    CompanyName = "DHL",
                                    PhoneNumber = 2175441239,
                                    EmailAddress = "jb@acme.com"
                                },
                                Address = new Address()
                                {
                                    StreetLines = "#05-33 Singapore Post Centre",
                                    City = "LAGOS",
                                    PostalCode = "",
                                    CountryCode = "NG"
                                }
                            },
                            Recipient = new Recipient()
                            {
                                Contact = new Contact()
                                {
                                    PersonName = "Tester 1",
                                    CompanyName = "DHL",
                                    PhoneNumber = 2175441239,
                                    EmailAddress = "jb@acme.com"
                                },
                                Address = new Address()
                                {
                                    StreetLines = "#05-33 Singapore Post Centre",
                                    City = "LAGOS",
                                    PostalCode = "",
                                    CountryCode = "NG"
                                }
                            }
                        },
                        Packages = new Packages()
                        {
                            RequestedPackages = new RequestedPackage[]
                            {
                                new RequestedPackage() {
                                    number = "1",
                                    Weight = 1,
                                    Dimensions = new Dimensions() {
                                        Length = 1,
                                        Width = 2,
                                        Height = 3
                                    },
                                    CustomerReferences = "Pieces 2"
                                 }
                            }
                        }
                    }
                }
            };

            string returnData;
            string postData = JsonConvert.SerializeObject(rootObject);
            client.DefaultRequestHeaders.Accept.Clear();

            string _ContentType = "application/json";
            var dataAsString = JsonConvert.SerializeObject(rootObject);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);

            HttpResponseMessage response = await client.PostAsync("rest/sndpt/ShipmentRequest", content);

            if (response.IsSuccessStatusCode)
            {
                using (HttpContent cont = response.Content)
                {
                    Task<string> result = cont.ReadAsStringAsync();
                    returnData = result.Result;

                    // process the json to get the waybill
                    var waybill = JsonConvert.DeserializeObject<RootResponse>(returnData);
                    var newWayBill = waybill.ShipmentResponse.ShipmentIdentificationNumber;
                    var trackerNumber = waybill.ShipmentResponse.PackagesResult.PackageResult[0].TrackingNumber;
                    ReBuildModel(newWayBill, trackerNumber);
                    return Ok(waybill);
                }
            }

            return Ok();
        }

        public void ReBuildModel(int waybill, string tracker) {
            Console.WriteLine(tracker);
            Console.WriteLine(waybill);
        }
    }
}