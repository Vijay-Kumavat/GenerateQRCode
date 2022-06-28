using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace GenerateQRCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string inputText)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                QRCodeGenerator qRCode = new QRCodeGenerator(); // Generate QR code package
                QRCodeData codeData = qRCode.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q); // Set the QR code version
                QRCode qrCode = new QRCode(codeData); // Initiallze the QRCode object for create graphic
                using (Bitmap oBitmap = qrCode.GetGraphic(20)) // show the QR code with png format
                {
                    oBitmap.Save(memory, ImageFormat.Png); 
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(memory.ToArray());
                }
            }
            return View();
        }
    }
}