using Microsoft.AspNetCore.Mvc;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.DTO.SubjectDTO;
using SAFLC_MVC.Applications.Model;
using SAFLC_MVC.Interfaces;
using SAFLC_MVC.Services;

namespace SAFLC_MVC.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
            
        public async Task<IActionResult> Index(string searchString, int pageSize = 10, int pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var pagedData = await _subjectService.GetFilteredSubjects(searchString, pageSize, pageNumber);
            return View(pagedData);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjectTable(string searchString = "", int pageSize = 10,  int pageNumber = 1)
        {
            var pagedData = await _subjectService.GetFilteredSubjects(searchString, pageSize, pageNumber);
            return PartialView("_SubjectTable", pagedData);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(CreateSubjectDTO subjectDTO)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please correct the errors in the form.";
                return View(subjectDTO);
            }

            var result = await _subjectService.CreateSubject(subjectDTO);

            return Json(result);
        }

    }
}
