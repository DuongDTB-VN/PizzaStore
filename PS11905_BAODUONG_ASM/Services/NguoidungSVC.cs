using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PS11905_BAODUONG_ASM.Models;
using PS11905_BAODUONG_ASM.Helpers;
using PS11905_BAODUONG_ASM.Models.ViewModels;

namespace PS11905_BAODUONG_ASM.Services
{
    public interface INguoidungSvc
    {
        List<Nguoidung> GetNguoidungAll();

        Nguoidung GetNguoidung(int id);

        int AddNguoidung(Nguoidung nguoidung);

        int EditNguoidung(int id, Nguoidung nguoidung);

        public Nguoidung Login(ViewLogin viewlogin);
    }

    public class NguoidungSVC : INguoidungSvc
    {
        protected DataContext _context;
        protected IMaHoaHelper _mahoaHelper;

        public NguoidungSVC(DataContext context, IMaHoaHelper mahoaHelper)
        {
            _context = context;
            _mahoaHelper = mahoaHelper;
        }

        public List<Nguoidung> GetNguoidungAll()
        {
            List<Nguoidung> list = new List<Nguoidung>();
            list = _context.Nguoidungs.ToList();
            return list;          
        }

        public Nguoidung GetNguoidung(int id)
        {
            Nguoidung nguoidung = null;
            nguoidung = _context.Nguoidungs.Find(id);
            return nguoidung;
        }

        public int AddNguoidung(Nguoidung nguoidung)
        {
            int ret = 0;
            try
            {
                nguoidung.Password = _mahoaHelper.Mahoa(nguoidung.Password);
                _context.Add(nguoidung);
                _context.SaveChanges();
                ret = nguoidung.NguoidungID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int EditNguoidung(int id, Nguoidung nguoidung)
        {
            int ret = 0;
            try
            {
                Nguoidung _nguoidung = null;
                _nguoidung = _context.Nguoidungs.Find(id);
                _nguoidung.UserName = nguoidung.UserName;
                _nguoidung.FullName = nguoidung.FullName;
                _nguoidung.Title = nguoidung.Title;
                _nguoidung.DOB = nguoidung.DOB;
                _nguoidung.Email = nguoidung.Email;
                _nguoidung.Admin = nguoidung.Admin;
                _nguoidung.Locked = nguoidung.Locked;
                if (nguoidung.Password != null)
                {
                    nguoidung.Password = _mahoaHelper.Mahoa(nguoidung.Password);
                    _nguoidung.Password = nguoidung.Password;
                }
                _context.Update(_nguoidung);
                _context.SaveChanges();
                ret = nguoidung.NguoidungID;

            }
            catch 
            {

                ret = 0;
            }
            return ret;
        }

        //Login Nguoidung
        public Nguoidung Login(ViewLogin viewlogin)
        {
            var u = _context.Nguoidungs.Where(
                    p => p.UserName.Equals(viewlogin.UserName)
                    && p.Password.Equals(_mahoaHelper.Mahoa(viewlogin.Password))
                    ).FirstOrDefault();
            return u;
        }

    }
}
