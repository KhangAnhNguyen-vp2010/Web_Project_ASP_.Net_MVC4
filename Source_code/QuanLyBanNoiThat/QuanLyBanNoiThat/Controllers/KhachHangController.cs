using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class KhachHangController : Controller
    {
        //
        // GET: /KhachHang/

        DataBaseDataContext db = new DataBaseDataContext();

        //
        public ActionResult DetailsKH_TheoTenTK(string tentk)
        {
            // Tìm user dựa vào tên đăng nhập
            var user = db._USER_KHs.FirstOrDefault(u => u.TENDANGNHAP == tentk);
            if (user == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy user, trả về lỗi 404
            }

            // Tìm khách hàng dựa vào MAKH từ bảng _USER_
            var kh = db.KHACHHANGs.FirstOrDefault(k => k.MAKH == user.MA_USER);
            if (kh == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy khách hàng, trả về lỗi 404
            }

            return View(kh); // Trả về view hiển thị thông tin khách hàng
        }

        // Edit
        [HttpGet]
        public ActionResult EditKH(string id)
        {
            
            KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(t => t.MAKH == id);
            if (kh == null)
            {
                return HttpNotFound();
            }


            return View(kh);
        }

        [HttpPost]
        public ActionResult EditKH(KHACHHANG updatedKH)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(t => t.MAKH == updatedKH.MAKH);
                    if (kh != null)
                    {
                        // Cập nhật thông tin
                        kh.TENKH = updatedKH.TENKH;
                        kh.DIACHI = updatedKH.DIACHI;
                        kh.EMAIL = updatedKH.EMAIL;
                        kh.SDT = updatedKH.SDT;
                        kh.HINHANH = updatedKH.HINHANH;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        return RedirectToAction("DetailsKH_TheoTenTK", "KhachHang", new { tentk = Session["NameKH"] });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                }
            }


            return View(updatedKH);
        }

        // show ad
        public ActionResult ShowKH_ForAdmin()
        {
            return View(db.KHACHHANGs.ToList());
        }

        // Details ad
        public ActionResult DetailsKH_Admin(string id)
        {
            var tl = db.KHACHHANGs.FirstOrDefault(t => t.MAKH == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl);
        }

        // Create ad
        [HttpGet]
        public ActionResult CreateKH_Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateKH_Admin(KHACHHANG newtl)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.KHACHHANGs.InsertOnSubmit(newtl);
                    db.SubmitChanges();



                    ViewBag.ThongBao = "Thêm Thành Công";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi thêm: " + ex.Message);
                }
            }
            return View(newtl);
        }

        // Edit ad
        [HttpGet]
        public ActionResult EditKH_Admin(string id)
        {

            KHACHHANG tl = db.KHACHHANGs.FirstOrDefault(t => t.MAKH == id);
            if (tl == null)
            {
                return HttpNotFound();
            }


            return View(tl);
        }

        [HttpPost]
        public ActionResult EditKH_Admin(KHACHHANG updatedTL)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    KHACHHANG tl = db.KHACHHANGs.FirstOrDefault(t => t.MAKH == updatedTL.MAKH);
                    if (tl != null)
                    {
                        // Cập nhật thông tin
                        tl.TENKH = updatedTL.TENKH;
                        tl.DIACHI = updatedTL.DIACHI;
                        tl.EMAIL = updatedTL.EMAIL;
                        tl.SDT = updatedTL.SDT;
                        tl.HINHANH = updatedTL.HINHANH;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        // Thông báo thành công
                        TempData["SuccessMessage"] = "Cập Nhật Thành Công!";
                        return RedirectToAction("ShowKH_ForAdmin", "KhachHang");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                }
            }


            return View(updatedTL);
        }

        // Delete ad
        public ActionResult DeleteKH_Admin(string id)
        {

            var tl = db.KHACHHANGs.FirstOrDefault(t => t.MAKH == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl); // Hiển thị xác nhận xóa
        }

        [HttpPost, ActionName("DeleteKH_Admin")]
        public ActionResult DeleteConfirmed(string id)
        {

            var tl = db.KHACHHANGs.FirstOrDefault(t => t.MAKH == id);
            if (tl == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.KHACHHANGs.DeleteOnSubmit(tl);
                db.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                TempData["SuccessMessage"] = "Xoá Thành Công!";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // Kiểm tra mã lỗi liên quan đến ràng buộc khóa ngoại (FK)
                if (ex.Number == 547) // 547 là mã lỗi cho vi phạm khóa ngoại
                {
                    TempData["ErrorMessage"] = "Không thể xóa vì đang được sử dụng ở bảng khác.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình xóa: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi: " + ex.Message;
            }
            return RedirectToAction("ShowKH_ForAdmin", "KhachHang");
        }
    }
}
