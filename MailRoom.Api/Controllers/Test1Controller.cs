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

        static HttpClient client = new HttpClient();


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

            string postData = JsonConvert.SerializeObject(rootObject);

            client.BaseAddress = new Uri("https://wsbexpress.dhl.com/");
            client.DefaultRequestHeaders.Accept.Clear();

            string _ContentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "secureidl", "T!9tS$4uB!6z"))));

            // try
            // {


            // } catch(Exception ex) {
            //     Console.WriteLine(ex.Message);
            //     return Ok(ex);
            // }

            // string newData = JsonConvert.SerializeObject(rootObject); 

            var dataAsString = JsonConvert.SerializeObject(rootObject);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);
            HttpResponseMessage response = await client.PostAsync("rest/sndpt/ShipmentRequest", content);

            HttpContent _Body = new StringContent(dataAsString);
            _Body.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);
            HttpResponseMessage response2 = client.PostAsync("rest/sndpt/ShipmentRequest", _Body).Result;

            string res;

            if (response.IsSuccessStatusCode)
            {

                using (HttpContent cont = response2.Content)
                {
                    Task<string> result = cont.ReadAsStringAsync();
                    res = result.Result;
                    Console.WriteLine(res);
                    return Ok(res);
                }

            }


            return Ok();
        }

        // public async Task<Uri> CreateProductAsync(RootObject rootObject)
        // {
        //     var dataAsString = JsonConvert.SerializeObject(rootObject);
        //     var content = new StringContent(dataAsString);
        //     HttpResponseMessage response = await client.PostAsync(
        //         "rest/sndpt/ShipmentRequest", content);
        //     response.EnsureSuccessStatusCode();

        //     // return URI of the created resource.
        //     return response.Headers.Location;
        // }



    }

}