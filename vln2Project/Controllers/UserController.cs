using h37.Services;
using Microsoft.AspNet.Identity;
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

            /* Return userID as string from User.Identity */
            return View(User.Identity.GetUserId<string>());
        }
    }
}