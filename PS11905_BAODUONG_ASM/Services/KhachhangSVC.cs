using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PS11905_BAODUONG_ASM.Models;
using PS11905_BAODUONG_ASM.Helpers;
using PS11905_BAODUONG_ASM.Models.ViewModels;

namespace PS11905_BAODUONG_ASM.Services
{
    public interface IKhachhangSvc
    {
        List<Khachhang> GetKhachhangAll();
        Khachhang GetKhachhang(int id);
        int AddKhachhang(Khachhang khachhang);
        int EditKhachhang(int id, Khachhang khachhang);
        Khachhang Login(ViewWebLogin viewWebLogin);

        bool FindEmail(string email);
    }
    public class KhachhangSVC : IKhachhangSvc
    {
        protected DataContext _context;
        protected IMaHoaHelper _maHoaHelper;

        public KhachhangSVC(DataContext context, IMaHoaHelper maHoaHelper)
        {
            _context = context;
            _maHoaHelper = maHoaHelper;
        }

        public List<Khachhang> GetKhachhangAll()
        {
            List<Khachhang> list = new List<Khachhang>();
            list = _context.Khachhangs.ToList();
            return list;
        }

        public Khachhang GetKhachhang(int id)
        {
            Khachhang khachhang = null;
            khachhang = _context.Khachhangs.Find(id);
            return khachhang;
        }

        public int AddKhachhang(Khachhang khachhang)
        {
            int ret = 0;

            if (!_context.Khachhangs.Any(x => x.Email.Equals(khachhang.Email)))
            {
                try
                {
                    khachhang.Password = _maHoaHelper.Mahoa(khachhang.Password);
                    khachhang.ConfirmPassword = khachhang.Password;

                    _context.Add(khachhang);
                    _context.SaveChanges();
                    ret = khachhang.KhachhangID;
                }
                catch
                {
                    ret = 0;
                }

            }
            return ret;
        }

        public int EditKhachhang(int id, Khachhang khachhang)
        {
            int ret = 0;
            try
            {
                Khachhang _khachhang = null;
                _khachhang = _context.Khachhangs.Find(id);

                _khachhang.FullName = khachhang.FullName;
                _khachhang.Ngaysinh = khachhang.Ngaysinh;
                _khachhang.PhoneNumber = khachhang.PhoneNumber;
                _khachhang.Email = khachhang.Email;
                if (_khachhang.Password != null)
                {
                    khachhang.Password = _maHoaHelper.Mahoa(khachhang.Password);
                    _khachhang.Password = khachhang.Password;
                    khachhang.ConfirmPassword = _maHoaHelper.Mahoa(khachhang.ConfirmPassword); //Mới thêm
                    _khachhang.ConfirmPassword = khachhang.ConfirmPassword;
                }

                _khachhang.Mota = khachhang.Mota;
                _context.Update(_khachhang);
                _context.SaveChanges();
                ret = _khachhang.KhachhangID;
            }
            catch
            {

                ret = 0;
            }
            return ret;
        }

        public Khachhang Login(ViewWebLogin viewWebLogin)
        {
            var u = _context.Khachhangs.Where(
                p => p.Email.Equals(viewWebLogin.Email)
                && p.Password.Equals(_maHoaHelper.Mahoa(viewWebLogin.Password))
                ).FirstOrDefault();
            return u;

        }

        public bool FindEmail(string email)
        {
            return _context.Khachhangs.Any(x => x.Email.Equals(email));
        }
    }
}
