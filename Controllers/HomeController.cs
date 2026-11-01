using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using HotelBoking.Models;
namespace HotelBoking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new HotelBokingEntities())
            {
                return View(db.Phongs.Include("LoaiPhong").Take(6).ToList());
            }
        }
    }
}