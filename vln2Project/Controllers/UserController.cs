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
            var projectList = _pService.getProjectsForUser(User.Identity.GetUserId<string>());
            var sharedProjectList = _pService.getProjectsSharedWithUser(User.Identity.GetUserId<string>());
            var model = new ProjectViewModel
            {
                userID = User.Identity.GetUserId<string>(),
                projectList = projectList,
                sharedProjectList = sharedProjectList,
                createProject = null
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SubscribeUser(string userName, int projectID)
        {
            string userID = _uService.getUserByName(userName).Id;
            try
            {
                _uService.subscribeUser(userID, projectID);
            }
            catch (ArgumentException e)
            {
                return new HttpStatusCodeResult(403, e.Message);
            }
            
            return RedirectToAction("Config", "Project", new { projectID = projectID });
        }

        [HttpPost]
        public ActionResult UnsubscribeUser(string userID, int projectID)
        {
            _uService.unsubscribeUser(userID, projectID);
            
            return RedirectToAction("Config", "Project", new { projectID = projectID });
        }
    }
}