using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Applications.Model;
using SAFLC_MVC.Interfaces;
using System.Numerics;
using System.Threading.Tasks;

namespace SAFLC_MVC.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchString, int pageSize = 10, int pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var students = await _studentService.GetFilteredStudents(searchString, pageSize, pageNumber);
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentTable(string searchString = "", int pageSize = 10, int pageNumber = 1)
        {
            var students = await _studentService.GetFilteredStudents(searchString, pageSize, pageNumber);
            return PartialView("_StudentTable", students);
        }


        public IActionResult Create() => View("CreateStudent");

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
                studentDTO = _mapper.Map<UpdateStudentDTO>(student);
                //student = _map result.Item;
                return View("EditStudent", studentDTO);

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