using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PS11905_BAODUONG_ASM.Models;

namespace PS11905_BAODUONG_ASM.Services
{
    public interface IDonhangChitietSvc
    {
        int AddDonhangChitietSvc(DonhangChitiet donhangChitiet);
    }
    public class DonhangChitietSVC : IDonhangChitietSvc
    {
        protected DataContext _context;

        public DonhangChitietSVC(DataContext context)
        {
            _context = context;
        }

        public int AddDonhangChitietSvc(DonhangChitiet donhangChitiet)
        {
            int ret = 0;
            try
            {
                _context.Add(donhangChitiet);
                _context.SaveChanges();
                ret = donhangChitiet.ChitietID;
            }
            catch
            {

                ret = 0;
            }
            return ret;
        }
    }
}
