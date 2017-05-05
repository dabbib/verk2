using h37.Models;
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
        public int getUserID(int userID)
        {
            /* todo return UserID  */
            return 0;
        }
        public int getName(string userName)
        {
            /* todo return User name */
            return 0;
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