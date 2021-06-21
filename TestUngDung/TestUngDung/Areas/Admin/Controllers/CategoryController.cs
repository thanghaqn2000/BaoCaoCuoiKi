using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.ChuoiTimKiem = searchString;           
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var db = new HoDucThangContext();
                if (db.Categories.Any(x => x.Name == category.Name))
                {
                    SetAlert("Category name already exists", "error");
                }
                else
                {
                    long id = dao.Insert(category);
                    if (id > 0)
                    {
                        SetAlert("Add success category", "success");
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        SetAlert("fail to add category", "error");
                    }
                }

            }
            return View("Create");
        }
        public ActionResult Edit(int id)
        {           
            var category = new CategoryDao().ViewDetail(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var res = dao.Update(category);
                if (res)
                {
                    SetAlert("Update category success", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Fail to update this category", "error");
                }
            }
            return View("Index");
        }
        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    var category = new CategoryDao().Delete(id);
        //    return RedirectToAction("Index");
        //}
        public ActionResult DeleteCategory(int id)
        {
            var category = new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }

    }
}