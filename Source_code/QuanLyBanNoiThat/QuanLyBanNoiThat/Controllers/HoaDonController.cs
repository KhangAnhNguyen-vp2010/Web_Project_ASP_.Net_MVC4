using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class HoaDonController : Controller
    {
        //
        // GET: /HoaDon/

        DataBaseDataContext db = new DataBaseDataContext();

        public ActionResult ShowHoaDon()
        {
            return View(db.HOADONs.ToList());
        }

        // mua hang
        [HttpGet]
        public ActionResult CreateHoaDon_KH(string id)
        {
            var sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            if (sp == null)
            {
                return HttpNotFound(); // trả về lỗi 404
            }
            
            return View(sp);
        }

        [HttpPost]
        public ActionResult CreateHoaDon_KH(int sl, string masp, double gia)
        {
            SANPHAM sp = new SANPHAM();
            sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == masp);
            if (sl > sp.SL_TON)
            {
                ViewBag.ThongBaoCreHD = "Số Lượng Mua Không Được Vượt Quá Số Lượng Tồn Kho";
            }
            else
            {
                try
                {
                    string mahd = maHD();
                    string sql = string.Format("insert into hoadon values('{0}', '{1}', null, getdate(), null, N'Chưa Thanh Toán')", mahd, Session["MaKH"]);
                    db.ExecuteCommand(sql);
                    string sql2 = string.Format("insert into chitiethd values('{0}', '{1}', '{2}', '{3}', null)", mahd, masp, sl, gia);
                    db.ExecuteCommand(sql2);
                    string sql3 = string.Format("EXEC TINH_TONGTIEN_HOADON '{0}'", mahd);
                    db.ExecuteCommand(sql3);
                    string sql4 = string.Format("update sanpham set sl_ton = sl_ton - '{0}' where masp = '{1}'", sl, masp);
                    db.ExecuteCommand(sql4);
                    TempData["AlertMessage"] = "Mua Hàng Thành Công";
                    return RedirectToAction("ShowSanPham", "SanPham");
                }
                catch (Exception ex)
                {
                    ViewBag.ThongBaoT = "Lỗi: " + ex.Message;
                }
            }
            return View(sp);
        }

        // huy don dang
        [HttpPost]
        public ActionResult HuyDonHang(string mahd, string masp, int sl)
        {
            try
            {
                string sql = string.Format("delete chitiethd where mahd = '{0}' and masp = '{1}'", mahd, masp);
                db.ExecuteCommand(sql);
                string sql2 = string.Format("delete hoadon where mahd = '{0}'", mahd);
                db.ExecuteCommand(sql2);
                string sql3 = string.Format("update sanpham set sl_ton = sl_ton + '{0}' where masp = '{1}'", sl, masp);
                db.ExecuteCommand(sql3);
                return RedirectToAction("ShowGioHang", "HoaDon", new { id = Session["MaKH"] });
            }
            catch (Exception ex)
            {
                ViewBag.ThongBaoT = "Lỗi: " + ex.Message;
            }
            return View();
        }

        string maHD()
        {
        
            var HDCuoi = db.HOADONs.OrderByDescending(t => t.MAHD).FirstOrDefault();

            // Mã khách hàng mặc định nếu chưa có khách hàng nào
            string old = "HD001";

            if (HDCuoi != null)
            {
                // Lấy mã khách hàng cuối cùng
                old = HDCuoi.MAHD;
                // Tách phần số từ mã khách hàng (bỏ đi 'KH')
                int stt = int.Parse(old.Substring(2));
                // Tăng số thứ tự lên 1
                stt++;
                // Tạo mã khách hàng mới với định dạng "KH" + 3 chữ số
                return "HD" + stt.ToString("D3");
            }

            // Trả về mã khách hàng mặc định nếu chưa có khách hàng nào
            return old;
        }

        // show gio hang
        public ActionResult ShowGioHang(string id)
        {
            var List = new List<CHITIETHD>();

            try
            {
                // Gọi stored procedure sử dụng ExecuteQuery
                string sql = string.Format("EXEC SHOW_HD_THEO_MAKH '{0}'", id);
                List = db.ExecuteQuery<CHITIETHD>(sql).ToList();
                ViewBag.TongTienALL = Convert.ToInt32(List.Where(t => t.HOADON.TRANGTHAI == "Chưa Thanh Toán").Sum(t => t.HOADON.TONGTIEN)).ToString();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi lấy dữ liệu: " + ex.Message;
            }
            return View(List);
        }

        // show hoa don chua duyet
        public ActionResult ShowHoaDon_ChuaDuyet()
        {
            return View(db.HOADONs.ToList());
        }

        // thuc hien duyet hoa don
        [HttpPost]
        public ActionResult DuyetHoaDon(string id)
        {
            HOADON hd = db.HOADONs.FirstOrDefault(t => t.MAHD == id);
            if (ModelState.IsValid)
            {
                try
                {                 
                    if (hd != null)
                    {
                        // Cập nhật thông tin
                        hd.MANV = (string)Session["MaNV"];

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        return RedirectToAction("ShowHoaDon_ChuaDuyet", "HoaDon");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                }
            }


            return View(hd);
        }

        // thanh toan
        [HttpGet]
        public ActionResult ThanhToan(int ALlorSingle, string makh, string mahd = null)
        {
            var list = new List<CHITIETHD>();
            if (ALlorSingle == 1)
            { 
                list = db.CHITIETHDs.Where(t => t.HOADON.MAKH == makh && t.HOADON.TRANGTHAI == "Chưa Thanh Toán" && t.HOADON.MANV != null).ToList();
                ViewBag.SumALL = Convert.ToInt32(list.Sum(t => t.HOADON.TONGTIEN)).ToString();
            }
            else
            {
                list = db.CHITIETHDs.Where(t => t.HOADON.MAKH == makh && t.MAHD == mahd).ToList();
                ViewBag.SumSingle = Convert.ToInt32(list.Sum(t => t.HOADON.TONGTIEN)).ToString();
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult ThucHienThanhToan(int ALLorSingle, long tongtien, long sumTotal, string makh, string mahd)
        {
            if (ALLorSingle == 1)
            {
                if (tongtien < sumTotal)
                {
                    TempData["ThongBaoThanhToan"] = "Số Tiền Không Đủ Để Thanh Toán";
                    return RedirectToAction("ThanhToan", "HoaDon", new { AllorSingle = 1, makh = Session["MaKH"] });
                }
                string sql = string.Format("update hoadon set trangthai = N'Đã Thanh Toán' where makh = '{0}'", makh);
                db.ExecuteCommand(sql);
            }
            else
            {
                if (tongtien < sumTotal)
                {
                    TempData["ThongBaoThanhToan"] = "Số Tiền Không Đủ Để Thanh Toán";
                    return RedirectToAction("ThanhToan", "HoaDon", new { AllorSingle = 0, makh = Session["MaKH"], mahd = mahd });
                }
                string sql1 = string.Format("update hoadon set trangthai = N'Đã Thanh Toán' where makh = '{0}' and mahd = '{1}'", makh, mahd);
                db.ExecuteCommand(sql1);
            }
            return RedirectToAction("ShowGioHang", "HoaDon", new { id = Session["MaKH"] });
        }

        // show for ad
        public ActionResult ShowHoaDon_ForAdmin()
        {
            return View(db.HOADONs.ToList());
        }

        // Details ad
        public ActionResult DetailsHD_Admin(string id)
        {
            var tl = db.HOADONs.FirstOrDefault(t => t.MAHD == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl);
        }

        // Create ad
        [HttpGet]
        public ActionResult CreateHD_Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateHD_Admin(HOADON newtl)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.HOADONs.InsertOnSubmit(newtl);
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
        public ActionResult EditHD_Admin(string id)
        {

            HOADON tl = db.HOADONs.FirstOrDefault(t => t.MAHD == id);
            if (tl == null)
            {
                return HttpNotFound();
            }


            return View(tl);
        }

        [HttpPost]
        public ActionResult EditHD_Admin(HOADON updatedTL)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    HOADON tl = db.HOADONs.FirstOrDefault(t => t.MAHD == updatedTL.MAHD);
                    if (tl != null)
                    {
                        // Cập nhật thông tin
                        tl.MAHD = updatedTL.MAHD;
                        tl.MAKH = updatedTL.MAKH;
                        tl.MANV = updatedTL.MANV;
                        tl.NGAYLAP = updatedTL.NGAYLAP;
                        tl.TONGTIEN = updatedTL.TONGTIEN;

                        // Lưu thay đổi vào database
                        db.SubmitChanges();

                        // Thông báo thành công
                        TempData["SuccessMessage"] = "Cập Nhật Thành Công!";
                        return RedirectToAction("ShowHoaDon_ForAdmin", "HoaDon");
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
        public ActionResult DeleteHD_Admin(string id)
        {

            var tl = db.HOADONs.FirstOrDefault(t => t.MAHD == id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            return View(tl); // Hiển thị xác nhận xóa
        }

        [HttpPost, ActionName("DeleteHD_Admin")]
        public ActionResult DeleteConfirmed(string id)
        {

            var tl = db.HOADONs.FirstOrDefault(t => t.MAHD == id);
            if (tl == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.HOADONs.DeleteOnSubmit(tl);
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
            return RedirectToAction("ShowHoaDon_ForAdmin", "HoaDon");
        }
    }
}
