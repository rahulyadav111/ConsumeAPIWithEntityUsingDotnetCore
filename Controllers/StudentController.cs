using DemoConsumingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace DemoConsumingAPI.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient _http;

        
        public StudentController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient();
            _http.BaseAddress = new Uri("https://localhost:7062/api/Student");
        }

       public async Task<IActionResult> Index()
        {
            var data = await _http.GetFromJsonAsync <List<Student>>("");


            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student s)
        {
           var data= await _http.PostAsJsonAsync("",s);
            if(data.IsSuccessStatusCode)
            {
                //HttpContext.Session.SetString("userName", s.Name);  // use for set value in session 


                return RedirectToAction("Index");
            }


            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data =await  _http.GetFromJsonAsync<Student>($"/api/Student/{id}");


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student s)
        {
            var res = await _http.PutAsJsonAsync("/api/Student", s);
            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }


        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = await _http.DeleteAsync($"api/Student/{id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        TempData["Success"] = "Record deleted successfully.";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        TempData["Error"] = $"Failed to delete record. Status Code: {response.StatusCode}";
        //        return RedirectToAction("Index");
        //    }
            //public async Task<IActionResult> Delete(int id)
            //{
            //    var res = await _http.DeleteAsync($"/api/Student/{id}");

            //    return RedirectToAction("Index");

            //}
            // return View("Index");

        //}


        public async Task<IActionResult> Delete(int id)
        {
            var result = await _http.DeleteAsync($"/api/Student/{id}");

            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("Delete");
            }




        }


    
    }
}
