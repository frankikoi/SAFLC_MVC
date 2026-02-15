using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Interfaces;
using System.Threading.Tasks;

namespace SAFLC_MVC.Controllers
{
    public class StudentController : Controller
    {
        // 1. Change List<string> to List<Student>
        private static List<Student> Students = new List<Student>
        {
            new Student { Id = 1, StudentNo = "2026-0001", FirstName = "Juan", LastName = "Dela Cruz", Gender = "Male", Status = true, ContactNumber = "09123456789" },
            new Student { Id = 2, StudentNo = "2026-0002", FirstName = "Maria", LastName = "Clara", Gender = "Female", Status = true, ContactNumber = "09987654321" }
        };

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var result = await _studentService.GetAll();

            if (!result.Success || result.Item == null)
            {
                TempData["error"] = result.Message ?? "Could not retrieve students.";
                return View(new List<GetStudentDTO>()); 
            }

            var students = result.Item;

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();
                students = students.Where(s =>
                    (s.FirstName?.ToLower().Contains(searchString) ?? false) ||
                    (s.LastName?.ToLower().Contains(searchString) ?? false) ||
                    (s.StudentNo?.ToLower().Contains(searchString) ?? false)
                ).ToList();
            }

            return View(students);
        }

        public IActionResult Create() => View("CreateStudent");

        // GET: Student/Edit/1
        public IActionResult Edit(int id)
        {

            //var student = _studentService.GetById(id);
            var student = Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = new GetStudentDTO();

            var result = await _studentService.GetById(id);

            if (result.Success)
            {
                student = result.Item;
                return View("EditStudent", student);

            }
            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> CreateStudent(CreateStudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please correct the errors in the form.";
                return View(student);
            }

            var result = await _studentService.CreateStudent(student);

            if (result.Success)
            {
                TempData["success"] = result.Message ?? "Student enrolled successfully!";
                return RedirectToAction(nameof(Index)); 
            }

            TempData["error"] = result.Message ?? "Failed to enroll student.";
            return View(student); 
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            //Handle Error
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please correct the errors in the form.";
                return View(student);
            }

            var index = Students.FindIndex(s => s.Id == student.Id);
            if (index != -1)
            {
                Students[index] = student;
                TempData["success"] = "Student details updated.";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Students.RemoveAll(s => s.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }
}