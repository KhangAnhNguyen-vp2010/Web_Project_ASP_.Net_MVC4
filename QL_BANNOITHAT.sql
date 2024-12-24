﻿CREATE DATABASE QL_BANNOITHAT

USE QL_BANNOITHAT

CREATE TABLE LOAISP
(
	MALOAI CHAR(10) NOT NULL,
	TENLOAI NVARCHAR(255),
	MOTA NVARCHAR(255),
	CONSTRAINT PK_LOAISP PRIMARY KEY (MALOAI)
)

CREATE TABLE SANPHAM
(
	MASP CHAR(10) NOT NULL,
	TENSP NVARCHAR(255),
	MOTA NVARCHAR(255),
	GIA MONEY,
	MALOAI CHAR(10),
	SL_TON INT,
	HINHANH NVARCHAR(255),
	CONSTRAINT PK_SANPHAM PRIMARY KEY (MASP),
	CONSTRAINT FK_SANPHAM_LOAISP FOREIGN KEY (MALOAI) REFERENCES LOAISP(MALOAI)
)

CREATE TABLE KHACHHANG
(
	MAKH CHAR(10) NOT NULL,
	TENKH NVARCHAR(255),
	DIACHI NVARCHAR(255),
	EMAIL NVARCHAR(255),
	SDT NVARCHAR(255),
	HINHANH NVARCHAR(255),
	CONSTRAINT PK_KHACHHANG PRIMARY KEY (MAKH)
)

CREATE TABLE NHANVIEN
(
	MANV CHAR(10) NOT NULL,
	TENNHANVIEN NVARCHAR(255),
	SDT NVARCHAR(255),
	EMAIL NVARCHAR(255),
	NGAYVAOLAM DATE,
	CONSTRAINT PK_NHANVIEN PRIMARY KEY (MANV)
)

CREATE TABLE HOADON
(
	MAHD CHAR(10) NOT NULL,
	MAKH CHAR(10),
	MANV CHAR(10),
	NGAYLAP DATE,
	TONGTIEN MONEY,
	TRANGTHAI nvarchar(255)
	CONSTRAINT PK_HOADON PRIMARY KEY (MAHD),
	CONSTRAINT FK_HOADON_KHACHHANG FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH),
	CONSTRAINT FK_HOADON_NHANVIEN FOREIGN KEY (MANV) REFERENCES NHANVIEN(MANV)
)

CREATE TABLE CHITIETHD
(
	MAHD CHAR(10) NOT NULL,
	MASP CHAR(10) NOT NULL,
	SOLUONG INT,
	GIABAN MONEY,
	THANHTIEN MONEY,
	CONSTRAINT PK_CHITIETHD PRIMARY KEY (MAHD, MASP),
	CONSTRAINT FK_CHITIETHD_HOADON FOREIGN KEY (MAHD) REFERENCES HOADON(MAHD),
	CONSTRAINT FK_CHITIETHD_SANPHAM FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP)
)

CREATE TABLE _USER_KH
(
	MA_USER CHAR(10) NOT NULL,
	TENDANGNHAP NVARCHAR(255) UNIQUE,
	MATKHAU NVARCHAR(255),
	VAITRO NVARCHAR(255),
	CONSTRAINT PK_USER PRIMARY KEY (MA_USER),
	CONSTRAINT FK_USER_KHACHHANG FOREIGN KEY (MA_USER) REFERENCES KHACHHANG(MAKH)
)

CREATE TABLE _USER_NV
(
	MA_USER CHAR(10) NOT NULL,
	TENDANGNHAP NVARCHAR(255) UNIQUE,
	MATKHAU NVARCHAR(255),
	VAITRO NVARCHAR(255),
	CONSTRAINT PK_USER_NV PRIMARY KEY (MA_USER),
	CONSTRAINT FK_USER_NHANVIEN FOREIGN KEY (MA_USER) REFERENCES NHANVIEN(MANV)
)

INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI001', N'Sofa', N'Sofa các loại từ da đến vải');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI002', N'Bàn', N'Các loại bàn phòng khách, phòng ăn');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI003', N'Ghế', N'Ghế đơn, ghế tựa và ghế bành');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI004', N'Giường', N'Giường đơn, giường đôi, giường tầng');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI005', N'Tủ', N'Tủ quần áo, tủ sách, tủ giày');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI006', N'Đèn', N'Đèn ngủ, đèn trần, đèn trang trí');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI007', N'Thảm', N'Thảm trải sàn, thảm trang trí');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI008', N'Rèm cửa', N'Rèm cửa các loại');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI009', N'Kệ', N'Kệ tivi, kệ treo tường');
INSERT INTO LOAISP (MALOAI, TENLOAI, MOTA) VALUES ('LOAI010', N'Trang trí', N'Vật dụng trang trí khác');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP001', N'Sofa Livia', N'Sofa vải màu xanh', 15000000, 'LOAI001', 20, N'sofa_livia.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP002', N'Bàn ăn Verona', N'Bàn ăn gỗ tự nhiên 6 chỗ', 12000000, 'LOAI002', 10, N'ban_an_verona.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP003', N'Ghế đơn Leon', N'Ghế tựa lưng cao bọc da', 5500000, 'LOAI003', 30, N'ghe_don_leon.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP004', N'Giường đơn Nora', N'Giường gỗ đơn', 9000000, 'LOAI004', 15, N'giuong_don_nora.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP005', N'Tủ quần áo Capri', N'Tủ gỗ 3 cánh', 13000000, 'LOAI005', 8, N'tu_capri.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP006', N'Đèn ngủ Luna', N'Đèn ngủ bàn', 2000000, 'LOAI006', 50, N'den_ngu_luna.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP007', N'Thảm lông cừu Blanca', N'Thảm trải phòng khách', 700000, 'LOAI007', 25, N'tham_blanca.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP008', N'Rèm cửa Velvet', N'Rèm vải nhung', 3000000, 'LOAI008', 20, N'rem_velvet.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP009', N'Kệ treo tường Zeta', N'Kệ treo tường gỗ', 1500000, 'LOAI009', 40, N'ke_zeta.png');
INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) VALUES ('SP010', N'Lọ hoa sứ Allegra', N'Lọ hoa sứ trắng', 800000, 'LOAI010', 60, N'lo_hoa_allegra.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP011', N'Bàn trà Stella', N'Bàn trà mặt kính hiện đại', 5000000, 'LOAI002', 12, N'ban_tra_stella.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP012', N'Tủ sách Iris', N'Tủ sách 4 tầng bằng gỗ', 6500000, 'LOAI005', 18, N'tu_sach_iris.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP013', N'Ghế xoay văn phòng Zoom', N'Ghế xoay lưng lưới thoáng khí', 3200000, 'LOAI003', 25, N'ghe_zoom.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP014', N'Tranh treo tường Abstract', N'Tranh sơn dầu phong cách hiện đại', 2500000, 'LOAI010', 30, N'tranh_abstract.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP015', N'Chăn lông cừu Aria', N'Chăn lông cừu mềm mại và ấm áp', 1800000, 'LOAI010', 35, N'chan_aria.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP016', N'Bàn làm việc Solo', N'Bàn làm việc gỗ óc chó sang trọng', 8000000, 'LOAI002', 10, N'ban_solo.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP017', N'Đèn trang trí Aurora', N'Đèn trang trí với ánh sáng ấm', 2000000, 'LOAI006', 40, N'den_aurora.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP018', N'Thảm lông cừu Deluxe', N'Thảm lông cừu trải phòng ngủ', 700000, 'LOAI007', 30, N'tham_deluxe.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP019', N'Kệ tivi Onyx', N'Kệ tivi treo tường gỗ tự nhiên', 4500000, 'LOAI009', 20, N'ke_onyx.png');

INSERT INTO SANPHAM (MASP, TENSP, MOTA, GIA, MALOAI, SL_TON, HINHANH) 
VALUES ('SP020', N'Rèm cửa Scandinavian', N'Rèm cửa phong cách Bắc Âu', 3000000, 'LOAI008', 15, N'rem_scandinavian.png');


INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH001', N'Nguyễn Văn An', N'123 Lê Lợi, Q1, HCM', 'an.nguyen@gmail.com', '0912345678', N'an.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH002', N'Lê Thị Bích', N'456 Hai Bà Trưng, Q3, HCM', 'bich.le@gmail.com', '0932345678', N'bich.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH003', N'Trần Quang Hùng', N'789 Điện Biên Phủ, Q10, HCM', 'hung.tran@gmail.com', '0902345678', N'hung.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH004', N'Đỗ Thu Hương', N'101 Nguyễn Đình Chiểu, Q3, HCM', 'huong.do@gmail.com', '0982345678', N'huong.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH005', N'Phạm Minh Châu', N'102 Phan Xích Long, Q.Phú Nhuận, HCM', 'chau.pham@gmail.com', '0918345678', N'chau.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH006', N'Nguyễn Thanh Hải', N'20 Đinh Tiên Hoàng, Q.Bình Thạnh, HCM', 'hai.nguyen@gmail.com', '0922345678', N'hai.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH007', N'Lê Hoàng Việt', N'35 Trần Hưng Đạo, Q5, HCM', 'viet.le@gmail.com', '0932345679', N'viet.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH008', N'Đinh Phương Trinh', N'78 Phạm Văn Đồng, Q.Thủ Đức, HCM', 'trinh.dinh@gmail.com', '0909345678', N'trinh.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH009', N'Trần Thị Lan', N'99 Nguyễn Văn Trỗi, Q.Phú Nhuận, HCM', 'lan.tran@gmail.com', '0912345699', N'lan.png');
INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, EMAIL, SDT, HINHANH) VALUES ('KH010', N'Bùi Quang Minh', N'105 Quang Trung, Q.Gò Vấp, HCM', 'minh.bui@gmail.com', '0972345678', N'minh.png');

INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV001', N'Nguyễn Văn Tuấn', '0908123456', 'tuan.nguyen@gmail.com', '2022-01-15');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV002', N'Phạm Thị Lan', '0934123456', 'lan.pham@gmail.com', '2021-03-10');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV003', N'Lê Quang Huy', '0945123456', 'huy.le@gmail.com', '2020-06-25');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV004', N'Trần Minh Hoàng', '0912345671', 'hoang.tran@gmail.com', '2019-08-19');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV005', N'Vũ Thị Thảo', '0938123457', 'thao.vu@gmail.com', '2023-01-30');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV006', N'Nguyễn Đức Long', '0909123456', 'long.nguyen@gmail.com', '2021-07-12');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV007', N'Phan Văn Bình', '0945123458', 'binh.phan@gmail.com', '2020-09-08');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV008', N'Nguyễn Thị Như', '0923123456', 'nhu.nguyen@gmail.com', '2019-11-05');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV009', N'Lê Thị Thanh', '0912345692', 'thanh.le@gmail.com', '2023-04-10');
INSERT INTO NHANVIEN (MANV, TENNHANVIEN, SDT, EMAIL, NGAYVAOLAM) VALUES ('NV010', N'Trương Hoàng Vinh', '0932345693', 'vinh.truong@gmail.com', '2021-10-22');

INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD001', 'KH001', 'NV001', '2023-01-15', 15000000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD002', 'KH002', 'NV002', '2023-01-20', 12000000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD003', 'KH003', 'NV003', '2023-02-02', 5500000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD004', 'KH004', 'NV004', '2023-02-10', 9000000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD005', 'KH005', 'NV005', '2023-02-15', 13000000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD006', 'KH006', 'NV001', '2023-03-01', 4000000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD007', 'KH007', 'NV002', '2023-03-05', 2800000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD008', 'KH008', 'NV003', '2023-03-15', 3000000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD009', 'KH009', 'NV004', '2023-03-20', 3000000);
INSERT INTO HOADON (MAHD, MAKH, MANV, NGAYLAP, TONGTIEN) VALUES ('HD010', 'KH010', 'NV005', '2023-04-01', 800000);

INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD001', 'SP001', 1, 15000000, 15000000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD002', 'SP002', 1, 12000000, 12000000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD003', 'SP003', 1, 5500000, 5500000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD004', 'SP004', 1, 9000000, 9000000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD005', 'SP005', 1, 13000000, 13000000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD006', 'SP006', 2, 2000000, 4000000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD007', 'SP007', 4, 700000, 2800000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD008', 'SP008', 1, 3000000, 3000000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD009', 'SP009', 2, 1500000, 3000000);
INSERT INTO CHITIETHD (MAHD, MASP, SOLUONG, GIABAN, THANHTIEN) VALUES ('HD010', 'SP010', 1, 800000, 800000);

select * from SANPHAM
select * from LOAISP
SELECT * FROM KHACHHANG
SELECT * FROM NHANVIEN
SELECT * FROM HOADON
SELECT * FROM CHITIETHD
SELECT * FROM _USER_KH

----------------
CREATE FUNCTION KIEMTRA_DANGNHAP_KH (@TK NVARCHAR(255), @MK NVARCHAR(255))
RETURNS INT
AS
BEGIN
    DECLARE @result INT;

    -- Kiểm tra xem có bản ghi nào khớp cả tên đăng nhập và mật khẩu hay không
    IF EXISTS (SELECT 1 FROM _USER_KH WHERE TENDANGNHAP = @TK AND MATKHAU = @MK)
        SET @result = 1;
    ELSE
        SET @result = 0;

    RETURN @result;
