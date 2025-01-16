using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/

        DataBaseDataContext db = new DataBaseDataContext();

        public ActionResult ShowSanPham(int page = 1, int pageSize = 9)
        {
            var products = db.SANPHAMs.OrderBy(sp => sp.MASP).ToList();
            // Tính toán số lượng sản phẩm cần hiển thị và tổng số trang
            int totalItems = products.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            // Lấy danh sách sản phẩm cho trang hiện tại
            var productsOnPage = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            // Truyền dữ liệu vào ViewBag để sử dụng trong View
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            return View(productsOnPage);
        }

        // details
        public ActionResult DetailsSanPham(string id)
        {
            var sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            if (sp == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy Tour, trả về lỗi 404
            }
            //return View(sp); // Trả về view chi tiết của tour
            return View(sp);
        }

        // edit
        [HttpGet]
        public ActionResult EditSP(string id)
        {
            // Tìm tour theo mã (id)
            SANPHAM sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

       
            return View(sp);
        }

        [HttpPost]
        public ActionResult EditSP(SANPHAM updatedSP)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tìm tour cần chỉnh sửa
                    SANPHAM sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == updatedSP.MASP);
                    if (sp != null)
                    {
                        // Cập nhật thông tin
                        sp.TENSP = updatedSP.TENSP;
                        sp.MOTA = updatedSP.MOTA;
                        sp.GIA = updatedSP.GIA;
                        sp.MALOAI = updatedSP.MALOAI;
                        sp.SL_TON = updatedSP.SL_TON;
                        sp.HINHANH = updatedSP.HINHANH;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        return RedirectToAction("ShowSanPham_ForAdmin", "Admin");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi cập nhật tour: " + ex.Message);
                }
            }

         
            return View(updatedSP);
        }

        public ActionResult ShowSanPham_ForAdmin()
        {
            return View(db.SANPHAMs.ToList());
        }

        // Details ad
        public ActionResult DetailsSP_Admin(string id)
        {
            var sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        // Create ad
        [HttpGet]
        public ActionResult CreateSP_Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSP_Admin(SANPHAM newsp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    db.SANPHAMs.InsertOnSubmit(newsp);
                    db.SubmitChanges();

                    

                    ViewBag.ThongBao = "Thêm Thành Công";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi thêm: " + ex.Message);
                }
            }
            return View(newsp);
        }

        // Edit ad
        [HttpGet]
        public ActionResult EditSP_Admin(string id)
        {
            
            SANPHAM sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            
            return View(sp);
        }

        [HttpPost]
        public ActionResult EditSP_Admin(SANPHAM updatedSP)
        {
            if (ModelState.IsValid)
            {
                try
                {
                  
                    SANPHAM sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == updatedSP.MASP);
                    if (sp != null)
                    {
                        // Cập nhật thông tin
                        sp.TENSP = updatedSP.TENSP;
                        sp.MOTA = updatedSP.MOTA;
                        sp.GIA = updatedSP.GIA;
                        sp.MALOAI = updatedSP.MALOAI;
                        sp.SL_TON = updatedSP.SL_TON;
                        sp.HINHANH = updatedSP.HINHANH;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        // Thông báo thành công
                        TempData["SuccessMessage"] = "Cập Nhật Thành Công!";
                        return RedirectToAction("ShowSanPham_ForAdmin", "SanPham");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                }
            }

            
            return View(updatedSP);
        }

        // Delete ad
        public ActionResult DeleteSP_Admin(string id)
        {
            
            var sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp); // Hiển thị xác nhận xóa
        }

        [HttpPost, ActionName("DeleteSP_Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
           
            var sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.SANPHAMs.DeleteOnSubmit(sp);
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
            return RedirectToAction("ShowSanPham_ForAdmin", "SanPham");
        }
    }
}
