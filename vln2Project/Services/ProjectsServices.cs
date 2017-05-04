using h37.Models;
using h37.Models.Entities;
using h37.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            newProject.projectOwner = null;
            db.Projects.Add(newProject);
            db.SaveChanges();
            
            /* Todo */

            return 0;
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
        public int createFile(int projectID, string fileName)
        {
            /* Todo */
            return 0;
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

    }
}