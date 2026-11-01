using System.Data.Entity;
using System;
using System.Linq;
using System.Web.Mvc;
using HotelBoking.Models;
namespace HotelBoking.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet]
        public ActionResult Index(string id)
        {
            if (Session["PersonId"] == null) return RedirectToAction("Index", "Login");
            using (var db = new HotelBokingEntities())
            {
                var room = db.Phongs.Include("LoaiPhong").FirstOrDefault(x => x.MaPhong == id);
                if (room == null) return HttpNotFound();
                return View(room);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string maPhong, DateTime ngayDen, DateTime ngayDi, int soKhach)
        {
            if (Session["PersonId"] == null) return RedirectToAction("Index", "Login");
            if (ngayDi <= ngayDen) return RedirectToAction("Index", new { id = maPhong });
            using (var db = new HotelBokingEntities())
            {
                var room = db.Phongs.FirstOrDefault(x => x.MaPhong == maPhong);
                if (room == null) return HttpNotFound();
                var overlap = db.OrderPhongs.Any(x => x.MaPhong == maPhong && x.TrangThaiThanhToan != 3 &&
                    x.NgayDen < ngayDi && x.NgayDi > ngayDen);
                if (overlap) return Content("Phòng đã được đặt trong khoảng thời gian này.");
                var nights = (ngayDi - ngayDen).Days;
                var price = room.GiaPhong ?? room.LoaiPhong.GiaPhong;
                db.OrderPhongs.Add(new OrderPhong
                {
                    MaOrderPhong = "OD" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                    MaPhong = maPhong, PersonId = (string)Session["PersonId"],
                    NgayDen = ngayDen, NgayDi = ngayDi, SoKhach = soKhach,
                    TrangThaiThanhToan = 0, TongTien = price * nights
                });
                db.SaveChanges();
                return RedirectToAction("History");
            }
        }
        public ActionResult History()
        {
            if (Session["PersonId"] == null) return RedirectToAction("Index", "Login");
            using (var db = new HotelBokingEntities())
                return View(db.OrderPhongs.Include("Phong").Where(x => x.PersonId == (string)Session["PersonId"]).OrderByDescending(x => x.NgayDen).ToList());
        }
    }
}