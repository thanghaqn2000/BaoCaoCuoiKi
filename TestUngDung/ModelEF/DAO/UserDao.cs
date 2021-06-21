using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class UserDao
    {
        HoDucThangContext db = null;
        public UserDao()
        {
            db = new HoDucThangContext();
        }
        public long Insert(UserAccount entity)
        {
            db.UserAccounts.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool Login(String UserName, string password)
        {
            var res = db.UserAccounts.Count(x => x.UserName == UserName && x.Password == password);
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public UserAccount GetById(string userName)
        {
            return db.UserAccounts.SingleOrDefault(x => x.UserName == userName);
        }

        public IEnumerable<UserAccount> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserAccount> model = db.UserAccounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) ||
                x.UserName.Contains(searchString));
            }
            return model.OrderBy(x => x.Id).ToPagedList(page,
            pageSize);
        }

        public bool Update(UserAccount entity)
        {
            try
            {
                var user = db.UserAccounts.Find(entity.Id);
                user.UserName = entity.UserName;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.UserName = entity.UserName;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public UserAccount ViewDetail(int id)
        {
            return db.UserAccounts.Find(id);
        }
        
        public bool Delete(int id)
        {
            try
            {
                var user = db.UserAccounts.Find(id);
                db.UserAccounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ChangeStatus(long id)
        {
            var user = db.UserAccounts.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
    }
}
