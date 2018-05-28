using System.Threading.Tasks;
using DinkToPdf;
using MailRoom.Api.Helper;
using MailRoom.Api.Helper.PDF;
using Microsoft.AspNetCore.Mvc;

namespace MailRoom.Api.Controllers
{
    public class GeneratePdfController : Controller
    {
        [HttpGet]
        [Route("/api/pdf")]
        public async Task<IActionResult> GenerateSomeDocumentAsync()
        {
            var path = "C:\\Dev\\VSCode\\MailRoomSolution\\MailRoom.Api\\Utils\\v0.12.4\\64 bit\\libwkhtmltox.dll";

            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(path);

            var converter = new SynchronizedConverter(new PdfTools());
            var doc = converter.Convert(new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                    },
                    Objects = {
                        new ObjectSettings() {
                            PagesCount = true,
                            HtmlContent = "This is my Test PDF...",
                            WebSettings = { DefaultEncoding = "utf-8" },
                            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                        }
                    }
            });

            return File(doc, "application/pdf");
        }

    }
}