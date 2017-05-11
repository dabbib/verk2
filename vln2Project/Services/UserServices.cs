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
            var x = new usersInProjects(userID, projectID);
            db.UsersInProjects.Add(x);
            db.SaveChanges();
        }
        public void unsubscribeUser(int projectID, string userID)
        {
            /* Todo */
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


    }
}