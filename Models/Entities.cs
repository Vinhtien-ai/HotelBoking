using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace HotelBoking.Models
{
    [Table("LoaiPhong")]
    public class LoaiPhong
    {
        [Key, StringLength(10)] public string MaLoaiPhong { get; set; }
        [StringLength(100)] public string TenLoaiPhong { get; set; }
        public double GiaPhong { get; set; }
        public virtual ICollection<Phong> Phongs { get; set; }
    }

    [Table("TrangThaiPhong")]
    public class TrangThaiPhong
    {
        [Key, StringLength(10)] public string MaTrangThai { get; set; }
        [StringLength(100)] public string TenTrangThai { get; set; }
        public virtual ICollection<Phong> Phongs { get; set; }
    }

    [Table("Phong")]
    public class Phong
    {
        [Key, StringLength(10)] public string MaPhong { get; set; }
        [StringLength(150)] public string TenPhong { get; set; }
        public string MoTaPhong { get; set; }
        [StringLength(10)] public string MaTrangThai { get; set; }
        [StringLength(10)] public string MaLoaiPhong { get; set; }
        public string HinhAnh { get; set; }
        public int? SoNguoi { get; set; }
        public int? DienTich { get; set; }
        public string TienIch { get; set; }
        public double? GiaPhong { get; set; }
        public virtual LoaiPhong LoaiPhong { get; set; }
        public virtual TrangThaiPhong TrangThaiPhong { get; set; }
    }

    [Table("Person")]
    public class Person
    {
        [Key, StringLength(10)] public string PersonId { get; set; }
        [StringLength(150)] public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public int GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Cccd { get; set; }
        public string Sdt { get; set; }
    }

    [Table("LoaiTaiKhoan")]
    public class LoaiTaiKhoan
    {
        [Key] public int MaLoaiTaiKhoan { get; set; }
        [StringLength(50)] public string TenLoai { get; set; }
    }

    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key, StringLength(10)] public string MaTaiKhoan { get; set; }
        [Index(IsUnique = true), StringLength(50)] public string UserName { get; set; }
        [StringLength(256)] public string Password { get; set; }
        public int MaLoaiTaiKhoan { get; set; }
        [StringLength(10)] public string PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual LoaiTaiKhoan LoaiTaiKhoan { get; set; }
    }

    [Table("KhachHang")]
    public class KhachHang
    {
        [Key, ForeignKey("Person"), StringLength(10)] public string KhachHangId { get; set; }
        public virtual Person Person { get; set; }
    }

    [Table("OrderPhong")]
    public class OrderPhong
    {
        [Key, StringLength(10)] public string MaOrderPhong { get; set; }
        public DateTime? NgayDen { get; set; }
        public DateTime? NgayDi { get; set; }
        [StringLength(10)] public string PersonId { get; set; }
        [StringLength(10)] public string MaPhong { get; set; }
        public int TrangThaiThanhToan { get; set; }
        public int? SoKhach { get; set; }
        public double? TongTien { get; set; }
        public string GhiChu { get; set; }
        public virtual Person Person { get; set; }
        public virtual Phong Phong { get; set; }
    }

    [Table("DichVu")]
    public class DichVu
    {
        [Key, StringLength(10)] public string MaDichVu { get; set; }
        [StringLength(150)] public string TenDichVu { get; set; }
        public double GiaDichVu { get; set; }
    }

    [Table("HoaDon")]
    public class HoaDon
    {
        [Key, StringLength(10)] public string MaHoaDon { get; set; }
        public DateTime? NgayIn { get; set; }
        public double TongTien { get; set; }
        [StringLength(10)] public string MaOrderPhong { get; set; }
    }

    public class HotelBokingEntities : DbContext
    {
        public HotelBokingEntities() : base("name=HotelBokingEntities") { Database.SetInitializer<HotelBokingEntities>(null); }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public DbSet<TrangThaiPhong> TrangThaiPhongs { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<LoaiTaiKhoan> LoaiTaiKhoans { get; set; }
        public DbSet<OrderPhong> OrderPhongs { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
    }
}