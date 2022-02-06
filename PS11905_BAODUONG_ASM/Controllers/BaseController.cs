using PS11905_BAODUONG_ASM.Filters;
using PS11905_BAODUONG_ASM.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace PS11905_BAODUONG_ASM.Controllers
{
    [AuthenticationFilter]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        protected string GetUserName()
        {
            return HttpContext.Session.GetString(SessionKey.Nguoidung.UserName);

        }

        protected string GetFullName()
        {
            return HttpContext.Session.GetString(SessionKey.Nguoidung.FullName);
        }

        protected string GetKHEmail()
        {
            return HttpContext.Session.GetString(SessionKey.Khachhang.KH_Email);
        }
    }
}
