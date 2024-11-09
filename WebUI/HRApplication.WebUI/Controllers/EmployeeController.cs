using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using HRApplication.WebUI.Models;
using Newtonsoft.Json;
using System.Text;

namespace HRApplication.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7051/api/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: EmployeeController
        public async Task<ActionResult> IndexAsync()
        {
            string endpoint = "Employee";

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync());
                return View(data);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string endpoint = "Employee/" + id;

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<Employee>(await response.Content.ReadAsStringAsync());
                return View(data);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: EmployeeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormFile file)
        {
            if (file != null && Path.GetExtension(file.FileName).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    List<Employee> employees = ParseXmlFile(file);
                    foreach (Employee employee in employees)
                    {
                        var jsonContent = JsonConvert.SerializeObject(employee);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await _httpClient.PostAsync("Employee", content);
                        
                    }

                    return RedirectToAction(nameof(IndexAsync));
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error parsing file: " + ex.Message;
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please upload a valid XML file.";
            }

            return View();
        }

        private List<Employee> ParseXmlFile(IFormFile file)
        {
            List<Employee> employees = new List<Employee>();

            // Load the XML from the uploaded file
            using (var stream = file.OpenReadStream())
            {
                XDocument xDoc = XDocument.Load(stream);

                employees = (from employee in xDoc.Descendants("Employee")
                             select new Employee
                             {
                                 FirstName = employee.Element("FirstName")?.Value,
                                 LastName = employee.Element("LastName")?.Value,
                                 Division = employee.Element("Division")?.Value,
                                 Building = employee.Element("Building")?.Value,
                                 Title = employee.Element("Title")?.Value,
                                 Room = employee.Element("Room")?.Value
                             }).ToList();
            }

            return employees;
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string endpoint = "Employee/" + id;

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<Employee>(await response.Content.ReadAsStringAsync());
                return View(data);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Employee employee)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(employee);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync("Employee", content);

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            string endpoint = "Employee/" + id;

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<Employee>(await response.Content.ReadAsStringAsync());
                return View(data);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Employee employee)
        {
            try
            {
                string endpoint = "Employee/" + employee.Id;

                HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint);

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
