﻿-- Lấy tên chi nhánh
create proc GetChiNhanh
as
begin
	declare @chinhanh int, @duong nvarchar(50), @quan nvarchar(50), @khuvuc nvarchar(50), @thanhpho nvarchar(50)
	if OBJECT_ID('temp..#GetChiNhanh') is not null drop table #GetChiNhanh
	create table #GetChiNhanh( DiaChi nvarchar(100))
	declare a cursor for select IDCHINHANH, DUONG, QUAN, KHUVUC, THANHPHO from CHINHANH
	open a
	fetch next from a into @chinhanh, @duong, @quan, @khuvuc, @thanhpho
	while(@@FETCH_STATUS=0)
	begin
		declare @string nvarchar(100)
		set @string = N'Chi nhánh ' + cast(@chinhanh as nvarchar(10)) + N', Đường ' + @duong + N', Quận ' + @quan + N', Khu vực ' + @khuvuc + N', Thành phố ' + @thanhpho
		insert into #GetChiNhanh(DiaChi) values(@string)
		fetch next from a into @chinhanh, @duong, @quan, @khuvuc, @thanhpho
	end
	close a
	deallocate a
	select * from #GetChiNhanh
end
go

-- Thêm nhân viên
create proc InsertNhanVien @idchinhanh int, @ten nvarchar(100), @sdt nvarchar(20),
@gt nvarchar(20), @ns date, @luong money, @dc nvarchar(100)
as
begin
	insert into NHANVIEN(IDCHINHANH, TENNHANVIEN, SDT, GIOITINH, NGAYSINH, LUONG, DIACHI) values(@idchinhanh, @ten, @sdt, @gt, @ns, @luong, @dc)
end
go

-- Sửa nhân viên
create proc UpdateNhanVien @idnv int, @idchinhanh int, @ten nvarchar(100), @sdt nvarchar(20),
@gt nvarchar(20), @ns date, @luong money, @dc nvarchar(100)
as
begin
	update NHANVIEN set IDCHINHANH = @idchinhanh, TENNHANVIEN = @ten,
	SDT = @sdt, GIOITINH = @gt, NGAYSINH = @ns, LUONG = @luong, DIACHI = @dc
	where IDNHANVIEN = @idnv
end
go

-- Thêm hợp đồng mua
create proc InsertHDM @idkh int, @idnha int, @nhl date, @nl date
as
begin
	insert into HOPDONGMUA(IDKHACHHANG, IDNHABAN, NGAYHIEULUC, NGAYLAP) values(@idkh, @idnha, @nhl, @nl)
end
go

-- Sửa hợp đồng mua
create proc UpdateHDM  @idhd int, @idkh int, @idnha int, @nhl date, @nl date
as
begin
	update HOPDONGMUA set IDKHACHHANG = @idkh, IDNHABAN = @idnha, NGAYHIEULUC = @nhl, NGAYLAP = @nl
	where IDHOPDONG = @idhd
end
go

-- Thêm hợp đồng thuê
create proc InsertHDT @idkh int, @idnha int, @nl date, @nbd date, @nkt date
as
begin
	insert into HOPDONGTHUE(IDKHACHHANG, IDNHATHUE, NGAYLAP, NGAYBATDAU, NGAYKETTHUC) values(@idkh, @idnha, @nl, @nbd, @nkt)
end
go

-- Sửa hợp đồng thuê
create proc UpdateHDT  @idhd int, @idkh int, @idnha int, @nl date, @nbd date, @nkt date
as
begin
	update HOPDONGTHUE set IDKHACHHANG = @idkh, IDNHATHUE = @idnha, NGAYLAP = @nl, NGAYBATDAU = @nbd, NGAYKETTHUC = @nkt
	where IDHOPDONG = @idhd
end
exec InsertHDM '6', '2', '07/24/2020', '06/24/2020'
go

