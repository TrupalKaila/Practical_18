using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Practical_17.Models;
using Practical_17.Repositories;
using Practical_17.ViewModels;

namespace Practical_17.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public StudentController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5221/");
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin,NormalUser")]
        public async Task<IActionResult> Index()
        {
            var students = await _httpClient.GetFromJsonAsync<List<Students>>("/api/StudentAPI");
            var studentVMs = _mapper.Map<List<StudentViewModel>>(students);
            return View(studentVMs);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Students student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/StudentAPI", student);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Student created successfully.";
                return RedirectToAction("Index");
            }

            return View(student);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<Students>($"api/StudentAPI/{id}");

            if (student == null)
                return RedirectToAction("Index");

            return View(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Students student)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/StudentAPI/{student.StudentId}", student);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Student updated successfully.";
                return RedirectToAction("Index");
            }

            return View(student);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,NormalUser")]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<Students>($"api/StudentAPI/{id}");

            return View(student);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<Students>($"api/StudentAPI/{id}");

            return View(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"api/StudentAPI/{id}");

            TempData["SuccessMessage"] = "Student deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}
