using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class User
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<Project> projectList { get; set; }
    }
}