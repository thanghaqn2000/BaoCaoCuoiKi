using ModelEF.DAO;
using ModelEF.Model;
using System.Linq;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.ChuoiTimKiem = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryId = new SelectList(dao.ListAll(), "Id", "Name",
            selectedID);

        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var db = new HoDucThangContext();
                if (db.Products.Any(x => x.Name == product.Name))
                {
                    SetAlert("Product's name already exists", "error");

                }
                else
                {
                    long id = dao.Insert(product);
                    if (id > 0)
                    {
                        SetAlert("Add product success", "success");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        SetAlert("Fail to add product", "error");
                    }
                }

            }
            else
            {
                SetViewBag();
            }
            return View("Create");
        }
        public ActionResult Edit(int id)
        {
            SetViewBag();
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var res = dao.Update(product);
                if (res)
                {
                    SetAlert("Update product success", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Fail to update this product", "error");
                }
            }
            return View("Index");
        }
        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    var product = new ProductDao().Delete(id);
        //    return RedirectToAction("Index");
        //}
        public ActionResult DeleteProduct(int id)
        {
            var product = new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            SetViewBag();
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }

    }
}