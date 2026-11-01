using System;
using System.Linq;
using System.Web.Mvc;
using HotelBoking.Models;
namespace HotelBoking.Controllers
{
    public class SignupController : Controller
    {
        [HttpGet]
        public ActionResult Index() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string hoten, string username, string password, string confirm, string sdt)
        {
            if (password != confirm) ModelState.AddModelError("", "Mật khẩu nhập lại không khớp.");
            using (var db = new HotelBokingEntities())
            {
                if (db.TaiKhoans.Any(x => x.UserName == username))
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                if (!ModelState.IsValid) return View();
                var person = new Person { PersonId = "P" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(), HoTen = hoten, Sdt = sdt };
                db.Persons.Add(person);
                db.KhachHangs.Add(new KhachHang { KhachHangId = person.PersonId });
                db.TaiKhoans.Add(new TaiKhoan
                {
                    MaTaiKhoan = "TK" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                    UserName = username, Password = PasswordHelper.Hash(password),
                    MaLoaiTaiKhoan = 1, PersonId = person.PersonId
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
        }
    }
}