using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class ProductDao
    {
        HoDucThangContext db = null;
        public ProductDao()
        {
            db = new HoDucThangContext();
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public Product GetById(string tenSanPham)
        {
            return db.Products.SingleOrDefault(x => x.Name == tenSanPham);
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) ||
                x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.Quantity).ThenByDescending(x => x.UniCost).ToPagedList(page,
            pageSize);
        }


        public bool Update(Product entity)
        {
            try
            {
                var sanPham = db.Products.Find(entity.Id);
                sanPham.Name = entity.Name;
                sanPham.Image = entity.Image;
                sanPham.CategoryId = entity.CategoryId;
                sanPham.Category = entity.Category;
                sanPham.UniCost = entity.UniCost;
                sanPham.Description = entity.Description;
                sanPham.Status = entity.Status;
                sanPham.Quantity = entity.Quantity;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<Product> ListAll()
        {
            IQueryable<Product> model = db.Products;
            return model.OrderBy(x => x.Quantity).ThenByDescending(x => x.UniCost).ToList();
        }
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var sanPham = db.Products.Find(id);
                db.Products.Remove(sanPham);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
