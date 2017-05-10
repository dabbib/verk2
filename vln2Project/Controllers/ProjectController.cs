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

        [HttpGet]
        public ActionResult CreateProject()
        {
            ProjectCreateViewModel model = new ProjectCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateProject(ProjectCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                _service.createProject(model.projectName, User.Identity.GetUserId<string>(), model.projectType);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
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