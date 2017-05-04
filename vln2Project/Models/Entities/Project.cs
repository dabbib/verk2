﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class Project
    {
        /// <summary>
        /// Entity class for projects
        /// </summary>
        public int id { get; set;}
        public string name { get; set; }
        public int numberOfFiles { get; set; }
    }
}