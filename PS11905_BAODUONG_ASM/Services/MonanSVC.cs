using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PS11905_BAODUONG_ASM.Models;

namespace PS11905_BAODUONG_ASM.Services
{
    public interface IMonAnSvc
    {
        Task<List<MonAn>> GetFoodsAsync();

        Task<MonAn> GetFoodById(int id);

        Task<List<MonAn>> GetFoodsAsyncByName(string name);

        MonAn GetMonAn(int id);

        int AddMonAn(MonAn monAn);

        int EditMonAn(int id, MonAn monAn);
    }

    public class MonanSVC : IMonAnSvc
    {
        protected DataContext _context;
        public MonanSVC(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MonAn>> GetFoodsAsyncByName(string name)
        {
            return await _context.MonAns.Where(x => x.Name.Contains(name.Trim())).ToListAsync();
        }

        public async Task<List<MonAn>> GetFoodsAsync()
        {
            return await _context.MonAns.ToListAsync();
        }

        public async Task<MonAn> GetFoodById(int id)
        {
            return await _context.MonAns.FindAsync(id);
        }

        public MonAn GetMonAn(int id)
        {
            MonAn monAn = null;
            monAn = _context.MonAns.Find(id); //cách này chỉ dùng cho Khóa chính
            //product = _context.Products.Where(e=>e.Id==id).FirstOrDefault(); //cách tổng quát
            return monAn;
        }

        public int AddMonAn(MonAn monAn)
        {
            int ret = 0;
            try
            {
                _context.Add(monAn);
                _context.SaveChanges();
                ret = monAn.MonAnID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }


        public int EditMonAn(int id, MonAn monAn)
        {
            int ret = 0;
            try
            {
                MonAn _monAn = null;
                _monAn = _context.MonAns.Find(id);
                _monAn.Name = monAn.Name;
                _monAn.Gia = monAn.Gia;
                _monAn.Phanloai = monAn.Phanloai;
                _monAn.HinhAnh = monAn.HinhAnh;
                _monAn.MoTa = monAn.MoTa;
                _monAn.TrangThai = monAn.TrangThai;

                _context.Update(_monAn);
                _context.SaveChanges();
                ret = monAn.MonAnID;
            }
            catch
            {

                ret = 0;
            }
            return ret;
        }




    }
}
