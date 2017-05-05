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
            Project newProject = new Project(projectName, userID, type);
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
        public void deleteProject(int projectID)
        {
            var fileList = getFiles(projectID);
            foreach(File x in fileList)
            {
                deleteFile(x.fileID);
            }
            db.Projects.Remove(getProjectByID(projectID));
            db.SaveChanges();
        }
        /// <summary>
        /// This function creates a new file in a given project
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="fileName"></param>
        /// <returns>fileID of file created</returns>
        public int createFile(int projectID, int userID, string fileName)
        {
            File newFile = new File(fileName, getProjectType(projectID).ToString(), projectID);
            incrementProjectFileCounter(projectID);
            Event newEvent = new Event(userID, newFile.fileID, DateTime.Now, Event.eventType.created);
            db.Files.Add(newFile);
            db.Events.Add(newEvent);
            db.SaveChanges();
            return newFile.fileID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public File getFileByID(int fileID)
        {
            var f = (from x in db.Files
                     where x.fileID.Equals(fileID)
                     select x).SingleOrDefault();
            if(f == null)
            {
                /* Todo exception */
            }
            return f;
        }
        /// <summary>
        /// This function deletes a single file from a given project
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns>fileID of the file deleted</returns>
        public void deleteFile(int fileID)
        {
            decrementProjectFileCounter(getFileByID(fileID).projectID);
            db.Files.Remove(getFileByID(fileID));
            db.SaveChanges();
        }



        public List<File> getFiles(int projectID)
        {
            List<File> f = (from x in db.Files
                            where x.projectID.Equals(projectID)
                            select x).ToList();
            return f;
        }
        public void logEvent(int userID, int fileID)
        {
            Event e = new Event(userID, fileID, DateTime.Now, Event.eventType.modified);
            db.Events.Add(e);
            db.SaveChanges();
        }
        public List<Event> getEventLog(int fileID)
        {
            List<Event> e = (from x in db.Events
                             where x.fileID.Equals(fileID)
                             select x).ToList();
            return e;
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
            return getProjectByID(projectID).type;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectID"></param>
        private void incrementProjectFileCounter(int projectID)
        {
            Project p = getProjectByID(projectID);
            p.numberOfFiles++;
            db.SaveChanges();
        }

        private void decrementProjectFileCounter(int projectID)
        {
            Project p = getProjectByID(projectID);
            p.numberOfFiles--;
            db.SaveChanges();
        }

    }
}