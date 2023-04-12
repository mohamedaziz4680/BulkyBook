using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]

	public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            
            if (ModelState.IsValid) {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Created Successfully";
                return RedirectToAction("Index");
        }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var covertype = _unitOfWork.CoverType.GetFirstOrDefault(ele => ele.Id== id);
            if (covertype == null) { return NotFound(); }
            return View(covertype);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid) {
                _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
                TempData["success"] = "Cover Type Updated Successfully";

                return RedirectToAction("Index");
        }
            return View(obj);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var covertype = _unitOfWork.CoverType.GetFirstOrDefault(ele => ele.Id == id);
            if (covertype == null) { return NotFound(); }
            return View(covertype);
        }

        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var covertype = _unitOfWork.CoverType.GetFirstOrDefault(ele => ele.Id == id);
            if (covertype == null) { return NotFound(); }
            _unitOfWork.CoverType.Remove(covertype);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