END;

go
DECLARE @kq INT;
SET @kq = dbo.KIEMTRA_DANGNHAP_KH('khang','khang123') 
PRINT @kq

CREATE FUNCTION KIEMTRA_DANGNHAP_NV (@TK NVARCHAR(255), @MK NVARCHAR(255))
RETURNS INT
AS
BEGIN
    DECLARE @result INT;

    -- Kiểm tra xem có bản ghi nào khớp cả tên đăng nhập và mật khẩu hay không
    IF EXISTS (SELECT 1 FROM _USER_NV WHERE TENDANGNHAP = @TK AND MATKHAU = @MK)
        SET @result = 1;
    ELSE
        SET @result = 0;

    RETURN @result;
END;


CREATE TRIGGER trg_KiemTraEmail_KH
ON KHACHHANG
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @EMAIL NVARCHAR(255);

    -- Lấy giá trị email từ bản ghi vừa được chèn hoặc cập nhật
    SELECT @EMAIL = EMAIL FROM inserted;

    -- Kiểm tra nếu email không chứa ký tự '@' hoặc dấu '.' sau '@'
    IF CHARINDEX('@', @EMAIL) = 0 OR CHARINDEX('.', @EMAIL, CHARINDEX('@', @EMAIL)) = 0
    BEGIN
        -- Hủy bỏ thao tác nếu email không hợp lệ
        ROLLBACK TRANSACTION;
        RAISERROR('Email không hợp lệ! Vui lòng nhập lại.', 16, 1);
    END
END;

CREATE TRIGGER trg_KiemTraEmail_NV
ON NHANVIEN
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @EMAIL NVARCHAR(255);

    -- Lấy giá trị email từ bản ghi vừa được chèn hoặc cập nhật
    SELECT @EMAIL = EMAIL FROM inserted;

    -- Kiểm tra nếu email không chứa ký tự '@' hoặc dấu '.' sau '@'
    IF CHARINDEX('@', @EMAIL) = 0 OR CHARINDEX('.', @EMAIL, CHARINDEX('@', @EMAIL)) = 0
    BEGIN
        -- Hủy bỏ thao tác nếu email không hợp lệ
        ROLLBACK TRANSACTION;
        RAISERROR('Email không hợp lệ! Vui lòng nhập lại.', 16, 1);
    END
END;


-------

CREATE TRIGGER UPDATE_THANHTIEN_CHITIETHD ON CHITIETHD
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @SL INT = (SELECT SOLUONG FROM inserted)
	DECLARE @GIA MONEY = (SELECT GIABAN FROM inserted)
	UPDATE CHITIETHD
	SET THANHTIEN = @SL * @GIA
	WHERE MAHD IN (SELECT MAHD FROM inserted)
	AND MASP IN (SELECT MASP FROM inserted)
END

CREATE PROC TINH_TONGTIEN_HOADON @MAHD CHAR(10)
AS
BEGIN
	DECLARE @TT MONEY = (SELECT SUM(THANHTIEN) FROM CHITIETHD WHERE MAHD = @MAHD)
	UPDATE HOADON
	SET TONGTIEN = @TT
	WHERE MAHD = @MAHD
END

CREATE PROC SHOW_HD_THEO_MAKH @MAKH CHAR(10)
AS
BEGIN
	SELECT HOADON.MAHD, SANPHAM.MASP, MANV, NGAYLAP, SOLUONG, GIABAN, TONGTIEN, HINHANH 
	FROM HOADON, CHITIETHD, SANPHAM
	WHERE HOADON.MAHD = CHITIETHD.MAHD
	AND CHITIETHD.MASP = SANPHAM.MASP
	AND MAKH = @MAKH
END

EXEC SHOW_HD_THEO_MAKH 'KH001'



------------------
INSERT INTO _USER_KH
VALUES ('KH001', 'khang', '123', 'customer');
INSERT INTO _USER_NV
VALUES ('NV001', 'nv001', '123', 'staff');
INSERT INTO _USER_NV
VALUES ('NV002', 'nv002', '123', 'manager');

DELETE FROM CHITIETHD
DELETE FROM HOADON
DELETE FROM KHACHHANG
DELETE FROM _USER_KH
