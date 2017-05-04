using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class Project
    {
        public enum projectType { js = 0, cs = 1}

        public int projectID { get; set;}
        public string projectName { get; set; }
        public User projectOwner { get; set; }
        public int numberOfFiles { get; set; }
        public List<File> fileList { get; set; }
        public projectType type { get; set; }
    }
}