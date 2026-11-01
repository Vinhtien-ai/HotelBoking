using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using HotelBoking.Models;
namespace HotelBoking.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string username, string password)
        {
            using (var db = new HotelBokingEntities())
            {
                var hash = PasswordHelper.Hash(password);
                var account = db.TaiKhoans.FirstOrDefault(x => x.UserName == username && x.Password == hash);
                if (account == null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                    return View();
                }
                Session["MaTaiKhoan"] = account.MaTaiKhoan;
                Session["UserName"] = account.UserName;
                Session["LoaiTaiKhoan"] = account.MaLoaiTaiKhoan;
                Session["PersonId"] = account.PersonId;
                if (account.MaLoaiTaiKhoan == 2) return RedirectToAction("Index", "Admin");
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}