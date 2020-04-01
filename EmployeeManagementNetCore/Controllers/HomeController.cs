using Microsoft.AspNetCore.Mvc;
using EmployeeManagementNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementNetCore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EmployeeManagementNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "EmployeeDetails"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedPhoto(model);

                Employee employee = new Employee
                {
                    Name = model.Name,
                    Department = model.Department,
                    Email = model.Email,
                    PhotoPath = uniqueFileName
                };

                Employee emp = _employeeRepository.Add(employee);
                return RedirectToAction("Details", new { id = emp.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);

            EmployeeEditViewModel model = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    Id = model.Id,
                    Name = model.Name,
                    Department = model.Department,
                    Email = model.Email
                };

                if (model.Photo != null)
                {
                    employee.PhotoPath = ProcessUploadedPhoto(model);
                }

                Employee emp = _employeeRepository.Update(employee);
                return RedirectToAction("Index");
            }

            return View();
        }

        private string ProcessUploadedPhoto(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileName = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileName);
                }
            }

            return uniqueFileName;
        }
    }
}
