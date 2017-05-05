using h37.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h37.Controllers
{
    public class UserController : Controller
    {
        private UserServices _service = new UserServices();

        // GET: User
        public ActionResult Index(int? id)
        {
            if(id == null)
            {
                return View();
            }

            var UserViewModelID = _service.getUserID(id.Value);

            return View(UserViewModelID);
        }
        /*
        public ActionResult Index(int id)
        {
            var UserViewModelID = _service.getUserID(id);

            return View(UserViewModelID);
        }*/

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}