-- Thêm nhà bán
create proc InsertNhaBan @idnv int, @idln int, @idcnha int, @idcn int, @duong nvarchar(50), @quan nvarchar(50), @kv nvarchar(50),
@tp nvarchar(50), @nd date, @nhh date, @sophong int, @tt int, @giathue money, @giaban money, @dieukien nvarchar(100)
as
begin
	if(@idnv != '')
		insert into NHABAN(IDNHANVIEN, IDLOAINHA, IDCHUNHA, IDCHINHANH, DUONG, QUAN, KHUVUC, THANHPHO, NGAYDANG, NGAYHETHAN, SOPHONG, TINHTRANGNHA, GIATHUE, GIANHA, DIEUKIEN)
		values(@idnv, @idln, @idcnha, @idcn, @duong, @quan, @kv, @tp, @nd, @nhh, @sophong, @tt, @giathue,@giaban, @dieukien)
	else
		insert into NHABAN(IDNHANVIEN, IDLOAINHA, IDCHUNHA, IDCHINHANH, DUONG, QUAN, KHUVUC, THANHPHO, NGAYDANG, NGAYHETHAN, SOPHONG, TINHTRANGNHA, GIATHUE, GIANHA, DIEUKIEN)
		values(null, @idln, @idcnha, @idcn, @duong, @quan, @kv, @tp, @nd, @nhh, @sophong, @tt, @giathue,@giaban, @dieukien)
end
go

-- Sửa nhà bán
create proc UpdateNhaBan @idmn int, @idnv int, @idln int, @idcnha int, @idcn int, @duong nvarchar(50), @quan nvarchar(50), @kv nvarchar(50),
@tp nvarchar(50), @nd date, @nhh date, @sophong int, @tt int, @giathue money, @giaban money, @dieukien nvarchar(100)
as
begin
	update NHABAN set IDNHANVIEN = @idnv, IDLOAINHA = @idln, IDCHUNHA = @idcnha, IDCHINHANH = @idcn, DUONG = @duong,
	QUAN = @quan, KHUVUC = @kv, THANHPHO = @tp, NGAYDANG = @nd, NGAYHETHAN = @nhh, SOPHONG = @sophong, TINHTRANGNHA = @tt, GIATHUE = @giathue,
	GIANHA = @giaban, DIEUKIEN = @dieukien
	where IDNHABAN = @idmn
end
go

-- Thêm nhà thuê
create proc InsertNhaThue @idnv int, @idln int, @idcnha int, @idcn int, @duong nvarchar(50), @quan nvarchar(50), @kv nvarchar(50),
@tp nvarchar(50), @nd date, @nhh date, @sophong int, @tt int, @giathue money
as
begin
	if(@idnv != '')
		insert into NHATHUE(IDNHANVIEN, IDLOAINHA, IDCHUNHA, IDCHINHANH, DUONG, QUAN, KHUVUC, THANHPHO, NGAYDANG, NGAYHETHAN, SOPHONG, TINHTRANGNHA, GIATHUE)
		values(@idnv, @idln, @idcnha, @idcn, @duong, @quan, @kv, @tp, @nd, @nhh, @sophong, @tt, @giathue)
	else
		insert into NHATHUE(IDNHANVIEN, IDLOAINHA, IDCHUNHA, IDCHINHANH, DUONG, QUAN, KHUVUC, THANHPHO, NGAYDANG, NGAYHETHAN, SOPHONG, TINHTRANGNHA, GIATHUE)
		values(null, @idln, @idcnha, @idcn, @duong, @quan, @kv, @tp, @nd, @nhh, @sophong, @tt, @giathue)
end
go

-- Sửa nhà thuê
create proc UpdateNhaThue @idmn int, @idnv int, @idln int, @idcnha int, @idcn int, @duong nvarchar(50), @quan nvarchar(50), @kv nvarchar(50),
@tp nvarchar(50), @nd date, @nhh date, @sophong int, @tt int, @giathue money
as
begin
	update NHATHUE set IDNHANVIEN = @idnv, IDLOAINHA = @idln, IDCHUNHA = @idcnha, IDCHINHANH = @idcn, DUONG = @duong,
	QUAN = @quan, KHUVUC = @kv, THANHPHO = @tp, NGAYDANG = @nd, NGAYHETHAN = @nhh, SOPHONG = @sophong, TINHTRANGNHA = @tt, GIATHUE = @giathue
	where IDNHATHUE = @idmn
end
go

-- Duyệt nhà bán
create proc DuyetNhaBan @idnha int, @idnv int
as
begin
	update NhaBan set IDNHANVIEN = @idnv where IDNHABAN = @idnha and IDNHANVIEN is null
	select * from NHATHUE where IDNHANVIEN is NULL
