using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        DataBaseDataContext db = new DataBaseDataContext();
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Log_out()
        {
            // Xóa tất cả session
            Session.Clear(); // Xóa tất cả các biến session
            Session.Abandon(); // Hủy bỏ phiên làm việc hiện tại
            return RedirectToAction("Index", "Home");
        }
    }
}
