using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using MailRoom.Api.Models;
using MailRoom.Api.Persistence;
using MailRoom.Api.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MailRoom.Api.Controllers
{
    [Route("api/airwaybill")]
    public class AirWayBillController : Controller
    {
        private readonly HttpClient client;
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment host;
        private readonly IMapper mapper;

        public AirWayBillController(HttpClient client, ApplicationDbContext context, IHostingEnvironment host, IMapper mapper)
        {
            this.mapper = mapper;
            this.host = host;
            this.context = context;
            this.client = client;
        }

        [HttpPost("{jobManifestId}")]
        public async Task<IActionResult> CreateAWB(int jobManifestId)
        {
            var returnMasterAWB = await CreateMasterAWB(jobManifestId);

            // BranchManifest
            var branchManifest = context.JobManifestBranches.Where(m => m.JobManifestId == jobManifestId);

            foreach (JobManifestBranch branch in branchManifest)
            {
                var returnBranchAWB = await CreateBranchAWB(branch.Id);
            }

            var masterManifest = await context.JobManifests
                  .Include(m => m.JobManifestBranchs)
                    .ThenInclude(c => c.ClientBranch)
                  .Include(n => n.JobManifestBranchs)
                    .ThenInclude(d => d.JobManifestLogs)                    
                  .SingleOrDefaultAsync(m => m.Id == jobManifestId);
                  
            var jobManifestResource = mapper.Map<JobManifest, JobManifestResource>(masterManifest);

            return Ok(jobManifestResource);
        }

        public async Task<IActionResult> CreateMasterAWB(int jobManifestId)
        {

            var today = DateTime.Now; //2018-06-13T17:10:09 GMT+01:00
            var shipTime = today.Year.ToString() + "-" + today.Month.ToString() + "-" + today.Day.ToString() + "T17:12:00" + " GMT+01:00";
            var masterManifest = await context.JobManifests.SingleOrDefaultAsync(m => m.Id == jobManifestId);

            if (masterManifest == null) return NotFound();

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
                        ShipTimestamp = shipTime, //utc_offset.ToString(), //"2018-06-13T17:10:09 GMT+01:00",
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
                    var labelImage = waybill.ShipmentResponse.LabelImage[0].GraphicImage;

                    AirWayBillResource awb = new AirWayBillResource()
                    {
                        AirWayBillNumber = newWayBill.ToString(),
                        TrackingNumber = trackerNumber,
                        LabelImage = labelImage
                    };

                    masterManifest.TrackingNumber = trackerNumber;
                    masterManifest.WayBillNumber = newWayBill.ToString();
                    masterManifest.GraphicImage = labelImage.ToString();

                    context.JobManifests.Attach(masterManifest);
                    context.Entry(masterManifest).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    return Ok(awb);
                }
            }

            return Ok();
        }

        public async Task<IActionResult> CreateBranchAWB(int branchId)
        {
            var today = DateTime.Now; //2018-06-13T17:10:09 GMT+01:00
            var shipTime = today.Year.ToString() + "-" + today.Month.ToString() + "-" + today.Day.ToString() + "T17:12:00" + " GMT+01:00";

            var branchManifest = await context.JobManifestBranches.SingleOrDefaultAsync(m => m.Id == branchId);

            if (branchManifest == null) return NotFound();

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
                        ShipTimestamp = shipTime, //"2018-06-13T17:10:09 GMT+01:00", //DateTime.Now.ToString(),
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
                    var labelImage = waybill.ShipmentResponse.LabelImage[0].GraphicImage;

                    AirWayBillResource awb = new AirWayBillResource()
                    {
                        AirWayBillNumber = newWayBill.ToString(),
                        TrackingNumber = trackerNumber,
                        LabelImage = labelImage
                    };

                    branchManifest.TrackingNumber = trackerNumber;
                    branchManifest.WayBillNumber = newWayBill.ToString();

                    context.JobManifestBranches.Attach(branchManifest);
                    context.Entry(branchManifest).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    return Ok(awb);
                }
            }

            return null;
        }

    }
}