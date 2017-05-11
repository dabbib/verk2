using h37.Models.ViewModels;
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
        private UserServices _uService = new UserServices();
        private ProjectsServices _pService = new ProjectsServices();

        /// <summary>
        /// Returns list of projects for the user logged in.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var result = _pService.getProjectsForUser(User.Identity.GetUserId<string>());
            var model = new ProjectViewModel { projectList = result, createProject = null };
            return View(model);
        }
    }
}