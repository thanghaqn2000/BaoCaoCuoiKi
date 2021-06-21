using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace ModelEF.DAO
{
    public class CategoryDao
    {
        HoDucThangContext db = null;
        public CategoryDao()
        {
            db = new HoDucThangContext();
        }
        public List<Category> ListAll()
        {
            return db.Categories.ToList();
            //return db.Categories.Where(x => x.Desciption == "đang cập nhật").ToList();
        }
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public Category GetById(string tenDanhMuc)
        {
            return db.Categories.SingleOrDefault(x => x.Name == tenDanhMuc);
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) ||
                x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Name).ToPagedList(page,
            pageSize);
        }

        public bool Update(Category entity)
        {
            try
            {
                var danhMuc = db.Categories.Find(entity.Id);
                danhMuc.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Desciption))
                {
                    danhMuc.Desciption = entity.Desciption;
                }
                danhMuc.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var danhMuc = db.Categories.Find(id);
                db.Categories.Remove(danhMuc);
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