end
go

-- Duyệt nhà thuê
create proc DuyetNhaThue @idnha int, @idnv int
as
begin
	update NhaThue set IDNHANVIEN = @idnv where IDNHATHUE = @idnha and IDNHANVIEN is null
	select * from NHATHUE where IDNHANVIEN is null
end
go

-- Thêm chủ nhà
create proc InsertChuNha @ten nvarchar(50), @dc nvarchar(100), @sdt nvarchar(20), @loai int
as
begin
	insert into CHUNHA(TENCHUNHA, DIACHI, SDT, LOAICHUNHA) values(@ten, @dc, @sdt, @loai)
end
go

-- Sửa chủ nhà
create proc UpdateChuNha @idcn int ,@ten nvarchar(50), @dc nvarchar(100), @sdt nvarchar(20), @loai nvarchar(20)
as
begin
	update CHUNHA set TENCHUNHA = @ten, DIACHI = @dc, SDT = @sdt, LOAICHUNHA = @loai where IDCHUNHA = @idcn
end
go

-- Thêm khách hàng
create proc InsertKhachHang @ten nvarchar(50), @dc nvarchar(100), @sdt nvarchar(20), @yc nvarchar(500)
as
begin
	insert into KHACHHANG(TENKHACHHANG, DIACHI, SDT, YEUCAU) values(@ten, @dc, @sdt, @yc)
end
go

-- Sửa khách hàng
create proc UpdateKhachHang @idkh int ,@ten nvarchar(50), @dc nvarchar(100), @sdt nvarchar(20), @yc nvarchar(500)
as
begin
	update KHACHHANG set TENKHACHHANG = @ten, DIACHI = @dc, SDT = @sdt, YEUCAU = @yc where IDKHACHHANG = @idkh
end
go

-- Select hợp đồng mua và thuê theo id chủ nhà
create proc SelectHDMB @loai int, @idcn int
as
begin
	if(@loai = 1)
		select hdm.* from HOPDONGMUA hdm, NHABAN nb where nb.IDCHUNHA = @idcn and hdm.IDNHABAN = nb.IDNHABAN
	else if(@loai = 2)
		select hdt.* from HOPDONGTHUE hdt, NHATHUE nt where nt.IDCHUNHA = @idcn and hdt.IDNHATHUE = nt.IDNHATHUE
end
go

-- Select hợp đồng mua và thuê theo id khách hàng
create proc SelectHDMBMT @loai int, @idkh int
as
begin
	if(@loai = 1)
		select hdm.* from HOPDONGMUA hdm where hdm.IDKHACHHANG = @idkh
	else if(@loai = 2)
		select hdt.* from HOPDONGTHUE hdt where hdt.IDKHACHHANG = @idkh
end
go

-- Select hợp đồng mua và bán theo id nhà
create proc SelectHDNBT @loai int, @idnha int
as
begin
	if(@loai = 1)
		select hdm.* from HOPDONGMUA hdm where hdm.IDNHABAN = @idnha
	else if(@loai = 2)
		select hdt.* from HOPDONGTHUE hdt where hdt.IDNHATHUE = @idnha
end
go

-- Select khách hàng
create proc SelectKH @loai int, @idnha int
as
begin
	if(@loai = 1)
		select kh.* from HOPDONGMUA hdm, KHACHHANG kh where hdm.IDNHABAN = @idnha and kh.IDKHACHHANG = hdm.IDKHACHHANG
	else if(@loai = 2)
		select kh.* from HOPDONGTHUE hdt, KHACHHANG kh where hdt.IDNHATHUE = @idnha and kh.IDKHACHHANG = hdt.IDKHACHHANG
end
go

-- Select nhân viên
create proc SelectNV @loai int, @idnha int
as
begin
	if(@loai = 1)
		select nv.* from NHABAN nb, NHANVIEN nv where nb.IDNHABAN = @idnha and nv.IDNHANVIEN = nb.IDNHANVIEN
	else if(@loai = 2)
		select nv.* from NHATHUE nt, NHANVIEN nv where nt.IDNHATHUE = @idnha and nv.IDNHANVIEN = nt.IDNHANVIEN
end
go

