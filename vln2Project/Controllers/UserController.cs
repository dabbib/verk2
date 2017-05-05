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
        [Authorize]
        public ActionResult Index(int? id)
        {
            if(id == null)
            {
                return View();
            }

            var UserViewModelID = _service.getUserByID(id.Value);

            return View(UserViewModelID);
        }
    }
}