using Microsoft.AspNetCore.Mvc;
using SAFLC_MVC.Applications.DTO.ClassesDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Interfaces;

namespace SAFLC_MVC.Controllers
{

    public class ClassesController : Controller
    {
        private readonly IClassesService _classesService;

        public ClassesController(IClassesService classesService)
        {
            _classesService = classesService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetClasses(string searchString = "")
        {
            var classesLit = await _classesService.GetFilteredClass(searchString);
            return PartialView("_ClassesTable", classesLit);
        }

        [HttpGet]
        public async Task<IActionResult> GetClassById(int id)
        {
            var classes = new GetClassesDTO();

            var result = await _classesService.GetById(id);

            return Ok(result); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClass(CreateClassesDTO createDto)
        {
            if (!ModelState.IsValid) {
                return Json(ResponseHelper.BuildFailure<bool>("Invalid Id provided"));
            }

            var result = await _classesService.CreateClass(createDto);

            return Json(result);
                                                                                   
        }

        [HttpPost]
        public async Task<IActionResult> EditClass(UpdateClassesDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return Json(ResponseHelper.BuildFailure<bool>("Invalid Id provided"));
            }

            var result = await _classesService.UpdateClass(dto);

            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteClass(int id)
        {
            if(id <= 0 )
                return Json(ResponseHelper.BuildFailure<bool>("Invalid Id provided"));

            var result = await _classesService.DeleteAsync(id);

            return Json(result);

        }
    }
}
