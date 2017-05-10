using h37.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using h37.Models.ViewModels;
using System.Collections;
using h37.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace h37.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectsServices _service = new ProjectsServices();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// To get create project view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateProject()
        {
            ProjectCreateViewModel model = new ProjectCreateViewModel();
            return View(model);
        }

        /// <summary>
        /// To post information about created project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateProject(ProjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.createProject(model.projectName, User.Identity.GetUserId<string>(), model.projectType);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route ("/Edit/projectID:int")]
        public ActionResult Edit(int projectID)
        {
            var p = _service.getProjectByID(projectID);
            var e = _service.getEventLogForProject(projectID);
            var f = _service.getFiles(projectID);
            var x = new ProjectEditViewModel() { projectID = p.projectID, projectName = p.projectName, numberOfFiles = p.numberOfFiles, eventList = e, fileList = f };
            return View(x);
        }







        public ActionResult projectName (int projectID)
        {
            var ProjectViewModelName = _service.getProjectByID(projectID);

            return View(ProjectViewModelName);

        }

        public ActionResult numberOfFiles(int projectID)
        {
            var ProjectViewModelFiles = _service.getNumberOfFilesInProject(projectID);

            return View(ProjectViewModelFiles);

        }
    }
}