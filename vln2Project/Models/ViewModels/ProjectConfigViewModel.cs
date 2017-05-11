using h37.Models.ViewModel;
using System.Collections.Generic;
using static h37.Models.Entities.Project;

namespace h37.Models.ViewModels
{
    public class ProjectConfigViewModel
    {
        public ProjectConfigViewModel()
        {

        }
        public string projectName { get; set; }
        public string projectOwnerID { get; set; }
        public int numberOfFiles { get; set; }
        public projectType type { get; set; }
        public List<UserViewModel> userList { get; set; }
        public List<UserViewModel> usersInProject { get; set; }
    }
}