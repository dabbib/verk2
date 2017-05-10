using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static h37.Models.Entities.Project;

namespace h37.Models.ViewModels
{
    public class ProjectViewModel
    {

        public int projectID { get; set; }
        public string projectName { get; set; }
        public int numberOfFiles { get; set; }

    }
}