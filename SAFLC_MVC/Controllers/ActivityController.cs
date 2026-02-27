using Microsoft.AspNetCore.Mvc;
using SAFLC_MVC.Applications.DTO.ActivityDTO;
using SAFLC_MVC.Applications.DTO.ClassesDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Interfaces;
using SAFLC_MVC.Services;

namespace SAFLC_MVC.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetActivities(string searchString = "")
        {
            var classesLit = await _activityService.GetFilteredActivity(searchString);
            return PartialView("_ClassesTable", classesLit);
        }

        [HttpGet]
        public async Task<IActionResult> GetClassById(int id)
        {
            var classes = new GetClassesDTO();

            var result = await _activityService.GetById(id);

            return Ok(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateActivity(CreateActivityDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(ResponseHelper.BuildFailure<bool>("Invalid Id provided"));
            }

            var result = await _activityService.CreateActivity(createDto);

            return Json(result);

        }

        [HttpPost]
        public async Task<IActionResult> EditClass(UpdateActivityDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(ResponseHelper.BuildFailure<bool>("Invalid Id provided"));
            }

            var result = await _activityService.UpdateActivity(updateDto);

            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteClass(int id)
        {
            if (id <= 0)
                return Json(ResponseHelper.BuildFailure<bool>("Invalid Id provided"));

            var result = await _activityService.DeleteAsync(id);

            return Json(result);

        }
    }
}
