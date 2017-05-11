using h37.Models;
using h37.Models.Entities;
using h37.Models.ViewModel;
using h37.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;


namespace h37.Services
{

    public class UserServices
    {
        private ApplicationDbContext db;
        private ProjectsServices _pService = new ProjectsServices();

        public UserServices()
        {
            db = new ApplicationDbContext();
        }

        
        public void subscribeUser(string userID, int projectID)
        {
            var u = (from v in db.UsersInProjects
                     where v.projectID.Equals(projectID)
                     & v.userID.Equals(userID)
                     select v).SingleOrDefault();
            if(u != null)
            {
                throw new ArgumentException("User already in project");
            }
            if(_pService.getProjectByID(projectID).projectOwnerID.Equals(userID))
            {
                throw new ArgumentException("User is owner of project");
            }
            var x = new usersInProjects(userID, projectID);
            db.UsersInProjects.Add(x);
            db.SaveChanges();
        }
        public void unsubscribeUser(string userID, int projectID)
        {
            var u = (from v in db.UsersInProjects
                     where v.projectID.Equals(projectID)
                     & v.userID.Equals(userID)
                     select v).SingleOrDefault();
            db.UsersInProjects.Remove(u);
            db.SaveChanges();
        }

        public IUser getUserByName(string userName)
        {
            var u = (from x in db.Users
                     where x.UserName.Equals(userName)
                     select x).SingleOrDefault();
            return u;
        }

        public List<UserViewModel> getListOfUsers()
        {
            var r = (from x in db.Users
                     select new UserViewModel
                     {
                         userID = x.Id,
                         userName = x.UserName
                     }).ToList();
            return r;
        }

        public List<UserViewModel> getListOfUsersInProject(int projectID)
        {
            var r = (from x in db.UsersInProjects
                     join u in db.Users on x.userID equals u.Id
                     where x.projectID.Equals(projectID)
                     select new UserViewModel
                     {
                         userID = x.userID,
                         userName = u.UserName
                     } 
                     ).ToList();
            return r;
        }
    }
}