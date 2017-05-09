using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static h37.Models.Entities.Project;

namespace h37.Models.ViewModels
{
    public class ProjectCreateViewModel
    {
        public string projectName { get; set; }

        public projectType projectType { get; set; }
    }
}