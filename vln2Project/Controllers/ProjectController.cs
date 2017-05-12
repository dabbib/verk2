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
        private UserServices _uService = new UserServices();

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }

        /// <summary>
        /// This function post information about newly created project to view.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ProjectCreateViewModel</returns>
        [HttpPost]
        [Authorize]
        public ActionResult CreateProject(ProjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var r = _service.createProject(model.projectName, User.Identity.GetUserId<string>(), model.projectType);
                    return RedirectToAction("Edit", new { projectID = r });

                }
                catch (Exception e)
                {
                    ViewBag.ReturnUrl = Request.Url.AbsoluteUri;
                    return View("Error", e);
                }
            }
            return View(model);
        }

        /// <summary>
        /// This function deletes a given project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>Redirects to index for user</returns>
        public ActionResult DeleteProject(int projectID)
        {
            try
            {
                _service.deleteProject(projectID);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Index", "User");
        }

        /// <summary>
        /// This function gets the edit view for a given project or file in project
        /// depending on the parameters.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="fileID"></param>
        /// <returns>ProjectEditViewModel to project edit view</returns>
        [HttpGet]
        [Authorize]
        [Route("/Edit/projectID:int/fileID:int")]
        public ActionResult Edit(int projectID = 0, int fileID = 0)
        {
            if(projectID == 0)
            {
                return RedirectToAction("Index", "User");
            }
            var p = _service.getProjectByID(projectID);
            var e = _service.getEventLogForProject(projectID);
            var f = _service.getFiles(projectID);
            var x = new ProjectEditViewModel() { projectID = p.projectID, projectName = p.projectName, numberOfFiles = p.numberOfFiles, eventList = e, fileList = f };
            if (fileID != 0)
            {
                var file = _service.getFileByID(fileID);
                ViewBag.code = file.content;
                ViewBag.fileID = file.fileID;
                ViewBag.fileName = "You are editing " + file.fileName;
            }
            else
            {
                ViewBag.fileName = "Please open file to edit";
            }
            return View(x);
        }

        /// <summary>
        /// This function sends information to the config view.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>ProjectConfigViewModel to config view.</returns>
        [HttpGet]
        [Authorize]
        [Route ("/Project/Configure/projectID:int")]
        public ActionResult Config(int projectID = 0)
        {
            if(projectID == 0)
            {
                return RedirectToAction("Index", "User");
            }
            Project p = _service.getProjectByID(projectID);
            ProjectConfigViewModel model = new ProjectConfigViewModel();
            model.projectID = p.projectID;
            model.projectName = p.projectName;
            model.projectOwnerID = p.projectOwnerID;
            model.numberOfFiles = p.numberOfFiles;
            model.type = p.type;
            model.userList = _uService.getListOfUsers();
            model.usersInProject = _uService.getListOfUsersInProject(projectID);
            return View(model);
        }

        /// <summary>
        /// This function reloads the config page.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Reloads the page</returns>
        [HttpPost]
        [Authorize]
        [Route ("/Project/Configure/projectID:int")]
        public ActionResult SaveConfig(Project item)
        {
            _service.updateProject(item);
            return RedirectToAction("Config", "Project", new { projectID = item.projectID });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateFile()
        {
            FileCreateViewModel model = new FileCreateViewModel();
            return View(model);
        }

        /// <summary>
        /// This function creates new files.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Reloads the page</returns>
        [HttpPost]
        [Authorize]
        public ActionResult CreateFile(FileCreateViewModel model)
        {
            model.userID = User.Identity.GetUserId<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    _service.createFile(model.projectID, model.userID, model.fileName);
                }
                catch (Exception e)
                {
                    ViewBag.ReturnUrl = Request.Url.AbsoluteUri;
                    return View("Error", e);
                }
                return RedirectToAction("Edit", new { projectID = model.projectID });
            }
            return null;
        }

        /// <summary>
        /// This function gets the edit view for a file in project.
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="projectID"></param>
        /// <returns>Sends file id and project to view.</returns>
        [HttpGet]
        [Authorize]
        [Route("~/Project/Edit/projectID:int/fileID:int")]
        public ActionResult EditFile(int fileID, int projectID)
        {
            try
            {
                ViewBag.Code = _service.getFileByID(fileID).content;
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Edit", new { projectID = projectID, fileID = fileID });
        }

        /// <summary>
        /// This function saves the file beeing edited and reloads the page.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Reloads the page.</returns>
        [HttpPost]
        [Authorize]
        [Route("~/Project/Edit/projectID:int/fileID:int")]
        public ActionResult SaveFile(FileSaveViewModel model)
        {
            try
            {
                _service.saveFileContent(model, User.Identity.GetUserId<string>());
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Edit", new { projectID = model.projectID, fileID = model.fileID });
        }

        /// <summary>
        /// This function deletes a given file.
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns>Reloads the page.</returns>
        [Authorize]
        public ActionResult DeleteFile(int fileID)
        {
            var f = _service.getFileByID(fileID);
            try
            {   
                _service.deleteFile(fileID);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Edit", new { projectID = f.projectID });
        }

    }
}