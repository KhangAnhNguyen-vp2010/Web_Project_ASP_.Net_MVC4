using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;
namespace QuanLyBanNoiThat.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        DataBaseDataContext db = new DataBaseDataContext();

        public ActionResult Index()
        {
            var nv = db.NHANVIENs.FirstOrDefault(t => t.MANV == (string)Session["MaNV"]);
            if (nv == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy Tour, trả về lỗi 404
            }
            return View(nv); // Trả về view chi tiết của tour
        }

        public ActionResult LoginAdmin(string taikhoan, string pass)
        {
            if (taikhoan != null || pass != null)
            {
                if (taikhoan.Length == 0 || pass.Length == 0)
                {
                    ViewBag.ThongBaoDN = "Tài Khoản Hoặc Mật Khẩu Không Được Bỏ Trống!";
                }
                else
                {
                    string sql = string.Format("SELECT dbo.KIEMTRA_DANGNHAP_NV(N'{0}',N'{1}')", taikhoan, pass);
                    var dk = db.ExecuteQuery<int>(sql).FirstOrDefault();
                    if (dk == 0)
                    {
                        ViewBag.ThongBaoDN = "Tài Khoản Hoặc Mật Khẩu Không Khớp";
                        return View();
                    }
                    
                    // Tìm user dựa vào tên đăng nhập
                    var user = db._USER_NVs.FirstOrDefault(u => u.TENDANGNHAP == taikhoan);
                    if (user == null)
                    {
                        return HttpNotFound(); // Nếu không tìm thấy user, trả về lỗi 404
                    }

                    Session["NameNV"] = user.TENDANGNHAP;
                    Session["MaNV"] = user.MA_USER;
                    Session["RoleNV"] = user.VAITRO;
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        public ActionResult Logout_admin()
        {
            // Xóa tất cả session
            Session.Clear(); // Xóa tất cả các biến session
            Session.Abandon(); // Hủy bỏ phiên làm việc hiện tại
            return RedirectToAction("Logout_admin", "Admin");
        }

    }
}
