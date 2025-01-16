using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class TheLoaiController : Controller
    {
        //
        // GET: /TheLoai/

        DataBaseDataContext db = new DataBaseDataContext();

        public ActionResult ShowTheLoai()
        {
            return View(db.LOAISPs.ToList());
        }

        public ActionResult SP_Theo_TheLoai(string id)
        {
            var List = db.SANPHAMs.Where(s => s.MALOAI == id).ToList();
            if (List.Count == 0)
            {
                ViewBag.Sach = "Không Có Sản Phẩm Nào Thuộc Loại Này";
            }
            return View(List);
        }

        // show for ad
        public ActionResult ShowTheLoai_ForAdmin()
        {
            return View(db.LOAISPs.ToList());
        }

        // Details ad
        public ActionResult DetailsTL_Admin(string id)
        {
            var tl = db.LOAISPs.FirstOrDefault(t => t.MALOAI == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl);
        }

        // Create ad
        [HttpGet]
        public ActionResult CreateTL_Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTL_Admin(LOAISP newtl)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.LOAISPs.InsertOnSubmit(newtl);
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
        public ActionResult EditTL_Admin(string id)
        {

            LOAISP tl = db.LOAISPs.FirstOrDefault(t => t.MALOAI== id);
            if (tl == null)
            {
                return HttpNotFound();
            }


            return View(tl);
        }

        [HttpPost]
        public ActionResult EditTL_Admin(LOAISP updatedTL)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    LOAISP tl = db.LOAISPs.FirstOrDefault(t => t.MALOAI == updatedTL.MALOAI);
                    if (tl != null)
                    {
                        // Cập nhật thông tin
                        tl.MALOAI = updatedTL.MALOAI;
                        tl.TENLOAI = updatedTL.TENLOAI;
                        tl.MOTA = updatedTL.MOTA;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        // Thông báo thành công
                        TempData["SuccessMessage"] = "Cập Nhật Thành Công!";
                        return RedirectToAction("ShowTheLoai_ForAdmin", "TheLoai");
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
        public ActionResult DeleteTL_Admin(string id)
        {

            var tl = db.LOAISPs.FirstOrDefault(t => t.MALOAI == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl); // Hiển thị xác nhận xóa
        }

        [HttpPost, ActionName("DeleteTL_Admin")]
        public ActionResult DeleteConfirmed(string id)
        {

            var tl = db.LOAISPs.FirstOrDefault(t => t.MALOAI == id);
            if (tl == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.LOAISPs.DeleteOnSubmit(tl);
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
            return RedirectToAction("ShowTheLoai_ForAdmin", "TheLoai");
        }
    }
}
