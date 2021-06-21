using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.ChuoiTimKiem = searchString;
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var MhMd5 = MaHoaMd5.MD5Hash(user.Password);
                user.Password = MhMd5;
                var db = new HoDucThangContext();
                if (db.UserAccounts.Any(x => x.UserName == user.UserName))
                {
                    SetAlert("User accunt already exists", "error");
                }
                else
                {
                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        SetAlert("Add User success", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        SetAlert("Fail to add user", "error");
                    }
                }
            }
            return View("Create");
        }
        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    HoDucThangContext db = new HoDucThangContext();
        //    var user = db.UserAccounts.Find(id);
        //    var dao = new UserDao();
        //    if (user.Status == true)
        //    {
        //        SetAlert("Không thể xóa , người dùng đang ở trạng thái Active", "warning");
        //    }
        //    else
        //    {
        //        var row = dao.Delete(id);
        //        if (row)
        //        {
        //            SetAlert("Xóa User thành công", "success");
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Thêm user không thành công.");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        public ActionResult DeleteUser(int id)
        {
            HoDucThangContext db = new HoDucThangContext();
            var user = db.UserAccounts.Find(id);
            var dao = new UserDao();
            if (user.Status == true)
            {
                SetAlert("Không thể xóa , người dùng đang ở trạng thái Active", "warning");
            }
            else
            {
                var row = dao.Delete(id);
                if (row)
                {
                    SetAlert("Xóa User thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công.");
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
             
        }
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var res = dao.Update(user);
                if (res)
                {
                    SetAlert("Update user success", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Fail to update user", "error");
                }
            }
            return View("Index");
        }
    }
}
