using h37.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.ViewModels
{
    public class ProjectEditViewModel
    {
        public int projectID { get; set; }
        public string projectName { get; set; }
        public int numberOfFiles { get; set; }
        public List<File> fileList { get; set; }
        public List<Event> eventList { get; set; }
        public FileCreateViewModel createFile { get; set; }

    }
}