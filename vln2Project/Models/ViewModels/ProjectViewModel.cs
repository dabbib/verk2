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
        public List<Project> projectList { get; set; }
        public ProjectCreateViewModel createProject { get; set; }

    }
}