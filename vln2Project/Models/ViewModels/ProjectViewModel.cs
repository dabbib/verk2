using h37.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static h37.Models.Entities.Project;

namespace h37.Models.ViewModels
{
    public class ProjectViewModel
    {
        public string userID { get; set; }
        public List<Project> projectList { get; set; }
        public List<Project> sharedProjectList { get; set; }
        public ProjectCreateViewModel createProject { get; set; }

    }
}