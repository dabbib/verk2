﻿using h37.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h37.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectsServices _service = new ProjectsServices();

        // GET: Project
        public ActionResult Index()
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