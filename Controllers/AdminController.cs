using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using HotelBoking.Models;
namespace HotelBoking.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["LoaiTaiKhoan"] == null || (int)Session["LoaiTaiKhoan"] != 2)
            {
                filterContext.Result = RedirectToAction("Index", "Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        public ActionResult Index()
        {
            using (var db = new HotelBokingEntities())
            {
                ViewBag.SoPhong = db.Phongs.Count();
                ViewBag.SoDon = db.OrderPhongs.Count();
                ViewBag.SoKhach = db.KhachHangs.Count();
                return View(db.OrderPhongs.Include("Phong").Include("Person").OrderByDescending(x => x.NgayDen).Take(20).ToList());
            }
        }
        public ActionResult Rooms()
        {
            using (var db = new HotelBokingEntities()) return View(db.Phongs.Include("LoaiPhong").ToList());
        }
    }
}