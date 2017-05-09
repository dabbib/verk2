using h37.Models;
using h37.Models.Entities;
using h37.Models.ViewModels;
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
        
        public User getUserByID(int userID)
        {
            return null;
        }
        public User getUserByName(string userName)
        {
            return null;
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