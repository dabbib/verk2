using h37.Models.ViewModel;
using System.Collections.Generic;
using static h37.Models.Entities.Project;

namespace h37.Models.ViewModels
{
    public class ProjectConfigViewModel
    {
        /*
         * This view model is sent to the Config view
         * for project configuration.
         */
        public ProjectConfigViewModel()
        {

        }
        public int projectID { get; set; }
        public string projectName { get; set; }
        public string projectOwnerID { get; set; }
        public int numberOfFiles { get; set; }
        public projectType type { get; set; }
        public List<UserViewModel> userList { get; set; }
        public string selectedUser { get; set; }
        public List<UserViewModel> usersInProject { get; set; }
    }
}