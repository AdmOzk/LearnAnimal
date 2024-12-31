using LearnAnimal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LearnAnimal.Services;

namespace LearnAnimal.Controllers
{
    public class HomeController : Controller
    {
        // Dependency injection
        private readonly ClaudeService _claudeService;

        public HomeController(ClaudeService claudeService)
        {
            _claudeService = claudeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Method to get Animal Information
        [HttpPost]
        public async Task<IActionResult> AnimalInfo(string animalName)
        {
            if (string.IsNullOrEmpty(animalName))
            {
                ViewBag.Error = "Please enter an animal name.";
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

        // Upload and Analyze Image Page
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
                ViewBag.Error = "Please upload a valid PNG image.";
                return View();
            }

            try
            {
                // Convert the image to Base64 format
                string base64Image;
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    base64Image = Convert.ToBase64String(imageBytes);
                }

                // Call AI for analysis
                var result = await _claudeService.AnimalImage(base64Image);
                ViewBag.AnalysisResult = result;
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
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
                ViewBag.Error = "Please upload a valid PDF file.";
                return View();
            }

            try
            {
                // Convert the PDF file to Base64 format
                string base64Pdf;
                using (var memoryStream = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(memoryStream);
                    var pdfBytes = memoryStream.ToArray();
                    base64Pdf = Convert.ToBase64String(pdfBytes);
                }

                // Get analysis result from Claude AI
                var result = await _claudeService.AnalyzePdfAsync(base64Pdf);
                ViewBag.PdfAnalysisResult = result;
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred during PDF analysis: {ex.Message}";
            }

            return View();
        }
    }
}
