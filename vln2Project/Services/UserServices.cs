﻿using h37.Models;
using h37.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace h37.Services
{
    
    public class UserServices
    {
        private ApplicationDbContext db;

        public UserServices()
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int createUser(string userName, string password)
        {
            /* todo create new User */
            User newUser = new User(userName, password);
            db.SystemUsers.Add(newUser);
            db.SaveChanges();

            return 0;
        }
        public User getUserByID(int userID)
        {
            /* todo return UserID  */
            User id = (from x in db.SystemUsers
                       where x.userID.Equals(userID)
                       select x).SingleOrDefault();
            if (id == null)
            {
                /* Todo exception if búbú */
            }
            return id;
        }
        public User getUserByName(string userName)
        {
            /* todo return User name */
            User n = (from x in db.SystemUsers
                      where x.userName.Equals(userName)
                      select x).SingleOrDefault();
            if (n == null)
            {
                /* Todo exception if búbú */
            }
            return n;
        }
        public List<Project> getProjects(int userID)
        {
            /* return list of  projects */
            return null;
        }
        public void subscribeUser(int projectID, string userName)
        {
            /* Todo */
        }
        public void unsubscribeUser(int projectID, string userName)
        {
            /* Todo */
        }
    }
}