using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;
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
            var students = await GetFilteredStudents(searchString);
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentTable(string searchString = "")
        {
            var students = await GetFilteredStudents(searchString);
            return PartialView("_StudentTable", students);
        }

        // Private helper to keep logic identical in both places
        private async Task<List<GetStudentDTO>> GetFilteredStudents(string searchString)
        {
            var result = await _studentService.GetAll();
            var students = result.Item ?? new List<GetStudentDTO>();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                students = students.Where(s =>
                    (s.FirstName?.Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.LastName?.Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.StudentNo?.Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();
            }
            return students;
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
        public async Task<IActionResult> EditStudentById(int id)
        {
            var studentDTO = new UpdateStudentDTO();

            var result = await _studentService.GetById(id);
             
            if (result.Success)
            {
                var student = result.Item;

                //student = _map result.Item;
                return View("EditStudent", student);

            }
            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> CreateStudent(CreateStudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please correct the errors in the form.";
                return View(studentDTO);
            }

            var result = await _studentService.CreateStudent(studentDTO);

            if (result.Success)
            {
                TempData["success"] = result.Message ?? "Student enrolled successfully!";
                return RedirectToAction(nameof(Index)); 
            }

            TempData["error"] = result.Message ?? "Failed to enroll student.";
            return View(studentDTO); 
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(UpdateStudentDTO studentDTO)
        {
            //Handle Error
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please correct the errors in the form.";
                return View(studentDTO);
            }

            var result = await _studentService.UpdateStudent(studentDTO);
            if (result.Success)
            {
                TempData["success"] = result.Message ?? "Student details updated.";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = result.Message ?? "Failed to update student.";
            return View(studentDTO);
        }

        [HttpPost] 
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return Json(ResponseHelper.BuildFailure<bool>("Invalid ID provided."));

            // Call the Service logic
            var result = await _studentService.DeleteAsync(id);

            // Return the result as JSON for the AJAX call to handle
            return Json(result);
        }
    }
}