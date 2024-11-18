using LearnAnimal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LearnAnimal.Services;

namespace LearnAnimal.Controllers
{
    public class HomeController : Controller
    {
     
        private readonly ClaudeService _claudeService;

        public HomeController( ClaudeService claudeService)
        {
            _claudeService = claudeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AnimalInfo(string animalName)
        {
            if (string.IsNullOrEmpty(animalName))
            {
                ViewBag.Error = "Lütfen bir hayvan ismi girin.";
                return View();
            }

            var info = await _claudeService.GetAnimalInfoAsync(animalName);
            ViewBag.AnimalInfo = info;
            return View();
        }

        [HttpGet]
        public IActionResult AnimalInfo()
        {
            return View();
        }


        // Fotoðraf Yükle ve Analiz Et Sayfasý
        [HttpGet]
        public IActionResult AnalyzeImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AnalyzeImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ViewBag.Error = "Lütfen geçerli bir PNG fotoðraf yükleyin.";
                return View();
            }

            try
            {
                // Görseli base64 formatýna çevir
                string base64Image;
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    base64Image = Convert.ToBase64String(imageBytes);
                }

                // Yapay zeka analizini çaðýr
                var result = await _claudeService.AnimalImage(base64Image);
                ViewBag.AnalysisResult = result;
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Bir hata oluþtu: {ex.Message}";
            }

            return View();
        }


        [HttpGet]
        public IActionResult AnalyzePdf()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AnalyzePdf(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                ViewBag.Error = "Lütfen geçerli bir PDF dosyasý yükleyin.";
                return View();
            }

            try
            {
                // PDF dosyasýný Base64 formatýna çevir
                string base64Pdf;
                using (var memoryStream = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(memoryStream);
                    var pdfBytes = memoryStream.ToArray();
                    base64Pdf = Convert.ToBase64String(pdfBytes);
                }

                // Claude AI'den analiz sonucunu al
                var result = await _claudeService.AnalyzePdfAsync(base64Pdf);
                ViewBag.PdfAnalysisResult = result;
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"PDF analizi sýrasýnda bir hata oluþtu: {ex.Message}";
            }

            return View();
        }



    }
}
