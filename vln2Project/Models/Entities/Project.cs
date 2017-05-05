using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class Project
    {
        public Project(string projectName, int projectOwnerID, projectType type)
        {
            this.projectName = projectName;
            this.projectOwnerID = projectOwnerID;
            this.type = type;
            numberOfFiles = 0;
        }
        public enum projectType { js = 0, cs = 1}

        public int projectID { get; set;}
        public string projectName { get; set; }
        public int projectOwnerID { get; set; }
        public int numberOfFiles { get; set; }
        public projectType type { get; set; }
    }
}