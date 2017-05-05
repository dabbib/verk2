using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class User
    {
        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            projectList = new List<int>();
        }
        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<int> projectList { get; set; }
    }
}