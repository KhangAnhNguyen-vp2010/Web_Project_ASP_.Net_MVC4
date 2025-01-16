using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class ChiTietHDController : Controller
    {
        //
        // GET: /ChiTietHD/

        DataBaseDataContext db = new DataBaseDataContext();

        // show ad
        public ActionResult ShowCTHD_Admin()
        {
            return View(db.CHITIETHDs.ToList());
        }

        // Details ad
        public ActionResult DetailsCTHD_Admin(string maHD,string maSP)
        {
            var tl = db.CHITIETHDs.FirstOrDefault(t => t.MAHD == maHD && t.MASP == maSP);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl);
        }

        // Create ad
        [HttpGet]
        public ActionResult CreateCTHD_Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCTHD_Admin(CHITIETHD newtl)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.CHITIETHDs.InsertOnSubmit(newtl);
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
        public ActionResult EditCTHD_Admin(string maHD, string maSP)
        {

            CHITIETHD tl = db.CHITIETHDs.FirstOrDefault(t => t.MAHD == maHD && t.MASP == maSP);
            if (tl == null)
            {
                return HttpNotFound();
            }


            return View(tl);
        }

        [HttpPost]
        public ActionResult EditCTHD_Admin(CHITIETHD updatedTL)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    CHITIETHD tl = db.CHITIETHDs.FirstOrDefault(t => t.MAHD == updatedTL.MAHD && t.MASP == updatedTL.MASP);
                    if (tl != null)
                    {
                        // Cập nhật thông tin
                        tl.SOLUONG = updatedTL.SOLUONG;
                        tl.GIABAN = updatedTL.GIABAN;
                        tl.THANHTIEN = updatedTL.THANHTIEN;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        // Thông báo thành công
                        TempData["SuccessMessage"] = "Cập Nhật Thành Công!";
                        return RedirectToAction("ShowCTHD_Admin", "ChiTietHD");
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
        public ActionResult DeleteCTHD_Admin(string maHD, string maSP)
        {

            var tl = db.CHITIETHDs.FirstOrDefault(t => t.MAHD == maHD && t.MASP == maSP);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl); // Hiển thị xác nhận xóa
        }

        [HttpPost, ActionName("DeleteCTHD_Admin")]
        public ActionResult DeleteConfirmed(string maHD, string maSP)
        {

            var tl = db.CHITIETHDs.FirstOrDefault(t => t.MAHD == maHD && t.MASP == maSP);
            if (tl == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.CHITIETHDs.DeleteOnSubmit(tl);
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
            return RedirectToAction("ShowCTHD_Admin", "CHiTietHD");
        }

    }
}
