﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.ViewModels
{
    public class ProjectConfigViewModel
    {
        public string projectName { get; set; }

        /// <summary>
        /// SKOÐA!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public List<ProjectViewModels> fileList { get; set; }

        public string projectType { get; set; }
    }
}