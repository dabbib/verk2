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

        /// <summary>
        /// This function subscribes a user to given project.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="projectID"></param>
        /// <returns>Reloads the page to show the new user subscribtion</returns>
        [HttpPost]
        [Authorize]
        public ActionResult SubscribeUser(string userName, int projectID)
        {
            try
            {
                string userID = _uService.getUserByName(userName).Id;
                _uService.subscribeUser(userID, projectID);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            
            return RedirectToAction("Config", "Project", new { projectID = projectID });
        }

        /// <summary>
        /// This function usnsubscribes user from a given project.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="projectID"></param>
        /// <returns>Reloads the page</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Unsubscribe(string userID, int projectID)
        {
            _uService.unsubscribeUser(userID, projectID);
            return RedirectToAction("Index", "user");
        }

        /// <summary>
        /// This function allows project owner to remove users from project.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="projectID"></param>
        /// <returns>Reloads the page</returns>
        [HttpPost]
        [Authorize]
        public ActionResult UnsubscribeUser(string userID, int projectID)
        {
            _uService.unsubscribeUser(userID, projectID);
            return RedirectToAction("Config", "Project", new { projectID = projectID });
        }
    }
}