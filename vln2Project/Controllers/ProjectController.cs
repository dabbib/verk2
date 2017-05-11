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
            return RedirectToAction("Index", "User");
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
                try
                {
                    var r = _service.createProject(model.projectName, User.Identity.GetUserId<string>(), model.projectType);
                    return RedirectToAction("Edit", new { projectID = r });

                }
                catch (ArgumentException e)
                {
                    return new HttpStatusCodeResult(403, e.Message);
                }
            }
            return View(model);
        }

        public ActionResult DeleteProject(int projectID)
        {
            _service.deleteProject(projectID);
            return RedirectToAction("Index", "User");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route ("/Edit/projectID:int/fileID:int")]
        public ActionResult Edit(int projectID, int fileID = 0)
        {
            var p = _service.getProjectByID(projectID);
            var e = _service.getEventLogForProject(projectID);
            var f = _service.getFiles(projectID);
            var x = new ProjectEditViewModel() { projectID = p.projectID, projectName = p.projectName, numberOfFiles = p.numberOfFiles, eventList = e, fileList = f };
            if(fileID != 0)
            {
                var file = _service.getFileByID(fileID);
                ViewBag.code = file.content;
                ViewBag.fileName = "You are editing " + file.fileName;
            }
            else
            {
                ViewBag.fileName = "Please open file to edit";
            }
            return View(x);
        }

        [HttpGet]
        public ActionResult CreateFile()
        {
            FileCreateViewModel model = new FileCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateFile(FileCreateViewModel model)
        {
            //model.projectID = projectID;
            model.userID = User.Identity.GetUserId<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    _service.createFile(model.projectID, model.userID, model.fileName);
                }
                catch (ArgumentException e)
                {
                    return new HttpStatusCodeResult(403, e.Message);
                }
                return RedirectToAction("Edit", new { projectID = model.projectID });
            }
            return null;
        }

        [HttpGet]
        [Route("~/Project/Edit/projectID:int/fileID:int")]
        public ActionResult EditFile(int fileID, int projectID)
        {
            ViewBag.Code = _service.getFileByID(fileID).content;
            return RedirectToAction("Edit", new { projectID = projectID, fileID = fileID });
        }

        [HttpPost]
        [Route("~/Project/Edit/projectID:int/fileID:int")]
        public ActionResult SaveFile(FileSaveViewModel model)
        {
            _service.saveFileContent(model, User.Identity.GetUserId<string>());
            return RedirectToAction("Edit", new { projectID = model.projectID, fileID = model.fileID });
        }

        public ActionResult DeleteFile(int fileID)
        {
            var f = _service.getFileByID(fileID);
            _service.deleteFile(fileID);
            return RedirectToAction("Edit", new { projectID = f.projectID });
        }

    }
}