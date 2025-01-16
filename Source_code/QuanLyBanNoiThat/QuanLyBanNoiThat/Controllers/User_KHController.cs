using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class User_KHController : Controller
    {
        //
        // GET: /User_KH/
        DataBaseDataContext db = new DataBaseDataContext();

        // show ad
        public ActionResult ShowUserKH_ForAdmin()
        {
            return View(db._USER_KHs.ToList());
        }

        // Details ad
        public ActionResult DetailsUserKH_Admin(string id)
        {
            var tl = db._USER_KHs.FirstOrDefault(t => t.MA_USER == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl);
        }

        // Create ad
        [HttpGet]
        public ActionResult CreateUserKH_Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUserKH_Admin(_USER_KH newtl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    newtl.VAITRO = "customer";
                    db._USER_KHs.InsertOnSubmit(newtl);
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
        public ActionResult EditUserKH_Admin(string id)
        {

            _USER_KH tl = db._USER_KHs.FirstOrDefault(t => t.MA_USER == id);
            if (tl == null)
            {
                return HttpNotFound();
            }

            return View(tl);
        }

        [HttpPost]
        public ActionResult EditUserKH_Admin(_USER_KH updatedTL)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _USER_KH tl = db._USER_KHs.FirstOrDefault(t => t.MA_USER == updatedTL.MA_USER);
                    if (tl != null)
                    {
                        // Cập nhật thông tin
                        tl.TENDANGNHAP = updatedTL.TENDANGNHAP;
                        tl.MATKHAU = updatedTL.MATKHAU;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        // Thông báo thành công
                        TempData["SuccessMessage"] = "Cập Nhật Thành Công!";
                        return RedirectToAction("ShowUserKH_ForAdmin", "User_KH");
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
        public ActionResult DeleteUserKH_Admin(string id)
        {

            var tl = db._USER_KHs.FirstOrDefault(t => t.MA_USER == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl); // Hiển thị xác nhận xóa
        }

        [HttpPost, ActionName("DeleteUserKH_Admin")]
        public ActionResult DeleteConfirmed(string id)
        {

            var tl = db._USER_KHs.FirstOrDefault(t => t.MA_USER == id);
            if (tl == null)
            {
                return HttpNotFound();
            }

            try
            {
                db._USER_KHs.DeleteOnSubmit(tl);
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
            return RedirectToAction("ShowUserKH_ForAdmin", "User_KH");
        }

    }
}
