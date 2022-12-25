using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/pdf")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdf _IPdf;
        private IConverter _converter;

        public PdfController(IPdf IPdf, IConverter converter)
        {
            _converter = converter;
            _IPdf = IPdf;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<string>>> Get(int id)
        {
            var response = new ResponseModel<string>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                string htmlCode = _IPdf.GetHTMLString(id);
                if (string.IsNullOrEmpty(htmlCode))
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Veri bulunamadı.");
                    return response;
                }
                response.body = htmlCode;
                return response;
                //var globalSettings = new GlobalSettings
                //{
                //    ColorMode = ColorMode.Color,
                //    Orientation = Orientation.Portrait,
                //    PaperSize = PaperKind.A4,
                //    Margins = new MarginSettings { Top = 10 },
                //    DocumentTitle = "PDF Report",
                //    Out = @"C:\Users\Administrator\Desktop\Null\source_fitim\Employee_Report.pdf"
                //};
                //var objectSettings = new ObjectSettings
                //{
                //    PagesCount = true,
                //    HtmlContent = _IPdf.GetHTMLString(id),
                //    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                //    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                //    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                //};
                //var pdf = new HtmlToPdfDocument()
                //{
                //    GlobalSettings = globalSettings,
                //    Objects = { objectSettings }
                //};
                // _converter.Convert(pdf);
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }



        }


    }
}
