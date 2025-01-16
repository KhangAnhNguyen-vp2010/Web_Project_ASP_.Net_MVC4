using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanNoiThat.Models;

namespace QuanLyBanNoiThat.Controllers
{
    public class LoginController : Controller
    {

        //
        // GET: /Login/
        DataBaseDataContext db = new DataBaseDataContext();

        public ActionResult DangNhap(string taikhoan, string pass)
        {
            if (taikhoan != null || pass != null)
            {
                if (taikhoan.Length == 0 || pass.Length == 0)
                {
                    ViewBag.ThongBaoDN = "Tài Khoản Hoặc Mật Khẩu Không Được Bỏ Trống!";
                }
                else
                {
                    string sql = string.Format("SELECT dbo.KIEMTRA_DANGNHAP_KH(N'{0}',N'{1}')", taikhoan, pass);
                    var dk = db.ExecuteQuery<int>(sql).FirstOrDefault();
                    if (dk == 0)
                    {
                        ViewBag.ThongBaoDN = "Tài Khoản Hoặc Mật Khẩu Không Khớp";
                        return View();
                    }
                    
                    
                    // Tìm user dựa vào tên đăng nhập
                    var user = db._USER_KHs.FirstOrDefault(u => u.TENDANGNHAP == taikhoan);
                    if (user == null)
                    {
                        return HttpNotFound(); // Nếu không tìm thấy user, trả về lỗi 404
                    }

                    
                    Session["NameKH"] = user.TENDANGNHAP.Trim();
                    Session["MaKH"] = user.MA_USER.Trim();
                    Session["RoleKH"] = user.VAITRO.Trim();
                    return RedirectToAction("Index", "Home");
                }            
            }        
            return View();
        }

        string maKH()
        {
            // Lấy khách hàng có mã lớn nhất
            var khachHangCuoi = db.KHACHHANGs.OrderByDescending(t => t.MAKH).FirstOrDefault();

            // Mã khách hàng mặc định nếu chưa có khách hàng nào
            string old = "KH001";

            if (khachHangCuoi != null)
            {
                // Lấy mã khách hàng cuối cùng
                old = khachHangCuoi.MAKH;
                // Tách phần số từ mã khách hàng (bỏ đi 'KH')
                int stt = int.Parse(old.Substring(2));
                // Tăng số thứ tự lên 1
                stt++;
                // Tạo mã khách hàng mới với định dạng "KH" + 3 chữ số
                return "KH" + stt.ToString("D3");
            }

            // Trả về mã khách hàng mặc định nếu chưa có khách hàng nào
            return old;
        }


        public ActionResult DangKy(string tk, string pw, string rpw, string name)
        {
            // Kiểm tra nếu các trường đầu vào không rỗng
            if (!string.IsNullOrEmpty(tk) || !string.IsNullOrEmpty(pw) || !string.IsNullOrEmpty(rpw) || !string.IsNullOrEmpty(name))
            {
                // Kiểm tra xem các trường có bị bỏ trống không
                if (tk.Length == 0 || pw.Length == 0 || rpw.Length == 0 || name.Length == 0)
                {
                    ViewBag.ThongBaoDK = "Vui lòng nhập đầy đủ thông tin.";
                }
                else
                {
                    // Kiểm tra xem mật khẩu và xác nhận mật khẩu có khớp nhau không
                    if (pw != rpw)
                    {
                        ViewBag.ThongBaoDK = "Mật khẩu và xác nhận mật khẩu không khớp.";
                        return View();
                    }

                    // Kiểm tra xem tài khoản đã tồn tại chưa
                    var khachHangTonTai = db._USER_KHs.FirstOrDefault(u => u.TENDANGNHAP == tk);
                    if (khachHangTonTai != null)
                    {
                        ViewBag.ThongBaoDK = "Tài khoản đã tồn tại.";
                        return View();
                    }

                    try
                    {
                        // Tạo mã khách hàng tự động
                        string newMaKH = maKH();

                        // Thực hiện thêm khách hàng vào cơ sở dữ liệu
                        string sql = string.Format("insert into khachhang(makh, tenkh) values('{0}', N'{1}')", newMaKH, name);
                        db.ExecuteCommand(sql);

                        // Thêm tài khoản vào bảng _USER_
                        string sqlUser = string.Format("insert into _user_kh values('{0}', N'{1}', N'{2}', 'customer')", newMaKH, tk, pw);
                        db.ExecuteCommand(sqlUser);

                        // Thông báo đăng ký thành công
                        TempData["SuccessMessage"] = "Đăng ký thành công!";
                        return RedirectToAction("DangNhap");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ThongBaoDK = "Lỗi khi đăng ký: " + ex.Message;
                    }
                }
            }

            return View();
        }


    }
}
