using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class usersInProjects
    {
        public usersInProjects(string userID, int projcetID)
        {
            this.userID = userID;
            this.projectID = projectID;
        }
        public int id { get; set; }
        public string userID { get; set; }
        public int projectID { get; set; }
    }
}