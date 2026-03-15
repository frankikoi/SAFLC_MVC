using Microsoft.AspNetCore.Mvc;
using SAFLC_MVC.Applications.DTO.SchoolYearDTO;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Interfaces;
using SAFLC_MVC.Pages.Student;
using SAFLC_MVC.Services;

namespace SAFLC_MVC.Controllers
{

    public class SchoolYearController : Controller
    {

        private readonly ISchoolYearService _schoolYearService;

        public SchoolYearController(ISchoolYearService schoolYearService)
        {
            _schoolYearService = schoolYearService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSchoolYears(int pageSize, string searchString = "")
        {
            var schoolYears = await _schoolYearService.GetFilteredSchoolYears(searchString, pageSize);
            return PartialView("_SchoolYearTable", schoolYears);
        }

        public async Task<IActionResult> GetSchoolYearById(int id)
        {
            var schoolyear = new GetSchoolYearDTO();

            var result = await _schoolYearService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSchoolYear(CreateSchoolYearDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _schoolYearService.CreateSchoolYear(dto);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditSchoolYear(UpdateSchoolYearDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please correct the errors in the form";
                return View(dto);
            }

            var result = await _schoolYearService.UpdateSchoolYear(dto);
            if (result.Success)
            {
                TempData["success"] = result.Message ?? "School Year details updated";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = result.Message ?? "Failed to update school year.";

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSchoolYear(int id)
        {
            if (id <= 0)
                return Json(ResponseHelper.BuildFailure<bool>("Invalid Id provided"));
            
            var result = await _schoolYearService.DeleteAsync(id);

            return Json(result);
        }

    }
}
