using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PS11905_BAODUONG_ASM.Models;

namespace PS11905_BAODUONG_ASM.Services
{

    public interface IDonhangSvc
    {
        List<Donhang> GetDonhangAll();
        List<Donhang> GetDonhangbyKhachhang(int khachhangId);

        Donhang GetDonhang(int id);

        Task<List<Donhang>> GetBillByCustomerEmail(string email);

        int AddDonhang(Donhang donhang);
        int EditDonhang(int id, Donhang donhang);
    }

    public class DonhangSVC : IDonhangSvc
    {
        protected DataContext _context;

        public DonhangSVC(DataContext context)
        {
            _context = context;
        }

        public List<Donhang> GetDonhangAll()
        {
            List<Donhang> list = new List<Donhang>();
            list = _context.Donhangs.OrderByDescending(x => x.Ngaydat)
                .Include(x => x.Khachhang)
                .Include(x => x.DonhangChitiets)
                .ToList();
            return list;
        }

        public List<Donhang> GetDonhangbyKhachhang(int khachhangId)
        {
            List<Donhang> list = new List<Donhang>();
            list = _context.Donhangs.Where(x => x.KhachhangID == khachhangId).OrderByDescending(x => x.Ngaydat)
                .Include(x => x.Khachhang)
                .Include(x => x.DonhangChitiets)
                .ToList();
            return list;
        }

        public Donhang GetDonhang(int id)
        {
            //Donhang donhang = null;
            //donhang = _context.Donhangs.Where(x => x.DonhangID == id)
            //    .Include(x => x.Khachhang)
            //    .Include(x => x.DonhangChitiets).ThenInclude(y => y.MonAn)
            //    .FirstOrDefault();
            return _context.Donhangs
                .Include(x => x.Khachhang)
                .Include(x => x.DonhangChitiets).ThenInclude(y => y.MonAn)
                .SingleOrDefault(x => x.DonhangID.Equals(id));
        }

        public int AddDonhang(Donhang donhang)
        {
            int ret = 0;
            try
            {
                _context.Add(donhang);
                _context.SaveChanges();
                ret = donhang.DonhangID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ret = 0;
            }
            return ret;
        }

        public int EditDonhang(int id, Donhang donhang)
        {
            int ret = 0;
            try
            {
                _context.Update(donhang);
                _context.SaveChanges();
                ret = donhang.DonhangID;
            }
            catch (Exception ex)
            {

                ret = 0;
            }
            return ret;
        }

        public async Task<List<Donhang>> GetBillByCustomerEmail(string email)
        {
            var customer = await _context.Khachhangs.SingleOrDefaultAsync(x => x.Email.Equals(email));
            if (customer != null)
                return await _context.Donhangs.Where(x => x.KhachhangID.Equals(customer.KhachhangID)).ToListAsync();
            else return null;
        }
    }
}
