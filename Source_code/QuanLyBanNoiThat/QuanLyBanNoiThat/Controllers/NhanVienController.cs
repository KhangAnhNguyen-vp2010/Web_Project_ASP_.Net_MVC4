using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class NhanVienController : Controller
    {
        //
        // GET: /NhanVien/
        DataBaseDataContext db = new DataBaseDataContext();

        // show ad
        public ActionResult ShowNV_ForAdmin()
        {
            return View(db.NHANVIENs.ToList());
        }

        // Details ad
        public ActionResult DetailsNV_Admin(string id)
        {
            var tl = db.NHANVIENs.FirstOrDefault(t => t.MANV == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl);
        }

        // Create ad
        [HttpGet]
        public ActionResult CreateNV_Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNV_Admin(NHANVIEN newtl)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.NHANVIENs.InsertOnSubmit(newtl);
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
        public ActionResult EditNV_Admin(string id)
        {

            NHANVIEN tl = db.NHANVIENs.FirstOrDefault(t => t.MANV == id);
            if (tl == null)
            {
                return HttpNotFound();
            }


            return View(tl);
        }

        [HttpPost]
        public ActionResult EditNV_Admin(NHANVIEN updatedTL)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    NHANVIEN tl = db.NHANVIENs.FirstOrDefault(t => t.MANV == updatedTL.MANV);
                    if (tl != null)
                    {
                        // Cập nhật thông tin
                        tl.TENNHANVIEN = updatedTL.TENNHANVIEN;
                        tl.SDT = updatedTL.SDT;
                        tl.EMAIL = updatedTL.EMAIL;
                        tl.NGAYVAOLAM = updatedTL.NGAYVAOLAM;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        // Thông báo thành công
                        TempData["SuccessMessage"] = "Cập Nhật Thành Công!";
                        if ((string)Session["RoleNV"] == "staff")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        return RedirectToAction("ShowNV_ForAdmin", "NhanVien");
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
        public ActionResult DeleteNV_Admin(string id)
        {

            var tl = db.NHANVIENs.FirstOrDefault(t => t.MANV == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl); // Hiển thị xác nhận xóa
        }

        [HttpPost, ActionName("DeleteNV_Admin")]
        public ActionResult DeleteConfirmed(string id)
        {

            var tl = db.NHANVIENs.FirstOrDefault(t => t.MANV == id);
            if (tl == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.NHANVIENs.DeleteOnSubmit(tl);
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
            return RedirectToAction("ShowNV_ForAdmin", "NhanVien");
        }

    }
}
