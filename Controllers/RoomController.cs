using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using HotelBoking.Models;
namespace HotelBoking.Controllers
{
    public class RoomController : Controller
    {
        public ActionResult Index(string loaiphong)
        {
            using (var db = new HotelBokingEntities())
            {
                var rooms = db.Phongs.Include("LoaiPhong").Include("TrangThaiPhong").AsQueryable();
                if (!string.IsNullOrEmpty(loaiphong)) rooms = rooms.Where(x => x.MaLoaiPhong == loaiphong);
                ViewBag.LoaiPhongs = db.LoaiPhongs.ToList();
                return View(rooms.ToList());
            }
        }
        public ActionResult Details(string id)
        {
            using (var db = new HotelBokingEntities())
            {
                var room = db.Phongs.Include("LoaiPhong").Include("TrangThaiPhong").FirstOrDefault(x => x.MaPhong == id);
                if (room == null) return HttpNotFound();
                return View(room);
            }
        }
    }
}