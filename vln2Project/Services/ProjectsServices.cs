using h37.Models;
using h37.Models.Entities;
using h37.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using static h37.Models.Entities.Project;

namespace h37.Services
{
    public class ProjectsServices
    {
        private ApplicationDbContext db;

        public ProjectsServices()
        {
            db = new ApplicationDbContext();
        }
        

        #region Project functions

        /// <summary>
        /// This function creates a new project with one file called index.js for js projects.
        /// </summary>
        /// <param name="projectName">Name of the project provided by user</param>
        /// <param name="userID">ID of the user creating the project</param>
        /// <param name="type">Type of project provided by user</param>
        /// <returns>Id of the created project</returns>
        public int createProject(string projectName, string userID, projectType type)
        {
            Project newProject = new Project(projectName, userID, type);

            try
            {
                db.Projects.Add(newProject);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


            /* Create the index file for the new project */
            createFile(newProject.projectID, userID, "index");
            return newProject.projectID;

        }

        /// <summary>
        /// Function to get projects by id.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>Single project</returns>
        public Project getProjectByID(int projectID)
        {
            Project p = (from x in db.Projects
                         where x.projectID.Equals(projectID)
                         select x).SingleOrDefault();

            if (p == null)
            {
                /* Todo exceptions if project does not exist */
            }
            return p;
        }

        /// <summary>
        /// This functions returns all projects for a given user.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<ProjectViewModel> getProjectsForUser(string userID)
        {
            IEnumerable<ProjectViewModel> p = (from x in db.Projects
                                               where x.projectOwnerID.Equals(userID)
                                               select new ProjectViewModel()
                                               {
                                                   projectID = x.projectID,
                                                   projectName = x.projectName,
                                                   numberOfFiles = x.numberOfFiles
                                               }).ToList();
            return p;
        }

        /// <summary>
        /// Function to get the number of files in a given project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>Number of files in the given project</returns>
        public int getNumberOfFilesInProject(int projectID)
        {
            return getProjectByID(projectID).numberOfFiles;
        }

        /// <summary>
        /// This functions gets the type of a given project.
        /// </summary>
        /// <param name="projectID">ID of the project</param>
        /// <returns>Type of project</returns>
        public projectType getProjectType(int projectID)
        {
            return getProjectByID(projectID).type;
        }

        /// <summary>
        /// This function deletes a given project and also all of its files.
        /// </summary>
        /// <param name="projectID"></param>
        public void deleteProject(int projectID)
        {
            var fileList = getFiles(projectID);

            /* Delete all files in the project */
            foreach(File x in fileList)
            {
                deleteFile(x.fileID);
            }

            /*
             * Todo: Remove all links between the users of the project and the project
             */

            db.Projects.Remove(getProjectByID(projectID));
            db.SaveChanges();
        }

        #endregion

        #region File functions

        /// <summary>
        /// This function creates a new file in a given project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="userID"></param>
        /// <param name="fileName"></param>
        /// <returns>ID of the file created</returns>
        public int createFile(int projectID, string userID, string fileName)
        {
            var f = (from x in db.Files
                     where x.fileName.Equals(fileName) & x.projectID.Equals(projectID)
                     select x).SingleOrDefault();
            if(f != null)
            {
                throw new ArgumentException("This file name is already taken");
            }
            
            File newFile = new File(fileName +"."+ getProjectType(projectID).ToString(), getProjectType(projectID).ToString(), projectID);
            incrementProjectFileCounter(projectID);

            db.Files.Add(newFile);
            db.SaveChanges();

            Event newEvent = new Event(userID, newFile.fileID, DateTime.Now, Event.eventType.created);

            db.Events.Add(newEvent);
            db.SaveChanges();

            return newFile.fileID;
        }

        /// <summary>
        /// This function saves content of a file.
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="content"></param>
        public void saveFileContent(FileSaveViewModel model, string userID)
        {
            var f = getFileByID(model.fileID);
            f.content = model.content;
            logEvent(userID, model.fileID);
            db.SaveChanges();
        }

        /// <summary>
        /// This functions gets a given file by id.
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns>A single file</returns>
        public File getFileByID(int fileID)
        {
            var f = (from x in db.Files
                     where x.fileID.Equals(fileID)
                     select x).SingleOrDefault();

            if(f == null)
            {
                /* Todo exception if búbú */
            }

            return f;
        }

        /// <summary>
        /// This function gets a file list for a given project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>List of files</returns>
        public List<File> getFiles(int projectID)
        {
            List<File> f = (from x in db.Files
                            where x.projectID.Equals(projectID)
                            select x).ToList();

            return f;
        }

        /// <summary>
        /// This function deletes a single file from a given project
        /// and also the event logs associated with that file.
        /// </summary>
        /// <param name="fileID"></param>
        public void deleteFile(int fileID)
        {
            var l = getEventLog(fileID);

            /* Remove all logs associated with that file */
            foreach(Event x in l)
            {
                db.Events.Remove(x);
            }

            decrementProjectFileCounter(getFileByID(fileID).projectID);

            db.Files.Remove(getFileByID(fileID));
            db.SaveChanges();
        }

        #endregion

        #region Event functions

        /// <summary>
        /// This function logs an event associated with a file.
        /// </summary>
        /// <param name="userID">ID of the user that made changes</param>
        /// <param name="fileID">ID of the file beeing changed</param>
        public void logEvent(string userID, int fileID)
        {
            Event e = new Event(userID, fileID, DateTime.Now, Event.eventType.modified);

            db.Events.Add(e);
            db.SaveChanges();
        }

        /// <summary>
        /// This function return a list of events associated with a given file.
        /// </summary>
        /// <param name="fileID">ID of the file associated with the event logs</param>
        /// <returns>List of events</returns>
        public List<Event> getEventLog(int fileID)
        {
            List<Event> e = (from x in db.Events
                             where x.fileID.Equals(fileID)
                             select x).ToList();

            return e;
        }

        /// <summary>
        /// This function returns a list of events associated with a given project
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public List<EventLogViewModel> getEventLogForProject(int projectID)
        {
            List<EventLogViewModel> e = (from x in db.Events
                                         join f in db.Files on x.fileID equals f.fileID
                                         join u in db.Users on x.userID equals u.Id
                                         where f.projectID.Equals(projectID)
                                         select new EventLogViewModel()
                                         {
                                             timestamp = x.timestamp,
                                             userName = u.UserName,
                                             type = x.type,
                                             fileName = f.fileName
                                         }).ToList();
            return e;
        }

        #endregion

        #region Helper functions

        /// <summary>
        /// This function increments the project file counter for a given project.
        /// </summary>
        /// <param name="projectID"></param>
        private void incrementProjectFileCounter(int projectID)
        {
            Project p = getProjectByID(projectID);

            p.numberOfFiles++;

            db.SaveChanges();
        }

        /// <summary>
        /// This function decrements the project file counter for a given project.
        /// </summary>
        /// <param name="projectID"></param>
        private void decrementProjectFileCounter(int projectID)
        {
            Project p = getProjectByID(projectID);

            p.numberOfFiles--;

            db.SaveChanges();
        }

        #endregion

    }
}