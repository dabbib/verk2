using h37.Models;
using h37.Models.Entities;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// This function creates a blank project with one file called index.js for js projects.
        /// </summary>
        /// <param name="userID">ID of the user creating the project</param>
        /// <param name="type">Type of project</param>
        /// <returns></returns>
        public int createProject(int userID, string projectName, projectType type)
        {
            Project newProject = new Project();
            newProject.projectName = projectName;
            newProject.projectOwnerID = userID;
            newProject.type = type;
            newProject.numberOfFiles = 0;
            createFile(newProject.projectID, userID, "index." + type);
            db.Projects.Add(newProject);
            db.SaveChanges();

            return newProject.projectID;
        }
        /// <summary>
        /// Function to get projects by id
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>Single project</returns>
        public Project getProjectByID(int projectID)
        {
            Project p = (from x in db.Projects
                         where x.projectID.Equals(projectID)
                         select x).SingleOrDefault();
            if(p == null)
            {
                /* Todo exceptions if project does not exist */
            }
            return p;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int getNumberOfFilesInProject(int projectID)
        {
            return getProjectByID(projectID).numberOfFiles;
        }
        /// <summary>
        /// This function deletes projects
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>Returns the projectID</returns>
        public int deleteProject(int projectID)
        {
            /* Todo */
            return 0;
        }
        /// <summary>
        /// This function creates a new file in a given project
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="fileName"></param>
        /// <returns>fileID of file created</returns>
        public int createFile(int projectID, int userID, string fileName)
        {
            File newFile = new File();
            newFile.fileName = fileName;
            newFile.fileType = getProjectType(projectID).ToString();
            incrementProjectFileCounter(projectID);
            Event newEvent = new Event(userID, DateTime.Now, 0);
            db.Events.Add(newEvent);
            db.SaveChanges();
            db.Files.Add(newFile);
            db.SaveChanges();
            return newFile.fileID;
        }
        /// <summary>
        /// This function deletes a single file from a given project
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns>fileID of the file deleted</returns>
        public int deleteFile(int fileID)
        {
            /* Todo */
            return 0;
        }
        public void subscribeUser(int projectID, string userName)
        {
            /* Todo */
        }
        public void unsubscribeUser(int projectID, string userName)
        {
            /* Todo */
        }
        public List<File> getFiles(int projectID)
        {
            /* Todo */
            return null;
        }
        public void logEvent(int userID, int fileID)
        {
            /* Todo */
        }
        public List<Event> getEventLog(int fileID)
        {
            /* Todo */
            return null;
        }


        /*
         * The following functions are private helper functions 
         * for the other functions in ProjectServices.
         */
         /// <summary>
         /// 
         /// </summary>
         /// <param name="projectID"></param>
         /// <returns></returns>
        private projectType getProjectType(int projectID)
        {
            Project p = (from x in db.Projects
                         where x.projectID.Equals(projectID)
                         select x).SingleOrDefault();
            if(p == null)
            {
                /* Todo  project does not exist exception */
            }
            return p.type;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectID"></param>
        private void incrementProjectFileCounter(int projectID)
        {
            Project p = (from x in db.Projects
                         where x.projectID.Equals(projectID)
                         select x).SingleOrDefault();
            if(p == null)
            {
                /* Todo project not found exception */
            }
            p.numberOfFiles++;
        }

    }
}