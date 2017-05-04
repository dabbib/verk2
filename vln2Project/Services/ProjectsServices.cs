using h37.Models;
using h37.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Services
{
    public class ProjectsServices
    {
        private ApplicationDbContext db;

        public ProjectsServices()
        {
            db = new ApplicationDbContext();
        }
        public List<ProjectViewModel> getProjectsForUser(int userID)
        {

            var viewModel = new List<ProjectViewModel>
            {
                /* Todo set data */
            };
            return viewModel;
        }

    }
}